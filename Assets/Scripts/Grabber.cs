using StarterAssets;
using System;
using System.Collections;
using UI;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	public bool IsCurrentlyGrabbing => grabbable != null && controller.Input.isGrabbing;
	public Grabbable Grabbable => grabbable;

	[SerializeField]
	private float lostGrabTime = 0.2f;

	private Grabbable grabbable;

	private FirstPersonController controller;
	private Coroutine resetGrabCoroutine;

	void Awake()
	{
		controller = GetComponent<FirstPersonController>();
	}

	void FixedUpdate()
	{
		//  grab icon
		if (GrabIconUI.Instance != null)
		{
			GrabIconUI.Instance.IsShown = grabbable != null;
			GrabIconUI.Instance.SetGrabbing(controller.Input.isGrabbing);
			GrabIconUI.Instance.Target = grabbable != null ? grabbable.transform : null;
		}

		if (!IsCurrentlyGrabbing) return;
		if (!grabbable.CanPlayerControl) return;

		Vector3 player_dir = controller.LastMoveDirection;
		player_dir.y = 0.0f;
		player_dir.Normalize();

		grabbable.Move( player_dir );
	}

	void OnTriggerStay( Collider collider )
	{
		if ( grabbable != null ) return;
		if ( !collider.TryGetComponent( out grabbable ) ) return;

		if ( resetGrabCoroutine == null ) return;
		StopCoroutine( resetGrabCoroutine );
		resetGrabCoroutine = null;
	}

	void OnTriggerExit( Collider collider )
	{
		if ( !collider.TryGetComponent( out Grabbable new_grabbable ) || new_grabbable != grabbable ) return;

		if ( resetGrabCoroutine != null ) return;
		resetGrabCoroutine = StartCoroutine( CoroutineResetGrab() );
	}

	IEnumerator CoroutineResetGrab()
	{
		yield return new WaitForSeconds( lostGrabTime );

		grabbable = null;
		resetGrabCoroutine = null;
	}
}