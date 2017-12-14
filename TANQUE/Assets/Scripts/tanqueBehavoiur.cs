using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tanqueBehavoiur : MonoBehaviour {

	public Text text;
	public Rigidbody2D bala;
	public GameObject balaAux;
	public GameObject tanque;
	public string horizontal;
	public string vertical;
	public string shoot;
	public string boost;
	public string look;
	public int numPlayer;
	public int vida;
	public float posicaoX;
	public float posicaoY;
	public float velocidade;
	public float velocidadeBoost;
	public float tempoBoost;
	public AudioSource somTiro;
	public AudioSource somMorte;
	public GameObject fade;
	public Text pauseText;
	public GameObject cabeca;
	public numeroBehaviour contadorVida;
	public contadorBehaviour contadorBoost;
	public GameObject layoutPause;
	public GameObject explosao;

	private int vidaAux;
	private float velocidadeAux;
	private float acumulaTempoBoost;

	private bool estaPausado;
	private bool recebeuBoost;
	private float contRecebeuBoost;
	static public bool estaPausadoAux;
	static public bool estaAcabado;

	private bool tanqueFoiMorto;
	private bool tanqueEstaPiscando;
	private float contTanquePiscando;
	private float tempoDeMorte;

	private float contExplosao;
	private Vector3 ultimaPosicao;

	// Use this for initialization
	void Start () {
		vidaAux = vida;
		velocidadeAux = velocidade;
		acumulaTempoBoost = tempoBoost;
		contRecebeuBoost = 0;
		estaPausado = false;
		estaAcabado = false;
		tanqueEstaPiscando = false;
		contTanquePiscando = 0;
		tanqueFoiMorto = false;
		tempoDeMorte = 0;
		contExplosao = 0;
		explosao.SetActive (false);
	}

	// Update is called once per frame
	void FixedUpdate () {
		estaPausadoAux = estaPausado;
		if(estaPausado == false && estaAcabado == false){

			float moveHorizontal = Input.GetAxis (horizontal);
			float moveVertical = Input.GetAxis (vertical);
			float turnBody = Input.GetAxis (look);

			cabeca.transform.Rotate (new Vector3(0, 0, turnBody)); 
			Vector2 movimente = new Vector2 (0, moveVertical);

			if (Input.GetButton (boost) && acumulaTempoBoost >= 0) {
				velocidade = velocidadeBoost;
				acumulaTempoBoost -= Time.deltaTime;
			} 
			else {
				velocidade = velocidadeAux;
			}

			transform.Translate(movimente*velocidade*Time.deltaTime);
			transform.Rotate (new Vector3 (0, 0, -moveHorizontal));

			if(Input.GetButtonDown(shoot)){
				atira();
			}
				
		}

		if(Input.GetButtonDown ("Start") && estaAcabado == false){
			if(estaPausado == false){
				estaPausado = true;
				fade.SetActive (true);
				layoutPause.SetActive(true);
			}
			else if(estaPausado == true){
				estaPausado = false;
				fade.SetActive (false);
				layoutPause.SetActive (false);
				pauseText.text = "";
			}
		}

		string vidaStr = vida.ToString ();
		if(vidaStr.Length == 1){
			vidaStr = "0" + vidaStr;
		}
		contadorVida.dezenaNum = int.Parse(vidaStr [0].ToString());
		contadorVida.unidadeNum = int.Parse(vidaStr [1].ToString());

		string boostStr = acumulaTempoBoost.ToString("N0");
		if(boostStr.Length == 1){
			boostStr = "00" + boostStr;//contadorBoost
		}else if(boostStr.Length == 2){
			boostStr = "0" + boostStr;
		}

		contadorBoost.dezenaMinutos = int.Parse(boostStr [0].ToString());
		contadorBoost.unidadeMinutos = int.Parse(boostStr [1].ToString());
		contadorBoost.dezenaSegundos = int.Parse(boostStr [2].ToString());
		contadorBoost.unidadeSegundos = 10;

		int pontos = 0;

		switch (numPlayer) {
		case 1:
			pontos = GameController.tanque1Matou;
			break;
		case 2:
			pontos = GameController.tanque2Matou;
			break;
		case 3:
			pontos = GameController.tanque3Matou;
			break;
		case 4:
			pontos = GameController.tanque4Matou;
			break;
		}

		text.text = "Player " + numPlayer + ":\n" + pontos.ToString ();

		if(recebeuBoost == true){
			contRecebeuBoost += Time.deltaTime;
			if(contRecebeuBoost >= 0.7f){
				recebeuBoost = false;
				contRecebeuBoost = 0;
			}
		}

		if(tanqueFoiMorto == true){
			contTanquePiscando += Time.deltaTime;
			//Debug.Log (tanqueEstaPiscando + " | " + contTanquePiscando);
			if(tanqueEstaPiscando == false && contTanquePiscando > 0.4f){
				tanque.transform.position = new Vector3 (tanque.transform.position.x, tanque.transform.position.y, -2);
				tanqueEstaPiscando = true;
				contTanquePiscando = 0;

			}else if(tanqueEstaPiscando == true && contTanquePiscando > 0.4f){
				tanque.transform.position = new Vector3 (tanque.transform.position.x, tanque.transform.position.y, 10);
				tanqueEstaPiscando = false;
				contTanquePiscando = 0;

			}
			tempoDeMorte += Time.deltaTime;
			if(tempoDeMorte >= 3){
				tanqueFoiMorto = false;
				tanqueEstaPiscando = false;
				contTanquePiscando = 0;
				tempoDeMorte = 0;
				if(tanque.transform.position != new Vector3 (tanque.transform.position.x, tanque.transform.position.y, -2)){
					tanque.transform.position = new Vector3 (tanque.transform.position.x, tanque.transform.position.y, -2);	
				}
			}

			if (contExplosao >= 2.6f) {
				explosao.SetActive (false);
				contExplosao = 0;
			} else {
				contExplosao += Time.deltaTime;
				explosao.transform.position = ultimaPosicao;
			}
		}
			
	}

	void atira(){
		somTiro.Play ();
		balaAux.SetActive (true);
		Instantiate (bala, bala.position, Quaternion.identity);
		balaAux.SetActive (false);
	}

	void levarDano(){
		if(tanqueFoiMorto == false){
			vida--;
			if(vida <= 0){
				somMorte.Play ();
				explosao.SetActive (true);
				ultimaPosicao = new Vector3 (tanque.transform.position.x, tanque.transform.position.y, -4);//explosao.transform.position = tanque.transform.position;
				tanque.transform.position=  (new Vector3(posicaoX, posicaoY, -2));
				vida = vidaAux;
				tanqueFoiMorto = true;
				tanqueEstaPiscando = false;
				tanque.transform.position = new Vector3 (tanque.transform.position.x, tanque.transform.position.y, 10);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(tanqueFoiMorto == false){
			switch (numPlayer) {
			case 1:
				if(other.gameObject.CompareTag("balaP2")){
					GameController.tanque2Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP3")){
					GameController.tanque3Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP4")){
					GameController.tanque4Matou++;
					levarDano ();
				}
				break;
			case 2:
				if(other.gameObject.CompareTag("balaP1")){
					GameController.tanque1Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP3")){
					GameController.tanque3Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP4")){
					GameController.tanque4Matou++;
					levarDano ();
				}
				break;
			case 3:
				if(other.gameObject.CompareTag("balaP1")){
					GameController.tanque1Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP2")){
					GameController.tanque2Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP4")){
					GameController.tanque4Matou++;
					levarDano ();
				}
				break;
			case 4:
				if(other.gameObject.CompareTag("balaP1")){
					GameController.tanque1Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP2")){
					GameController.tanque2Matou++;
					levarDano ();
				}
				else if(other.gameObject.CompareTag("balaP3")){
					GameController.tanque3Matou++;
					levarDano ();
				}
				break;
			}
		}

		if (other.gameObject.CompareTag ("up") && recebeuBoost == false && acumulaTempoBoost < 91) {
			acumulaTempoBoost += 5;
			recebeuBoost = true;
		}

	}
}
