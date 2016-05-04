using UnityEngine;
using System.Collections;

public class rotateondrag : MonoBehaviour {

	float 	f_lastX 	= 0.0f;
	float 	f_difX 		= 0.5f;
	int 	i_directionX = 0;

	float 	f_lastY 	= 0.0f;
	float 	f_difY 		= 0.5f;
	int 	i_directionY = 0;

	Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			i_directionX = 0;
			i_directionY = 0;
			transform.rotation = startRotation;
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			f_difX = 0.0f;
			f_difY = 0.0f;
		}
		else if (Input.GetMouseButton (0)) {
			f_difX = Mathf.Abs (f_lastX - Input.GetAxis ("Mouse X"));

			if (f_lastX < Input.GetAxis ("Mouse X")) {
				i_directionX = -1;
				transform.Rotate(0, -f_difX, 0, Space.World);
			}

			if (f_lastX > Input.GetAxis ("Mouse X")) {
				i_directionX = 1;
				transform.Rotate(0, f_difX, 0, Space.World);
			}

			f_lastX = -Input.GetAxis ("Mouse X");



			f_difY = Mathf.Abs (f_lastY - Input.GetAxis ("Mouse Y"));

			if (f_lastY < Input.GetAxis ("Mouse Y")) {
				i_directionY = -1;
				transform.Rotate(f_difY, 0, 0, Space.World);
			}
			if (f_lastY > Input.GetAxis ("Mouse Y")) {
				i_directionY = 1;
				transform.Rotate(-f_difY, 0, 0, Space.World);
			}

			f_lastY = -Input.GetAxis ("Mouse Y");
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
