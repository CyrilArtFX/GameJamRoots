using UnityEngine;
using Utils;

public class Grabbable : MonoBehaviour
{
	public bool CanPlayerControl { get; set; }
	public Vector3 DesiredDirection { get; private set; }
	public Vector3 MoveDirection { get; set; }
	public Rigidbody Rigidbody { get; private set; }

	public IntersectionRail PreviousRail, NextRail;

	[SerializeField]
	private float moveSpeed = 2.0f;

	void Awake()
	{
		CanPlayerControl = true;
		MoveDirection = Vector3.right;
		Rigidbody = GetComponent<Rigidbody>(); 
	}

	public void Move( Vector3 desired_dir )
	{
		if ( desired_dir == Vector3.zero ) 
		{
			DesiredDirection = Vector3.zero;
			return;
		};

		Vector3 target = NextRail.transform.position;
		if ( Vector3.Dot( NextRail.transform.position - transform.position, desired_dir ) < 0.0f )
		{
			target = PreviousRail.transform.position;
		}
		target.y = transform.position.y;

		float move_speed = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards( transform.position, target, move_speed );

		DesiredDirection = desired_dir;
	}

	void OnDrawGizmos()
	{
		if ( PreviousRail != null )
		{
			Gizmos.color = Color.cyan;
			Vector3 dir = PreviousRail.transform.position - transform.position;
			dir.y = 0.0f;
			GizmosPlus.DrawArrow( transform.position, dir );
		}
		if ( NextRail != null )
		{
			Gizmos.color = Color.yellow;
			Vector3 dir = NextRail.transform.position - transform.position;
			dir.y = 0.0f;
			GizmosPlus.DrawArrow( transform.position, dir );
		}

		Gizmos.color = Color.green;
		GizmosPlus.DrawArrow( transform.position, DesiredDirection );
	}
}