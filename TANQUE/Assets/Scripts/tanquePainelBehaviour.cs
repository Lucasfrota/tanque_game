using UnityEngine;
using System.Collections;

public class tanquePainelBehaviour : MonoBehaviour {

	public GameObject azul;
	public GameObject verde;
	public GameObject vermelho;
	public GameObject amarelo;
	public int num;

	// Use this for initialization
	void Start () {
		tiraCor ();
		colocaCor ();
	}
	
	// Update is called once per frame
	void Update () {
		tiraCor ();
		colocaCor ();
	}

	void tiraCor(){
		azul.SetActive (false);
		verde.SetActive (false);
		vermelho.SetActive (false);
		amarelo.SetActive (false);
	}

	void colocaCor(){
		switch(num){
		case 1:
			azul.SetActive (true);
			break;
		case 2:
			verde.SetActive (true);
			break;
		case 3:
			vermelho.SetActive (true);
			break;
		case 4:
			amarelo.SetActive (true);
			break;
		}
	}
}
