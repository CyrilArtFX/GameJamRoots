using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Grabbable : MonoBehaviour
{
	public bool CanPlayerControl { get; set; }
	public Vector3 DesiredDirection { get; private set; }
	public Vector3 MoveDirection { get; set; }
	public Rotable Rotable => rotable;

	public IntersectionRail PreviousRail, NextRail;

	[SerializeField]
	private float moveSpeed = 2.0f;
	[SerializeField]
	private LayerMask obstacleLayerMask;
	[SerializeField]
	private Rotable rotable;

	private new Collider collider;
	private new Rigidbody rigidbody;

	void Awake()
	{
		CanPlayerControl = true;
		MoveDirection = Vector3.right;

		rigidbody = GetComponent<Rigidbody>(); 
		collider = GetComponent<Collider>();
	}

	public void Move( Vector3 desired_dir )
	{
		if ( desired_dir == Vector3.zero ) 
		{
			DesiredDirection = Vector3.zero;
			return;
		};

		//  get target position
		Vector3 target = NextRail.CenterPos.position;
		if ( Vector3.Dot( NextRail.CenterPos.position - transform.position, desired_dir ) < 0.0f )
		{
			target = PreviousRail.CenterPos.position;
		}
		target.y = transform.position.y;

		//  get move speed & next position
		float move_speed = moveSpeed * Time.deltaTime;
		Vector3 new_position = Vector3.MoveTowards( rigidbody.position, target, move_speed );

		//  check collision first
		foreach ( Collider collider in Physics.OverlapBox( new_position, collider.bounds.extents, transform.rotation, obstacleLayerMask ) )
		{
			if ( collider == this.collider ) continue;
			return;
		}

		//  apply
		rigidbody.MovePosition( new_position );

		DesiredDirection = desired_dir;
	}

	void OnDrawGizmos()
	{
		if ( PreviousRail != null )
		{
			Gizmos.color = Color.cyan;
			Vector3 dir = PreviousRail.CenterPos.position - transform.position;
			dir.y = 0.0f;
			GizmosPlus.DrawArrow( transform.position, dir );
		}
		if ( NextRail != null )
		{
			Gizmos.color = Color.yellow;
			Vector3 dir = NextRail.CenterPos.position - transform.position;
			dir.y = 0.0f;
			GizmosPlus.DrawArrow( transform.position, dir );
		}

		Gizmos.color = Color.green;
		GizmosPlus.DrawArrow( transform.position, DesiredDirection );
	}
}