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

		Vector3 player_dir = controller.LastMoveDirection;
		if ( Mathf.Abs( player_dir.x ) > Mathf.Abs( player_dir.z ) )
		{
			player_dir = new( player_dir.x, 0.0f, 0.0f );
		}
		else
		{
			player_dir = new( 0.0f, 0.0f, player_dir.z );
		}
		player_dir.Normalize();

		//print( Mathf.Round( player_dir.x ) + " " + 0.0f + " " + Mathf.Round( player_dir.y ) );
		
		grabbable.Move( player_dir, grabForce );
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