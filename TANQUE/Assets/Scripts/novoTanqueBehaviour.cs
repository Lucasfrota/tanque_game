using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class novoTanqueBehaviour : MonoBehaviour {

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

	private int vidaAux;
	private float velocidadeAux;
	private float acumulaTempoBoost;

	private bool estaPausado;
	static public bool estaAcabado;


	// Use this for initialization
	void Start () {
		vidaAux = vida;
		velocidadeAux = velocidade;
		acumulaTempoBoost = tempoBoost;
		estaPausado = false;
		estaAcabado = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
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

		if(Input.GetButtonDown ("Start")){
			if(estaPausado == false){
				estaPausado = true;
				fade.SetActive (true);
				pauseText.text = "PAUSE!";
			}
			else if(estaPausado == true){
				estaPausado = false;
				fade.SetActive (false);
				pauseText.text = "";
			}
		}

		text.text = "Player " + numPlayer + "\n" + vida.ToString () + " | " + acumulaTempoBoost.ToString("N1");
	}

	void atira(){
		somTiro.Play ();
		balaAux.SetActive (true);
		Instantiate (bala, bala.position, Quaternion.identity);
		balaAux.SetActive (false);
	}

	void levarDano(){
		vida--;
		if(vida <= 0){
			somMorte.Play ();
			tanque.SetActive (false);
			tanque.transform.position=  (new Vector3(posicaoX, posicaoY));
			tanque.SetActive (true);
			vida = vidaAux;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
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
}
