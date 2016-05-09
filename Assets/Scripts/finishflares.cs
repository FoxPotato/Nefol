using UnityEngine;
using System.Collections;

public class finishflares : MonoBehaviour {

	private rotateondrag centerScript;

	// Use this for initialization
	void Start () {
		centerScript = GetComponent<rotateondrag> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (((transform.rotation.eulerAngles.x < 1.5f || transform.rotation.eulerAngles.x > 358.5f) &&
		    (transform.rotation.eulerAngles.y < 1.5f || transform.rotation.eulerAngles.y > 358.5f) &&
		    (transform.rotation.eulerAngles.z < 1.5f || transform.rotation.eulerAngles.z > 358.5f)) && centerScript.isCorrect) {
			
			Light[] stars = GetComponentsInChildren<Light> ();

			foreach (Light star in stars) {
				float intense = Mathf.PingPong (Time.time, 1f);
				star.intensity = intense;
			}
			
		}

	}
}
