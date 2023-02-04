using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using Utils;
using static Grabber;

public class Grabbable : MonoBehaviour
{
	public enum GrabAxis
	{
		LocalX,
		LocalZ,
		GlobalX,
		GlobalZ,
	}

	public Vector3 DesiredDirection { get; private set; }
	public Vector3 MovedDirection { get; private set; }
	public Vector3 MoveDirection { get; set; }
	public Rigidbody Rigidbody { get; private set; }

	[SerializeField]
	private GrabAxis grabAxis = GrabAxis.LocalX;

	void Awake()
	{
		MoveDirection = Vector3.right;
		Rigidbody = GetComponent<Rigidbody>(); 
	}
	
	public Vector3 GetGrabDirection()
	{
		switch ( grabAxis )
		{
			case GrabAxis.LocalX:
				return transform.right;
			case GrabAxis.LocalZ:
				return transform.forward;
			case GrabAxis.GlobalX:
				return Vector3.right;
			case GrabAxis.GlobalZ:
				return Vector3.forward;
		}

		return Vector3.zero;
	}

	public void Move( Vector3 desired_dir, float force )
	{
		Vector3 grab_dir = MoveDirection;//GetGrabDirection();

		float move_speed = force * Time.fixedDeltaTime * Vector3.Dot( grab_dir, desired_dir );
		Vector3 move_dir = move_speed * grab_dir;

		//  TODO: don't use physics (rigidbody + colliders)
		Rigidbody.MovePosition( Rigidbody.position + move_dir );

		DesiredDirection = desired_dir;
		MovedDirection = move_dir;
	}

	private void LateUpdate()
	{
		DesiredDirection = MovedDirection = Vector3.zero;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;

		Vector3 grab_dir = GetGrabDirection();
		GizmosPlus.DrawArrow( transform.position, grab_dir );
		GizmosPlus.DrawArrow( transform.position, -grab_dir );
	}
}