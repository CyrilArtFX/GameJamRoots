using System.Collections;
using UnityEngine;
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

	public Rigidbody Rigidbody { get; private set; }

	[SerializeField]
	private GrabAxis grabAxis = GrabAxis.LocalX;

	void Awake()
	{
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

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;

		Vector3 grab_dir = GetGrabDirection();
		GizmosPlus.DrawArrow( transform.position, grab_dir );
		GizmosPlus.DrawArrow( transform.position, -grab_dir );
	}
}