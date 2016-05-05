using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	public float rotationRate = 0.2f;

	void Update () {
		transform.Rotate (Vector3.up, Time.smoothDeltaTime * rotationRate);
	}
}
