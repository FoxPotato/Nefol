using UnityEngine;
using System.Collections;

public class drawLine : MonoBehaviour {

	private LineRenderer lineRenderer;
	private float counter;
	private float dist;

	public GameObject centerCube;
	private rotateondrag centerScript;

	public Transform[] stars;

	public float lineWidth = 0.1f;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetWidth (lineWidth, lineWidth);
		lineRenderer.SetVertexCount (stars.Length);

		centerCube = GameObject.Find ("Cube");
		centerScript = centerCube.GetComponent<rotateondrag> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!GameObject.Find ("Cube").GetComponent<rotateondrag> ().isCorrect) {
			lineRenderer.SetVertexCount (0);	
			return;
		} else
			lineRenderer.SetVertexCount (stars.Length);

		for(int i = 0; i < stars.Length; i++)
			lineRenderer.SetPosition (i, stars[i].position);
	}
}
