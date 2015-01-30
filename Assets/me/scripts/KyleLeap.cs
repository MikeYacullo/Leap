using UnityEngine;
using System.Collections;
using Leap;

public class KyleLeap : MonoBehaviour
{

		Transform head, ribs, neck;
		Controller controller;
		// Use this for initialization
		void Start ()
		{
				controller = new Controller ();
				head = transform.Find ("Root/Ribs/Neck/Head");
				neck = transform.Find ("Root/Ribs/Neck");
				ribs = transform.Find ("Root/Ribs");
		}
	
		// Update is called once per frame
		void Update ()
		{
				Frame frame = controller.Frame ();
				HandList hands = frame.Hands;
				Hand hRight = frame.Hands.Rightmost;
				Leap.Vector handPosLeap = hRight.PalmPosition;
				handPosLeap = leapToWorld (handPosLeap, frame.InteractionBox);
				Leap.Vector handPosNorm = leapToNormalized (handPosLeap, frame.InteractionBox);
				
				float ribXmax = 38.0f;
				Debug.Log ("handposnorm.x" + handPosNorm.x);
				float ribXrot = (handPosNorm.x - 0.5f) * ribXmax;
				Debug.Log (ribXrot);
				ribs.transform.eulerAngles = new Vector3 (ribs.transform.eulerAngles.x, ribXrot, ribs.transform.eulerAngles.z);
				//ribs.transform.Rotate (ribXrot, 0, 0);
				//Vector3 handPos = new Vector3 (handPosLeap.x, handPosLeap.y, handPosLeap.z);
				//Vector3 lookPos = new Vector3 (handPos.x, ribs.position.y, ribs.position.z);
				//Debug.Log (handPos.x + "," + ribs.position.y + "," + ribs.position.z);
				//ribs.LookAt (lookPos);
		}
		
		Leap.Vector leapToWorld (Leap.Vector leapPoint, InteractionBox iBox)
		{
				Leap.Vector normalized = leapToNormalized (leapPoint, iBox);
				normalized += new Leap.Vector (0.5f, 0f, 0.5f);
				return normalized * 100.0f;
		}
		
		Leap.Vector leapToNormalized (Leap.Vector leapPoint, InteractionBox iBox)
		{
				leapPoint.z *= -1.0f;
				Leap.Vector normalized = iBox.NormalizePoint (leapPoint, true);
				return normalized;
		}
}
