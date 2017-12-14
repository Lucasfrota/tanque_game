using UnityEngine;
using System.Collections;

public class caixaFixaBehaviour : MonoBehaviour {

	public GameObject madeira;
	public GameObject rachadura1;
	public GameObject rachadura2;

	private int contTiro;

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("colide");
			if(other.gameObject.CompareTag("balaP1") || other.gameObject.CompareTag("balaP2") || other.gameObject.CompareTag("balaP3") || other.gameObject.CompareTag("balaP4")){
				Debug.Log ("entra");
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
					Destroy (madeira);
				}
				contTiro++;

		}
	}
}
