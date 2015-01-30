using UnityEngine;
using System.Collections;
using Leap;

public static class UnityMatrixExtension
{
		public static readonly Vector LEAP_UP = new Vector (0, 1, 0);
		public static readonly Vector LEAP_FORWARD = new Vector (0, 0, -1);
		public static readonly Vector LEAP_ORIGIN = new Vector (0, 0, 0);
	
		public static Quaternion Rotation (this Matrix matrix)
		{
				Vector3 up = matrix.TransformDirection (LEAP_UP).ToUnity ();
				Vector3 forward = matrix.TransformDirection (LEAP_FORWARD).ToUnity ();
				return Quaternion.LookRotation (forward, up);
		}
	
		public static Vector3 Translation (this Matrix matrix)
		{
				return matrix.TransformPoint (LEAP_ORIGIN).ToUnityScaled ();
		}
}
