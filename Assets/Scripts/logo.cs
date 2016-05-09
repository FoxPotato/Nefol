using UnityEngine;
using System.Collections;

public class logo : MonoBehaviour {

	float 			lastX 		= 0.0f;
	float 			difX 		= 0.5f;

	float 			lastY 		= 0.0f;
	float 			difY 		= 0.5f;

	float			extraSteps 	= 0.1f;

	Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			transform.rotation = startRotation;
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			difX = 0.0f;
			difY = 0.0f; 
		}
		else if (Input.GetMouseButton (0)) {
			difX = Mathf.Abs (lastX - Input.GetAxis ("Mouse X"));

			if (lastX < Input.GetAxis ("Mouse X")) 
				transform.Rotate(0, -difX - extraSteps, 0, Space.World);

			if (lastX > Input.GetAxis ("Mouse X")) 
				transform.Rotate(0, difX + extraSteps, 0, Space.World);

			lastX = -Input.GetAxis ("Mouse X");

			difY = Mathf.Abs (lastY - Input.GetAxis ("Mouse Y"));

			if (lastY < Input.GetAxis ("Mouse Y"))
				transform.Rotate(difY + extraSteps, 0, 0, Space.World);

			if (lastY > Input.GetAxis ("Mouse Y"))
				transform.Rotate(-difY - extraSteps, 0, 0, Space.World);

			lastY = -Input.GetAxis ("Mouse Y");
		} 

	}
}
