using UnityEngine;
using System.Collections;

public class caixaVoidBehaviour : MonoBehaviour {

	public GameObject vazio;
	public GameObject madeira;
	public GameObject metal;
	public GameObject rachadura1;
	public GameObject rachadura2;
	public GameObject pickUp;

	private int numAlet;
	private bool ehMadeira;
	private int contTiro;

	// Use this for initialization
	void Start () {

		madeira.SetActive (false);
		metal.SetActive (false);
		rachadura1.SetActive (false);
		rachadura2.SetActive (false);

		contTiro = 0;

		numAlet = Random.Range (0, 10);
		if (numAlet > 8) {
			metal.SetActive (true);
		} else if (numAlet > 2 && numAlet < 8) {
			//pickUp.SetActive (false);
			Instantiate (pickUp, transform.position, Quaternion.identity);
			//pickUp.SetActive (false);
			Destroy (vazio);
		} else if (numAlet <= 2) {
			madeira.SetActive (true);
			ehMadeira = true;
		}
		else {
			vazio.SetActive (false);
		}
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
				if(contTiro == 4){
					madeira.SetActive (false);
					rachadura2.SetActive (false);
					Destroy (vazio);
				}
				contTiro++;
			}
		}
	}
}
