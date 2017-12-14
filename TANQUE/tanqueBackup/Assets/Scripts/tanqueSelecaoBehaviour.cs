using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tanqueSelecaoBehaviour : MonoBehaviour {

	public string horizontal;
	public string vertical;
	public string shoot;
	public string boost;
	public int numPlayer;
	public AudioSource somTiro;
	public Text text;
	public GameObject cobertura;

	private bool estaAtivado;

	private float velocidade;
	private float velocidadeAux;
	private float velocidadeBoost;

	private bool foiAlterado;

	// Use this for initialization
	void Start () {
		estaAtivado = false;
		velocidade = 3;
		velocidadeBoost = 10;
		velocidadeAux = velocidade;
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis (horizontal);
		float moveVertical = Input.GetAxis (vertical);

		if(cameraBehaviour.menuSelecionado == false){
			if(Input.GetButtonDown(shoot)){
				if(estaAtivado == false){
					cobertura.SetActive (false);
					somTiro.Play ();
					menuSelectionController.numPlayersNoJogo++;
				}

				switch(numPlayer){
				case 1:
					GameController.tanque1Selecionado = true;
					break;
				case 2:
					GameController.tanque2Selecionado = true;
					break;
				case 3:
					GameController.tanque3Selecionado = true;
					break;
				case 4:
					GameController.tanque4Selecionado = true;
					break;
				}
				estaAtivado = true;
			}

			if(estaAtivado == true){

				Vector2 movimente = new Vector2 (0, moveVertical);

				if (Input.GetButton (boost)) {
					velocidade = velocidadeBoost;
				} 
				else {
					velocidade = velocidadeAux;
				}

				transform.Translate(movimente*velocidade*Time.deltaTime);
				transform.Rotate (new Vector3 (0, 0, -moveHorizontal));
			}
		}

		if(cameraBehaviour.menuSelecionado == true){
			if (moveHorizontal == 1 && foiAlterado == false && cameraBehaviour.duracao < 1800) {
				cameraBehaviour.duracao += 30;
				foiAlterado = true;
			}else if(moveHorizontal == -1 && foiAlterado == false && cameraBehaviour.duracao > 30){
				cameraBehaviour.duracao -= 30;
				foiAlterado = true;
			}
			if((moveHorizontal < 1 && moveHorizontal > -1)){
				foiAlterado = false;
			}
		}

	}
}
