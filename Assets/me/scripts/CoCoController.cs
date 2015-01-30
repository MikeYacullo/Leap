using UnityEngine;
using System.Collections;
using Leap;

public class CoCoController : MonoBehaviour
{

	Controller controller;
		
	Transform spine1, neck, jaw;
	
	void Start ()
	{
		controller = new Controller ();
				
		spine1 = transform.Find ("RigCoco/DEF_hips/DEF_spine1");
		neck = transform.Find ("RigCoco/DEF_hips/DEF_spine1/DEF_spine2/DEF_spine3/DEF_neck");
		jaw = transform.Find ("RigCoco/DEF_hips/DEF_spine1/DEF_spine2/DEF_spine3/DEF_neck/DEF_jaw1/DEF_jaw2");
	}
	
	void Update ()
	{
		Frame frame = controller.Frame ();
		HandList hands = frame.Hands;
		;
		//require both hands 
		if (hands.Count > 1) {
			//right hand to control spine rotation
			Hand rHand = controller.Frame ().Hands.Rightmost;
			Quaternion palm_rotation = rHand.Basis.Rotation ();
			Debug.Log ("X rotation component of the palm is: " + palm_rotation.x);
			Debug.Log ("Y rotation component of the palm is: " + palm_rotation.y);
			Debug.Log ("Z rotation component of the palm is: " + palm_rotation.z);
			float rot;
			rot = Mathf.Clamp (palm_rotation.y * 90.0f, -30.0f, 30.0f);
			Debug.Log (rot);
			spine1.transform.localEulerAngles = new Vector3 (rot, spine1.transform.localEulerAngles.y, spine1.transform.localEulerAngles.z);
			
			//left hand to control neck turn
			Hand lHand = controller.Frame ().Hands.Leftmost;
			palm_rotation = lHand.Basis.Rotation ();
			float yRot = Mathf.Clamp (palm_rotation.x * 108 + 20, -30.0f, 30.0f);
			float xRot = Mathf.Clamp (palm_rotation.y * 90.0f, -30.0f, 30.0f);
			neck.transform.localEulerAngles = new Vector3 (xRot, yRot, neck.transform.localEulerAngles.z);
			
			
			//left hand pinch to control jaw
			//jaw rotation: 20 is closed 66 is fully open
			float pinch = lHand.PinchStrength;
			float jRot = 66.0f - (46.0f * pinch);
			jaw.transform.localEulerAngles = new Vector3 (jaw.transform.localEulerAngles.x, jRot, jaw.transform.localEulerAngles.z);
		}
	}
}
