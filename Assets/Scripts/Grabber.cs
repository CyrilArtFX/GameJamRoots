using StarterAssets;
using System;
using System.Collections;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	[SerializeField]
	private float grabForce = 5.0f;
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
		if ( !grabbable || !controller.Input.isGrabbing ) return;

		/*Vector3 move_dir = controller.LastMoveDirection.normalized;
		if ( Mathf.Abs( Mathf.Abs( move_dir.x ) - Mathf.Abs( move_dir.z ) ) <= 0.2f ) return;
		if ( Mathf.Abs( move_dir.x ) > Mathf.Abs( move_dir.z ) )
		{
			move_dir = new( move_dir.x, 0.0f, 0.0f );
		}
		else
		{
			move_dir = new( 0.0f, 0.0f, move_dir.z );
		}*/

		Vector3 player_dir = controller.LastMoveDirection.normalized;

		//print( Mathf.Round( player_dir.x ) + " " + 0.0f + " " + Mathf.Round( player_dir.y ) );
		
		Vector3 grab_dir = grabbable.GetGrabDirection();

		float move_speed = grabForce * Time.fixedDeltaTime * Vector3.Dot( grab_dir, controller.LastMoveDirection.normalized );
		Vector3 move_dir = move_speed * grab_dir;

		grabbable.Rigidbody.MovePosition( grabbable.Rigidbody.position + move_dir );
	}

	void OnTriggerEnter( Collider collider )
	{
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