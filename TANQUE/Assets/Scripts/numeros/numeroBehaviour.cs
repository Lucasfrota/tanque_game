using UnityEngine;
using System.Collections;

public class numeroBehaviour : MonoBehaviour {

	public numBehaviour dezena;
	public numBehaviour unidade;

	public int dezenaNum;
	public int unidadeNum;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		dezena.num = dezenaNum;
		unidade.num = unidadeNum;
	}

	public void setNumeros(int dezena, int unidade){
		this.dezena.num = dezena;
		this.unidade.num = unidade;
	}
}
