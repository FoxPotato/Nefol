using UnityEngine;
using System.Collections;

public class drawLine : MonoBehaviour {

	private LineRenderer lineRenderer;
	private float counter;
	private float dist;

	public bool checkRot = true;
	public Material material;
	public GameObject centerCube;
	private rotateondrag centerScript;

	GameObject[] lineRenderers;
	public Transform[] stars;


	public float lineWidth = 0.1f;

	// Use this for initialization
	void Start () {
		lineRenderers = new GameObject [stars.Length-1];

		for(int i = 0; i < lineRenderers.Length; i++){
			lineRenderers[i] = new GameObject();
			lineRenderers[i].AddComponent<LineRenderer>();
			LineRenderer comp = lineRenderers [i].GetComponent<LineRenderer> ();
			comp.SetWidth (lineWidth, lineWidth);
			comp.SetColors (Color.white, Color.white);
			comp.material = material;
			lineRenderers [i].name = "["+ i +"]";
		}

		if (checkRot) 
			centerScript = centerCube.GetComponent<rotateondrag> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (checkRot && !centerScript.isCorrect) {
			for(int i = 0; i < lineRenderers.Length; i++)
				lineRenderers[i].GetComponent<LineRenderer>().SetVertexCount(0);	
			return;
		} else
			for(int i = 0; i < lineRenderers.Length; i++)
				lineRenderers[i].GetComponent<LineRenderer>().SetVertexCount(2);	

		for (int i = 0; i < lineRenderers.Length; i++) {
			if (lineRenderers [i] == null)
				continue;
			lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (0, stars [i].position);
			lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (1, stars [i+1].position);
		}
		
	}
}
