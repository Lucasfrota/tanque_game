using UnityEngine;
using System.Collections;

public class contadorBehaviour : MonoBehaviour {

	public numeroBehaviour minutos;
	public numeroBehaviour segundos;

	public int dezenaMinutos;
	public int unidadeMinutos;
	public int dezenaSegundos;
	public int unidadeSegundos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		setHora (dezenaMinutos, unidadeMinutos, dezenaSegundos, unidadeSegundos);
	}

	//funcao nao funciona...
	void setHora(int dezenaMinutos, int unidadeMinutos, int dezenaSegundos, int unidadeSegundos){
		minutos.dezenaNum = dezenaMinutos;
		minutos.unidadeNum = unidadeMinutos;
		segundos.dezenaNum = dezenaSegundos;
		segundos.unidadeNum = unidadeSegundos;
	}
}
