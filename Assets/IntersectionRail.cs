using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class IntersectionRail : MonoBehaviour
{
	[SerializeField]
	private IntersectionRail[] connectedRails;

	[SerializeField]
	private Transform center;

	public Transform CenterPos => center;

	private Grabbable grabbable;

	private bool canSwitchDirection = false;

	void FixedUpdate()
	{
		if ( !grabbable ) return;

		if ( !canSwitchDirection )
		{
			SnapToPosition();
			return;
		}

		GrabbableSwitchDesiredDirection();
	}

	private void GrabbableSwitchDesiredDirection()
	{
		if ( grabbable.DesiredDirection == Vector3.zero ) return;

		//  get most desired direction to follow
		float best_dot = -1.0f;
		IntersectionRail next_rail = null;
		foreach ( IntersectionRail rail in connectedRails )
		{
			Vector3 dir = ( rail.center.position - center.position ).normalized;
			float dot = Vector3.Dot( dir, grabbable.DesiredDirection );
			if ( dot > best_dot )
			{
				best_dot = dot;
				next_rail = rail;
			}
		}

		//  switch rails
		if ( next_rail == null || grabbable.NextRail == next_rail ) return;

		grabbable.PreviousRail = this;
		grabbable.NextRail = next_rail;
	}

	private void SnapToPosition()
	{
		grabbable.Move( ( center.position - grabbable.transform.position ).normalized );

		if ( VectorPlus.GetXZ( grabbable.transform.position - center.position ).sqrMagnitude <= 0.01f )
		{
			grabbable.transform.position = center.position.GetXZ();
			canSwitchDirection = true;
			grabbable.CanPlayerControl = true;
		}
	}

	void OnTriggerStay( Collider collider )
	{
		if ( grabbable != null ) return;
		if ( !collider.TryGetComponent( out grabbable ) ) return;

		canSwitchDirection = false;
		grabbable.CanPlayerControl = false;
	}

	void OnTriggerExit( Collider collider )
	{
		if ( !collider.TryGetComponent( out Grabbable new_grabbable ) || grabbable != new_grabbable ) return;

		grabbable = null;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;

		if ( connectedRails != null )
		{
			foreach ( IntersectionRail rail in connectedRails )
			{
				Vector3 dir = rail.center.position - center.position;
				Vector3 dir_normalized = dir.normalized;
				//GizmosPlus.DrawArrow( transform.position, dir );

				Vector3 offset = new( dir_normalized.z * 0.1f, 0.0f, -dir_normalized.x * 0.1f );
				GizmosPlus.DrawArrow(center.position + offset, dir );
			}
		}
	}
}
