using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class removestars : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {

		Collider[] hitColliders = Physics.OverlapSphere (GetComponent<Transform> ().position, 280f);
		for (int i = 0; i < hitColliders.Length; i++)
			if (!hitColliders [i].transform.IsChildOf (transform)) {
				Debug.Log ("true");
				DestroyImmediate (hitColliders [i].gameObject);
			} else {
				Debug.Log ("false");
			}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
