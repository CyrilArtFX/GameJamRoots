using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class IntersectionRail : MonoBehaviour
{
	[Flags]
	public enum Axes
	{
		PositiveX = 1,
		NegativeX = 2,
		PositiveZ = 4,
		NegativeZ = 8,
	}

	[SerializeField]
	private Axes allowedAxes = Axes.PositiveX | Axes.NegativeX | Axes.PositiveZ | Axes.NegativeZ;

	[SerializeField]
	private Collider[] axesColliders;

	private Grabbable grabbable;

	public bool IsAllowingAxis( Axes axis )
	{
		return ( allowedAxes & axis ) == axis;
	}

	void Update()
	{
		if ( !grabbable ) return;

		print( grabbable + " " + grabbable.Rigidbody.velocity );
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

	void OnValidate()
	{
		if ( axesColliders == null || axesColliders.Length != 4 ) 
		{
			Debug.LogWarning( "IntersectionRail: please specify 4 axes colliders!" );
			return;
		}

		axesColliders[0].enabled = !IsAllowingAxis( Axes.PositiveX );
		axesColliders[1].enabled = !IsAllowingAxis( Axes.NegativeX );
		axesColliders[2].enabled = !IsAllowingAxis( Axes.PositiveZ );
		axesColliders[3].enabled = !IsAllowingAxis( Axes.NegativeZ );
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;

		//  draw allowed axes
		if ( IsAllowingAxis( Axes.PositiveX ) )
			GizmosPlus.DrawArrow( transform.position, Vector3.right );
		else 
			GizmosPlus.DrawWireCollider( axesColliders[0] );

		if ( IsAllowingAxis( Axes.NegativeX ) )
			GizmosPlus.DrawArrow( transform.position, -Vector3.right );
		else 
			GizmosPlus.DrawWireCollider( axesColliders[1] );

		if ( IsAllowingAxis( Axes.PositiveZ ) )
			GizmosPlus.DrawArrow( transform.position, Vector3.forward );
		else 
			GizmosPlus.DrawWireCollider( axesColliders[2] );

		if ( IsAllowingAxis( Axes.NegativeZ ) )
			GizmosPlus.DrawArrow( transform.position, -Vector3.forward );
		else 
			GizmosPlus.DrawWireCollider( axesColliders[3] );
	}
}
