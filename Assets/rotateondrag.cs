using UnityEngine;
using System.Collections;

public class rotateondrag : MonoBehaviour {

	float 	lastX 		= 0.0f;
	float 	difX 		= 0.5f;
	int 	directionX 	= 0;

	float 	lastY 		= 0.0f;
	float 	difY 		= 0.5f;
	int 	directionY 	= 0;

	float	extraSteps 	= 0.5f;

	Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			directionX = 0;
			directionY = 0;
			transform.rotation = startRotation;
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			difX = 0.0f;
			difY = 0.0f;
		}
		else if (Input.GetMouseButton (0)) {
			difX = Mathf.Abs (lastX - Input.GetAxis ("Mouse X"));

			if (lastX < Input.GetAxis ("Mouse X")) {
				directionX = -1;
				transform.Rotate(0, -difX - extraSteps, 0, Space.World);
			}

			if (lastX > Input.GetAxis ("Mouse X")) {
				directionX = 1;
				transform.Rotate(0, difX + extraSteps, 0, Space.World);
			}

			lastX = -Input.GetAxis ("Mouse X");



			difY = Mathf.Abs (lastY - Input.GetAxis ("Mouse Y"));

			if (lastY < Input.GetAxis ("Mouse Y")) {
				directionY = -1;
				transform.Rotate(difY + extraSteps, 0, 0, Space.World);
			}
			if (lastY > Input.GetAxis ("Mouse Y")) {
				directionY = 1;
				transform.Rotate(-difY - extraSteps, 0, 0, Space.World);
			}

			lastY = -Input.GetAxis ("Mouse Y");
		} 
		else 
		{
//			if (f_difX > 0.5f)
//				f_difX -= 0.05f;	
//			if (f_difX < 0.5f)
//				f_difX += 0.05f;
//		
//			transform.Rotate(0, f_difX * i_directionX, 0, Space.World);
//
//			if (f_difY > 0.5f)
//				f_difY -= 0.05f;	
//			if (f_difY < 0.5f)
//				f_difY += 0.05f;
//			
//			transform.Rotate(f_difY * i_directionY, 0, 0, Space.World);
		}

	}
}
