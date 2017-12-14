using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameraBehaviour : MonoBehaviour {

	public Text instrucoes;
	public Text duracaoText;
	public contadorBehaviour tempo;

	private Vector3 alvo;

	static public bool menuSelecionado;
	static public float duracao;
	static public bool foiAlterado;

	// Use this for initialization
	void Start () {
		alvo = new Vector3 (0, 22, -10);
		menuSelecionado = false;
		duracao = 30;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetButtonDown("Menu")){
			if (menuSelecionado == true) {
				menuSelecionado = false;
			} else {
				menuSelecionado = true;
			}
		}

		if(Input.GetButtonDown("Start")){
			menuSelecionado = false;
		}

		if (menuSelecionado == true) {
			alvo = new Vector3 (0, 22, -10);
			instrucoes.text = "";
		} else {
			alvo = new Vector3 (0, 0, -10);
			duracaoText.text = "";
		}
		transform.position = Vector3.MoveTowards (transform.position, alvo, 0.9f);

		string tempoAux = (duracao/60).ToString();
		string[] Aux = new string[2];
		Aux = tempoAux.Split ('.');

		string minutos = Aux [0];
		string segundos = "00";

		if(Aux.Length == 2){
			float segundosAux = float.Parse("0." + Aux [1]);
			segundos = (segundosAux*60).ToString();
			string[] segundosLista = segundos.Split('.');
			segundos = segundosLista [0];
		}

		if (minutos.Length == 1) {
			minutos = "0" + minutos;
		}
		if (segundos.Length == 1) {
			segundos = "0" + segundos;
		}
			
		converteTempo(int.Parse(minutos[0].ToString()), int.Parse(minutos[1].ToString()), int.Parse(segundos[0].ToString()), int.Parse(segundos[1].ToString()));

	}

	void converteTempo(int dM, int uM, int dS, int uS){
		tempo.dezenaMinutos = dM;
		tempo.unidadeMinutos = uM;
		tempo.dezenaSegundos = dS;
		tempo.unidadeSegundos = uS;
	}
}
