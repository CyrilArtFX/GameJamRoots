using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	public class MenuController : MonoBehaviour
	{
		public CinemachineVirtualCamera VirtualCamera => camera;

		public UI3D CurrentButton => (currentButtonID >= 0 && currentButtonID < buttons.Length) ? buttons[currentButtonID] : null;
		public UIButton3D[] buttons;

		public UI3D backButton;
		public UI3D[] showUIs;

		[SerializeField]
		private CinemachineVirtualCamera camera;

		private int currentButtonID = 0;

		void Start()
		{
			UnSelect();
		}

		public void NextButton()
		{
			SelectButtonByID((currentButtonID + 1) % buttons.Length);
		}

		public void PreviousButton()
		{
			SelectButtonByID(currentButtonID - 1 < 0 ? buttons.Length - 1 : currentButtonID - 1);
		}

		public virtual void Select()
		{
			foreach ( UI3D ui in showUIs )
			{
				ui.TargetAlpha = 1.0f;
			}

			camera.Priority = 11;
		}

		public virtual void UnSelect()
		{
			foreach ( UI3D ui in showUIs )
			{
				ui.TargetAlpha = 0.0f;
			}

			camera.Priority = 10;
		}

		public void Back()
		{
			if (backButton == null) return;

			backButton.DoClick();
		}

		public void SelectButtonByID(int id)
		{
			if (CurrentButton != null)
			{
				CurrentButton.IsHovered = false;
			}

			currentButtonID = id;
			CurrentButton.IsHovered = true;
		}
	}
}