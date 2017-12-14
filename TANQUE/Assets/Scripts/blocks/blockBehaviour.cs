using UnityEngine;
using System.Collections;

public class blockBehaviour : MonoBehaviour {

	public GameObject madeira;
	public GameObject metal;
	public GameObject rachadura1;
	public GameObject rachadura2;

	private int numAlet;
	private bool ehMadeira;
	private int contTiro;

	// Use this for initialization
	void Start () {
		contTiro = 0;

		numAlet = Random.Range (0, 10);
		if(numAlet >= 8){
			metal.SetActive(true);
		}
		else if (numAlet > 2 && numAlet < 8) {
			//madeira.SetActive (false);
			transform.position = new Vector3(0, 0, 10);
		} else {
			//madeira.SetActive (true);
			ehMadeira = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(ehMadeira == true){
			if(other.gameObject.CompareTag("balaP1") || other.gameObject.CompareTag("balaP2") || other.gameObject.CompareTag("balaP3") || other.gameObject.CompareTag("balaP4")){
				if(contTiro == 0){
					rachadura1.SetActive (true);
				}
				if (contTiro == 2) {
					rachadura1.SetActive (false);
					rachadura2.SetActive (true);
				}
				if(contTiro == 1){
					madeira.SetActive (false);
					transform.position = new Vector3 (0, 0, 10);
				}
				contTiro++;
			}
		}
	}
}
