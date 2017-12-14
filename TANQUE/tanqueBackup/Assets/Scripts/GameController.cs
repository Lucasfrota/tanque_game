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

	static public float tempoPartida = 5;
	private float tempoPatidaAux;

	// Use this for initialization
	void Start () {
		if(tanque1Selecionado != true){
			Destroy (tanque1);//tanque1.SetActive (false);
		}
		if(tanque2Selecionado != true){
			Destroy (tanque2);//tanque2.SetActive (false);
		}
		if(tanque3Selecionado != true){
			Destroy (tanque3);//tanque3.SetActive (false);
		}
		if(tanque4Selecionado != true){
			Destroy (tanque4);//tanque4.SetActive (false);
		}
	
		tanque1Matou = 0;
		tanque2Matou = 0;
		tanque3Matou = 0;
		tanque4Matou = 0;

		resultado.text = "";

		tempoPatidaAux = tempoPartida;
	}
	
	// Update is called once per frame
	void Update () {

		if(tempoPartida < 0){
			tempo.gameObject.SetActive (false);
		}

		if (tempoPartida >= 0) {

			string tempoAux = (tempoPartida/60).ToString();
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

			tempoPartida -= Time.deltaTime;
		} else {
			tanqueBehavoiur.estaAcabado = true;
			if (camera.transform.position == new Vector3 (0, 50, -10)) {

				if(Input.GetButtonDown("Menu")){
					StartCoroutine (ChangeLevel ("selctionPlayer"));
				}

				if(Input.GetButtonDown("Restart")){
					StartCoroutine (ChangeLevel ("maze"));
					tempoPartida = tempoPatidaAux;
				}

				string saida = "";
				if (tanque1Selecionado == true) {
					saida += "Player 1: " + tanque1Matou.ToString () + "\n";
				}
				if (tanque2Selecionado == true) {
					saida += "Player 2: " + tanque2Matou.ToString () + "\n";
				}
				if (tanque3Selecionado == true) {
					saida += "Player 3: " + tanque3Matou.ToString () + "\n"; 
				}
				if (tanque4Selecionado == true) {
					saida += "Player 4: " + tanque4Matou.ToString () + "\n";
				}

				resultado.text = saida;

				if (tanque1Matou >= tanque2Matou && tanque1Matou >= tanque3Matou && tanque1Matou >= tanque4Matou) {
					conter.text = "Player 1 Venceu!";
				} else if (tanque2Matou >= tanque1Matou && tanque2Matou >= tanque3Matou && tanque2Matou >= tanque4Matou) {
					conter.text = "Player 2 Venceu!";
				} else if (tanque3Matou >= tanque1Matou && tanque3Matou >= tanque2Matou && tanque3Matou >= tanque4Matou) {
					conter.text = "Player 3 Venceu!";
				} else if (tanque4Matou >= tanque1Matou && tanque4Matou >= tanque2Matou && tanque4Matou >= tanque3Matou) {
					conter.text = "Player 4 Venceu!";
				}
			}
		}

		if(tanqueBehavoiur.estaAcabado == true){
			camera.transform.position = Vector3.MoveTowards (camera.transform.position, new Vector3(0, 50, -10), 5);
		}
	}

	void converteTempo(int dM, int uM, int dS, int uS){
		tempo.dezenaMinutos = dM;
		tempo.unidadeMinutos = uM;
		tempo.dezenaSegundos = dS;
		tempo.unidadeSegundos = uS;
	}

	IEnumerator ChangeLevel(string name){
		float fadeTime = GameObject.Find ("chaoTanqueNovo").GetComponent<Fading>().BeginFade (1);//Fading.BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel(name);
	}
}
