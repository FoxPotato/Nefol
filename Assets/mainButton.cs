using UnityEngine;
using System.Collections;

public class mainButton : MonoBehaviour {

	public Transform TL;
	public Transform BR;

	RectTransform rect;
	float distancex, distancey;

	// Use this for initialization
	void Start () {

		distancex = Camera.main.WorldToScreenPoint (BR.position).x - Camera.main.WorldToScreenPoint (TL.position).x;
		distancey = Camera.main.WorldToScreenPoint (BR.position).y - Camera.main.WorldToScreenPoint (TL.position).y;

		if (distancex < 0)
			distancex = -distancex;
		if (distancey < 0)
			distancey = -distancey;

		rect = GetComponent<RectTransform> ();



	}
	
	// Update is called once per frame
	void Update () {

		rect.position = new Vector3(Camera.main.WorldToScreenPoint (TL.position).x + distancex/2, Camera.main.WorldToScreenPoint (TL.position).y - distancey / 2);
		rect.sizeDelta = new Vector2 (distancex, distancey);

		if (Input.GetMouseButtonDown (0)) {
			if (RectTransformUtility.RectangleContainsScreenPoint (rect, Input.mousePosition)) {
				System.Random rand = new System.Random ();
				int id = rand.Next (1, 3);
				UnityEngine.SceneManagement.SceneManager.LoadScene (id);
				Debug.Log(id);
			}
			
//			Debug.Log ("-------------");
//			Debug.Log (Input.mousePosition.x + " - " + Input.mousePosition.y);
//			Debug.Log (Camera.main.WorldToScreenPoint(TL.position).x + " - " + Camera.main.WorldToScreenPoint(TL.position).y);			
//			Debug.Log (distancex + " - " + distancey);
//			Debug.Log ("-------------");


		}
	}
}
