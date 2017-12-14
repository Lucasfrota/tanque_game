using UnityEngine;
using System.Collections;

public class pickUpController : MonoBehaviour {

	public GameObject pickUp;

	private int numAlet;
	private float cont;
	private float x;
	private float y;
	private bool estaAtivo;

	// Use this for initialization
	void Start () {
		x = transform.position.x;
		y = transform.position.y;
		estaAtivo = false;
		cont = 0;
	}
	
	// Update is called once per frame
	void Update () {

		numAlet = Random.Range (0, 10000);
		if(numAlet == 1){
			estaAtivo = true;
		}

		if (estaAtivo == true) {
			transform.position = new Vector3 (x, y, -2);
			cont += Time.deltaTime;
			if(cont >= 5){
				estaAtivo = false;
				cont = 0;
			}
		} else {
			transform.position = new Vector3 (-117.2f, 22.4f, 6);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("tanqueP1") || other.gameObject.CompareTag ("tanqueP2") || other.gameObject.CompareTag ("tanqueP3") || other.gameObject.CompareTag ("tanqueP4")) {
			estaAtivo = false;
		}
	}
}