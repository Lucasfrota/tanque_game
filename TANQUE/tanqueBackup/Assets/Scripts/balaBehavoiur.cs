using UnityEngine;
using System.Collections;

public class balaBehavoiur : MonoBehaviour {

	public GameObject bala;
	public Transform mira;

	private Vector2 alvo;

	// Use this for initialization
	void Start () {
		alvo = mira.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Vector3.MoveTowards(transform.position, alvo,0.6f);//.Lerp(transform.position, mira.transform.position, Time.time)//+= transform.forward * 3 * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other){
		Destroy (bala);
	}
}
