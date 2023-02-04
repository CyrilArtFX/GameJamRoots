using System.Collections;
using UnityEngine;

namespace Utils
{
	public static class VectorPlus
	{
		public static Vector3 GetXZ( this Vector3 vector )
		{
			return new( vector.x, 0.0f, vector.z );
		}
	}
}