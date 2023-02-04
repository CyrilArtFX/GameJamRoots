using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class IntersectionRail : MonoBehaviour
{
	[SerializeField]
	private IntersectionRail[] connectedRails;

	private Grabbable grabbable;


	void Update()
	{
		if ( !grabbable ) return;

		if ( grabbable.DesiredDirection == Vector3.zero ) return;

		float best_dot = -1.0f;
		IntersectionRail next_rail = null;
		foreach ( IntersectionRail rail in connectedRails )
		{
			Vector3 dir = ( rail.transform.position - transform.position ).normalized;
			float dot = Vector3.Dot( dir, grabbable.DesiredDirection );
			if ( dot > best_dot )
			{
				best_dot = dot;
				next_rail = rail;
			}
		}

		if ( next_rail == null ) return;
		if ( grabbable.NextRail == next_rail ) return;

		//grabbable.transform.position = new( transform.position.x, grabbable.transform.position.y, transform.position.z );
		grabbable.PreviousRail = this;
		grabbable.NextRail = next_rail;
	}

	void OnTriggerEnter( Collider collider )
	{
		if ( !collider.TryGetComponent( out grabbable ) ) return;
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
				Vector3 dir = rail.transform.position - transform.position;
				Vector3 dir_normalized = dir.normalized;
				//GizmosPlus.DrawArrow( transform.position, dir );

				Vector3 offset = new( dir_normalized.z * 0.1f, 0.0f, -dir_normalized.x * 0.1f );
				GizmosPlus.DrawArrow( transform.position + offset, dir );
			}
		}
	}
}
