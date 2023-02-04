using System.Collections;
using UnityEngine;

namespace Game
{
	public class ObscureArea : MonoBehaviour
	{
		[SerializeField]
		private float pushForce = 10.0f;
		[SerializeField]
		private Vector3 pushDirection = Vector3.forward;

		private new Collider collider;

		void Awake()
		{
			collider = GetComponent<Collider>();
		}

		void OnTriggerStay( Collider collider )
		{
			if ( !collider.TryGetComponent( out CharacterController controller ) ) return;

			Vector3 dir = pushDirection;//collider.transform.position - transform.position;
			float dist_ratio = dir.sqrMagnitude / collider.bounds.size.sqrMagnitude;
			controller.Move( Time.deltaTime * pushForce * dist_ratio * dir.normalized );
		}

		void OnValidate()
		{
			pushDirection = pushDirection.normalized;
		}

		void OnDrawGizmos()
		{
			if ( !collider ) Awake();

			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube( collider.bounds.center, collider.bounds.size );

			Gizmos.DrawLine( transform.position, transform.position + pushDirection );
		}
	}
}