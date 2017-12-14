using UnityEngine;
using System.Collections;

public class numBehaviour : MonoBehaviour {

	public GameObject n0;
	public GameObject n1;
	public GameObject n2;
	public GameObject n3;
	public GameObject n4;
	public GameObject n5;
	public GameObject n6;
	public GameObject n7;
	public GameObject n8;
	public GameObject n9;

	public int num;

	// Use this for initialization
	void Start () {
		zeraTudo ();
		colocaNumero ();
	}
	
	// Update is called once per frame
	void Update () {
		zeraTudo ();
		colocaNumero();
	}

	void zeraTudo(){
		n0.SetActive (false);
		n1.SetActive (false);
		n2.SetActive (false);
		n3.SetActive (false);
		n4.SetActive (false);
		n5.SetActive (false);
		n6.SetActive (false);
		n7.SetActive (false);
		n8.SetActive (false);
		n9.SetActive (false);
	}

	void colocaNumero(){
		switch(num){
		case 0:
			n0.SetActive (true);
			break;
		case 1:
			n1.SetActive (true);
			break;
		case 2:
			n2.SetActive (true);
			break;
		case 3:
			n3.SetActive (true);
			break;
		case 4:
			n4.SetActive (true);
			break;
		case 5:
			n5.SetActive (true);
			break;
		case 6:
			n6.SetActive (true);
			break;
		case 7:
			n7.SetActive (true);
			break;
		case 8:
			n8.SetActive (true);
			break;
		case 9:
			n9.SetActive (true);
			break;
		}
	}
}
