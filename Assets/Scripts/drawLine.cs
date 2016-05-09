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
	void Awake () {
		lineRenderers = new GameObject [stars.Length-1];

		material.SetColor ("_EmissionColor", new Color (0.2f, 0.2f, 0.2f));

		for(int i = 0; i < lineRenderers.Length; i++){
			lineRenderers[i] = new GameObject();
			lineRenderers[i].AddComponent<LineRenderer>();
			LineRenderer comp = lineRenderers [i].GetComponent<LineRenderer> ();
			comp.SetWidth (lineWidth, lineWidth);
			comp.SetColors (new Color (0.2f, 0.2f, 0.2f), new Color (0.2f, 0.2f, 0.2f));
			comp.material = material;
			lineRenderers [i].name = "["+ i +"]";
		}

		if (checkRot) 
			centerScript = centerCube.GetComponent<rotateondrag> ();
		
		for (int i = 0; i < lineRenderers.Length; i++) {
			if (lineRenderers [i] == null)
				continue;
			lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (0, stars [i].position);
			lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (1, stars [i+1].position);
		}
	}
	
	// Update is called once per frame
	void Update () {

//		for (int i = 0; i < lineRenderers.Length; i++) {
//			if (lineRenderers [i] == null)
//				continue;
//			lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (0, stars [i].position);
//			lineRenderers [i].GetComponent<LineRenderer> ().SetPosition (1, stars [i+1].position);
//		}

		if (!checkRot || ((centerCube.transform.rotation.eulerAngles.x < 1.5f || centerCube.transform.rotation.eulerAngles.x > 358.5f) && 
							(centerCube.transform.rotation.eulerAngles.y < 1.5f || centerCube.transform.rotation.eulerAngles.y > 358.5f) && 
			(centerCube.transform.rotation.eulerAngles.z < 1.5f || centerCube.transform.rotation.eulerAngles.z > 358.5f) ) && centerScript.isCorrect)
			material.SetColor ("_EmissionColor", Color.white);
		
	}
}
