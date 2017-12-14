using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject tanque1;
	public GameObject tanque2;
	public GameObject tanque3;
	public GameObject tanque4;
	public contadorBehaviour tempo;
	public GameObject camera;
	public AudioSource somAmbiente;
	public AudioSource somFinal;

	public Text conter;
	public Text resultado;

	static public bool tanque1Selecionado;
	static public bool tanque2Selecionado;
	static public bool tanque3Selecionado;
	static public bool tanque4Selecionado;

	static public int tanque1Matou;
	static public int tanque2Matou;
	static public int tanque3Matou;
	static public int tanque4Matou;

	public GameObject vidaP1;
	public GameObject vidaP2;
	public GameObject vidaP3;
	public GameObject vidaP4;

	public GameObject caixasMenu;
	public GameObject layout2Jogadores;
	public GameObject layout3Jogadores;
	public GameObject layout4Jogadores;

	public contadorBehaviour painelUmP1;
	public contadorBehaviour painelUmP2;
	public tanquePainelBehaviour tPainelUmP1;
	public tanquePainelBehaviour tPainelUmP2;

	public contadorBehaviour painelDoisP1;
	public contadorBehaviour painelDoisP2;
	public contadorBehaviour painelDoisP3;
	public tanquePainelBehaviour tPainelDoisP1;
	public tanquePainelBehaviour tPainelDoisP2;
	public tanquePainelBehaviour tPainelDoisP3;

	public contadorBehaviour painelTresP1;
	public contadorBehaviour painelTresP2;
	public contadorBehaviour painelTresP3;
	public contadorBehaviour painelTresP4;

	public GameObject boostP1;
	public GameObject boostP2;
	public GameObject boostP3;
	public GameObject boostP4;

	static public float tempoPartida = 5;
	private float tempoPatidaAux;
	private int numeroJogadores;

	private float contadorPisca;
	private bool contEstaAtivado;
	private float contadorDoFinal;

	private ArrayList jogadores = new ArrayList();

	// Use this for initialization
	void Start () {

		jogadores.Add (1);
		jogadores.Add (2);
		jogadores.Add (3);
		jogadores.Add (4);

		numeroJogadores = menuSelectionController.numPlayersNoJogo;

		if(tanque1Selecionado == false){
			Destroy (tanque1);
			Destroy (vidaP1);
			Destroy (boostP1);
			jogadores.Remove (1);
		}
		if(tanque2Selecionado == false){
			Destroy (tanque2);
			Destroy (vidaP2);
			Destroy (boostP2);
			jogadores.Remove (2);
		}
		if(tanque3Selecionado == false){
			Destroy (tanque3);
			Destroy (vidaP3);
			Destroy (boostP3);
			jogadores.Remove (3);
		}
		if(tanque4Selecionado == false){
			Destroy (tanque4);
			Destroy (vidaP4);
			Destroy (boostP4);
			jogadores.Remove (4);
		}
	
		tanque1Matou = 0;
		tanque2Matou = 0;
		tanque3Matou = 0;
		tanque4Matou = 0;

		tempoPatidaAux = tempoPartida;
		contadorPisca = 0;
		contadorDoFinal = 0;
		contEstaAtivado = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (tanqueBehavoiur.estaPausadoAux == true || tanqueBehavoiur.estaAcabado == true) {
			camera.transform.position = Vector3.MoveTowards (camera.transform.position, new Vector3 (0, 50, -10), 5);
			somAmbiente.Pause ();
		} else if (tanqueBehavoiur.estaPausadoAux == false) {
			camera.transform.position = Vector3.MoveTowards (camera.transform.position, new Vector3 (0, 0.5f, -10), 5);
			somAmbiente.UnPause ();
			tempoPartida -= Time.deltaTime;
		}

		if (tempoPartida >= 0) {

			if(tempoPartida <= 15){
				contadorPisca += Time.deltaTime;
				if(contEstaAtivado == true && contadorPisca > 0.3f){
					tempo.gameObject.SetActive (false);
					contEstaAtivado = false;
					contadorPisca = 0;
				}else  if(contEstaAtivado == false && contadorPisca > 0.3f){
					tempo.gameObject.SetActive (true);
					contEstaAtivado = true;
					contadorPisca = 0;
				}



			}

			string tempoAux = (tempoPartida / 60).ToString ();
			string[] Aux = new string[2];
			Aux = tempoAux.Split ('.');

			string minutos = Aux [0];
			string segundos = "00";

			if (Aux.Length == 2) {
				float segundosAux = float.Parse ("0." + Aux [1]);
				segundos = (segundosAux * 60).ToString ();
				string[] segundosLista = segundos.Split ('.');
				segundos = segundosLista [0];
			}

			if (minutos.Length == 1) {
				minutos = "0" + minutos;
			}
			if (segundos.Length == 1) {
				segundos = "0" + segundos;
			}

			converteTempo (int.Parse (minutos [0].ToString ()), int.Parse (minutos [1].ToString ()), int.Parse (segundos [0].ToString ()), int.Parse (segundos [1].ToString ()));

		} else {
			tanqueBehavoiur.estaAcabado = true;
			tanqueBehavoiur.estaPausadoAux = true;
			somAmbiente.Pause ();
			somFinal.gameObject.SetActive (true);

			painelFim ();

			tempo.gameObject.SetActive (false);
			if (tanque1Selecionado == true) {
				Destroy (boostP1);
				Destroy (vidaP1);
			}
			if (tanque2Selecionado == true) {
				Destroy (boostP2);
				Destroy (vidaP2);
			}
			if (tanque3Selecionado == true) {
				Destroy (boostP3);
				Destroy (vidaP3);
			}
			if (tanque4Selecionado == true) {
				Destroy (boostP4);
				Destroy (vidaP4);
			}

			if(camera.transform.position == new Vector3 (0, 50, -10)){
				resultado.text = "Aperte (Triangulo) para voltar ao menu principal\nAperte (Quadrado) para jogar novamente";

				if (Input.GetButtonDown ("Menu") && contadorDoFinal >= 2) {
					tanque1Selecionado = false;
					tanque2Selecionado = false;
					tanque3Selecionado = false;
					tanque4Selecionado = false;
					StartCoroutine (ChangeLevel ("selctionPlayer"));
				} else if (Input.GetButtonDown ("Restart") && contadorDoFinal >= 2) {
					StartCoroutine (ChangeLevel ("maze"));
					tempoPartida = tempoPatidaAux;
				}

				contadorDoFinal += Time.deltaTime;
			}
		}
	}

	void converteTempo(int dM, int uM, int dS, int uS){
		tempo.dezenaMinutos = dM;
		tempo.unidadeMinutos = uM;
		tempo.dezenaSegundos = dS;
		tempo.unidadeSegundos = uS;
	}

	IEnumerator ChangeLevel(string name){
		float fadeTime = GameObject.Find ("chaoTanqueNovo").GetComponent<Fading>().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel(name);
	}

	void painelFim(){
		if(numeroJogadores == 2){
			layout2Jogadores.gameObject.SetActive (true);

			tPainelUmP1.num = int.Parse((jogadores [0]).ToString());
			ajustaPlacar (painelUmP1, int.Parse((jogadores [0]).ToString()));

			tPainelUmP2.num = int.Parse((jogadores [1]).ToString());
			ajustaPlacar (painelUmP2, int.Parse((jogadores [1]).ToString()));

		}else if(numeroJogadores == 3){
			layout3Jogadores.gameObject.SetActive (true);

			tPainelDoisP1.num = int.Parse ((jogadores[0]).ToString());
			ajustaPlacar (painelDoisP1, int.Parse((jogadores [0]).ToString()));

			tPainelDoisP2.num = int.Parse ((jogadores[1]).ToString());
			ajustaPlacar (painelDoisP2, int.Parse((jogadores [1]).ToString()));

			tPainelDoisP3.num = int.Parse ((jogadores[2]).ToString());
			ajustaPlacar (painelDoisP3, int.Parse((jogadores [2]).ToString()));

		}else if(numeroJogadores == 4){
			layout4Jogadores.gameObject.SetActive (true);

			ajustaPontuacao(painelTresP1, tanque1Matou);
			ajustaPontuacao(painelTresP2, tanque2Matou);
			ajustaPontuacao(painelTresP3, tanque3Matou);
			ajustaPontuacao(painelTresP4, tanque4Matou);

		}
	}

	string ajustaPontuacao(int pontuacao){
		string aux;
		aux = pontuacao.ToString ();
		if (aux.Length == 1) {
			aux = "000" + aux;
		} else if (aux.Length == 2) {
			aux = "00" + aux;
		} else if (aux.Length == 3) {
			aux = "0" + aux;
		} else {
			aux = "0000";
		}

		return aux;
	}

	int retornaInt (char c){
		return int.Parse (c.ToString());
	}
		
	void ajustaPontuacao(contadorBehaviour cb, int num){
		string pontuacao;
		pontuacao = ajustaPontuacao (num);
		cb.dezenaMinutos = retornaInt (pontuacao[0]);
		cb.unidadeMinutos = retornaInt (pontuacao[1]);
		cb.dezenaSegundos = retornaInt (pontuacao [2]);
		cb.unidadeSegundos = retornaInt (pontuacao [3]);
	}

	void ajustaPlacar(contadorBehaviour cb, int num){
		switch(num){
			case 1:
				ajustaPontuacao (cb, tanque1Matou);
				break;
			case 2:
				ajustaPontuacao(cb, tanque2Matou);
				break;
			case 3:
				ajustaPontuacao(cb, tanque3Matou);
				break;
			case 4:
				ajustaPontuacao(cb, tanque4Matou);
				break;
		}
	}
}
