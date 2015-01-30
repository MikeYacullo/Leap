using UnityEngine;
using System.Collections;
using Leap;
public class LookAt : MonoBehaviour
{
		public Controller controller;
		
		public GameObject target;
		// Use this for initialization
		void Start ()
		{
				controller = new Controller ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				Frame frame = controller.Frame ();
				HandList hands = frame.Hands;
				Hand hRight = frame.Hands.Rightmost;
				Vector3 handPos = new Vector3 (hRight.PalmPosition.x, hRight.PalmPosition.y, -hRight.PalmPosition.z);
				transform.LookAt (handPos);
				//transform.rotation = Quaternion.FromToRotation (Vector3.forward, (transform.position - handPos).normalized);
		}
}
