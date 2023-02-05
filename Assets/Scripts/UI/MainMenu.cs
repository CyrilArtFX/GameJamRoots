using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
	public class MainMenu : MonoBehaviour
	{
		[Header("Scene"), SerializeField]
		private string playScene;

		[SerializeField]
		private float timeToggleInput = 1.0f, timeForControllerPress = 0.2f;

		[SerializeField]
		private LayerMask uiMask;

		[SerializeField]
		private MenuController mainMenu;
		
		[SerializeField]
		private CinemachineVirtualCamera playCamera;
		[SerializeField]
		private float playTime = 1.5f;

		private MenuController currentMenu;

		private bool isMouseControlled = true;
		private bool isInputDisabled = false;
		private UI3D hoveredUI;

		private Vector3 farWorldPos, nearWorldPos, hitPos;
		private bool hitWasUI = false;

		private Vector3 lastMousePos = Vector3.zero;

		void Start()
		{
			//  set current menu
			currentMenu = mainMenu;
			currentMenu.Select();
		}

		public void Play()
		{
			StartCoroutine(CoroutinePlay());
			DisableInputFor(timeToggleInput);
		}

		IEnumerator CoroutinePlay()
		{
			//  switch scene
			playCamera.Priority = 30;

			//  wait
			yield return new WaitForSeconds(playTime);

			BlackFade.instance.eventEndOfFadeIn.AddListener(
				() =>
				{
					SceneManager.LoadScene(playScene);
				}
			);
			BlackFade.instance.StartFade( FadeType.BothFadesWithRestore, Color.white );
		}

		public void QuitGame()
		{
			BlackFade.instance.eventEndOfFadeIn.AddListener(
				() =>
				{
					#if UNITY_EDITOR
						UnityEditor.EditorApplication.isPlaying = false;
					#else
						Application.Quit();
					#endif
				}
			);
			BlackFade.instance.StartFade( FadeType.BothFadesWithRestore, Color.black );
			DisableInputFor(timeToggleInput);
		}

		void MouseUpdate()
		{
			//  handle button behaviour w/ mouse
			Camera camera = Camera.main;

			//  get far & near mouse positions
			Vector2 mouse_pos = GetMousePosition();
			Vector3 far_mouse_pos = new(mouse_pos.x, mouse_pos.y, camera.farClipPlane);
			Vector3 near_mouse_pos = new(mouse_pos.x, mouse_pos.y, camera.nearClipPlane);

			//  translate mouse position to world
			farWorldPos = camera.ScreenToWorldPoint(far_mouse_pos);
			nearWorldPos = camera.ScreenToWorldPoint(near_mouse_pos);

			//  check for button input
			if (Physics.Raycast(nearWorldPos, farWorldPos - nearWorldPos, out RaycastHit hit, 16.0f, uiMask))
			{
				GameObject hit_object = hit.collider.gameObject;

				if (hit_object.TryGetComponent(out UI3D ui))
				{
					SelectButton(ui);
					hitWasUI = true;
				}

				hitPos = hit.point;
			}
			else
			{
				//  unselect hovered button
				UnSelectButton();

				hitWasUI = false;
			}
		}

		public void PressHovered()
		{
			if (hoveredUI == null) return;

			//  click button
			hoveredUI.DoClick();

			//  disable input
			DisableInputFor(timeToggleInput);
		}

		public void SelectButton(UI3D ui)
		{
			//  unhover last button
			if (hoveredUI != null)
			{
				hoveredUI.IsHovered = false;
			}

			//  set hovered
			hoveredUI = ui;
			ui.IsHovered = true;
		}

		public void UnSelectButton()
		{
			if ( hoveredUI != null )
			{
				hoveredUI.IsHovered = false;
				hoveredUI = null;
			}
		}
		
		public void SelectNextButton() 
		{
			currentMenu.NextButton();
			SelectButton(currentMenu.CurrentButton);
		}
		public void SelectPreviousButton() 
		{
			currentMenu.PreviousButton();
			SelectButton(currentMenu.CurrentButton);
		}

		public void Back() => currentMenu.Back();

		private Vector2 GetMousePosition()
		{
			return Mouse.current.position.ReadValue();
		}

		void ControllerUpdate()
		{
			//  move buttons
			float axis = Input.GetAxis("UpDown");
			if (axis < 0.0f)
			{
				SelectNextButton();
				DisableInputFor(timeForControllerPress);
			}
			else if (axis > 0.0f)
			{
				SelectPreviousButton();
				DisableInputFor(timeForControllerPress);
			}

			//  enter
			if (Input.GetButtonDown("Submit"))
			{
				PressHovered();
			}
			//  back shortcut
			else if (Input.GetButtonDown("Cancel"))
			{
				Back();
			}
		}

		void Update()
		{
			if (isInputDisabled) return;

			/*if (isMouseControlled)
			{
				//  check for controller input
				float axis = Input.GetAxis("UpDown");
				if (axis != 0.0f)
				{
					isMouseControlled = false;
				}
			}
			else
			{
				//  check for mouse movement
				Vector3 mouse_pos = GetMousePosition();
				
				Vector3 mouse_delta = lastMousePos - mouse_pos;
				if (lastMousePos != Vector3.zero && mouse_delta.sqrMagnitude > 0)
				{
					isMouseControlled = true;
				}
				
				lastMousePos = mouse_pos;
			}

			if (!isMouseControlled)
			{
				ControllerUpdate();
			}*/
		
			MouseUpdate();
		}

		void OnDrawGizmos()
		{
			Gizmos.color = hitWasUI ? Color.green : Color.red;
			Gizmos.DrawLine(nearWorldPos, hitPos);
			Gizmos.DrawSphere(hitPos, 0.1f);
		}

		public void ShowMenu(MenuController menu)
		{
			currentMenu.UnSelect();
			menu.Select();
			currentMenu = menu;

			DisableInputFor(timeToggleInput);
		}

		IEnumerator CoroutineDisableInputFor(float time)
		{
			isInputDisabled = true;

			yield return new WaitForSeconds(time);

			isInputDisabled = false;
		}

		void DisableInputFor(float time)
		{
			StartCoroutine(CoroutineDisableInputFor(time));
		}
	}
}
