using UnityEngine;
using System.Collections;

public class rotateondrag : MonoBehaviour {

	public bool isCorrect = false;

	float 			lastX 		= 0.0f;
	float 			difX 		= 0.5f;

	float 			lastY 		= 0.0f;
	float 			difY 		= 0.5f;

	float			extraSteps 	= 0.1f;

	public float	margin 		= 4f;

	Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;

		System.Random rand = new System.Random ();
		transform.Rotate (rand.Next (50, 359), rand.Next (50, 359), rand.Next (50, 359));
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			System.Random rand = new System.Random ();
			transform.Rotate (rand.Next (50, 359), rand.Next (50, 359), rand.Next (50, 359));
		}

		if (Input.GetMouseButtonDown (0) && !isCorrect) 
		{
			difX = 0.0f;
			difY = 0.0f; 
		}
		else if (Input.GetMouseButton (0) && !isCorrect) {
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
		else 
		{
			
			if ((transform.rotation.eulerAngles.x < margin || transform.rotation.eulerAngles.x > (360f - margin)) &&
			   (transform.rotation.eulerAngles.y < margin || transform.rotation.eulerAngles.y > (360f - margin))) {
				isCorrect = true;
				transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.time * 0.0001f);
			}
			else
				isCorrect = false;

		}

	}
}
