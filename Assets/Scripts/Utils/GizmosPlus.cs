using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
	public static class GizmosPlus
	{
		public const float ARROW_HEAD_ANGLE = 30.0f;
		public const float ARROW_HEAD_LENGTH = 0.25f;

		public static void DrawArrow( Vector3 pos, Vector3 dir )
		{
			Gizmos.DrawRay( pos, dir );

			Vector3 right = ( Quaternion.LookRotation( dir ) * Quaternion.Euler( ARROW_HEAD_ANGLE, 0.0f, 0.0f ) * Vector3.back ) * ARROW_HEAD_LENGTH ;
			Vector3 left = ( Quaternion.LookRotation( dir ) * Quaternion.Euler( -ARROW_HEAD_ANGLE, 0.0f, 0.0f ) * Vector3.back ) * ARROW_HEAD_LENGTH ;
		
			Gizmos.DrawRay( pos + dir, right );
			Gizmos.DrawRay( pos + dir, left );
		}

		public static void DrawWireCollider( Collider collider )
		{
			Gizmos.DrawWireCube( collider.bounds.center, collider.bounds.size );
		}
	}
}