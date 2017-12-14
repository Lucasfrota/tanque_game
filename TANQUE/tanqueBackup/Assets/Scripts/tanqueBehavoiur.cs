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
	public int numPlayer;
	public int vida;
	public float posicaoX;
	public float posicaoY;
	public float velocidade;
	public float velocidadeBoost;
	public float tempoBoost;

	private int vidaAux;
	private float velocidadeAux;
	private float acumulaTempoBoost;


	// Use this for initialization
	void Start () {
		vidaAux = vida;
		velocidadeAux = velocidade;
		acumulaTempoBoost = tempoBoost;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis (horizontal);
		float moveVertical = Input.GetAxis (vertical);

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
			atira ();
		}

		text.text = "Player " + numPlayer + "\n" + vida.ToString () + " | " + acumulaTempoBoost.ToString("N1");
	}

	void atira(){
		balaAux.SetActive (true);
		Instantiate (bala, bala.position, Quaternion.identity);
		balaAux.SetActive (false);
	}

	void levarDano(){
		vida--;
		if(vida <= 0){
			tanque.SetActive (false);
			tanque.transform.position=  (new Vector3(posicaoX, posicaoY));
			tanque.SetActive (true);
			vida = vidaAux;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		switch (numPlayer) {
		case 1:
			if(other.gameObject.CompareTag("balaP2") || other.gameObject.CompareTag("balaP3") || other.gameObject.CompareTag("balaP4")){
				levarDano ();
			}
			break;
		case 2:
			if (other.gameObject.CompareTag ("balaP1") || other.gameObject.CompareTag ("balaP3") || other.gameObject.CompareTag ("balaP4")) {
				levarDano ();
			}
			break;
		case 3:
			if (other.gameObject.CompareTag ("balaP1") || other.gameObject.CompareTag ("balaP2") || other.gameObject.CompareTag ("balaP4")) {
				levarDano ();
			}
			break;
		case 4:
			if (other.gameObject.CompareTag ("balaP1") || other.gameObject.CompareTag ("balaP2") || other.gameObject.CompareTag ("balaP3")) {
				levarDano ();
			}
			break;
		}
	}
}
