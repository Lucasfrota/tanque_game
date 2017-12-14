using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuSelectionController : MonoBehaviour {

	public AudioSource somTiro;
	public Text aviso;
	static public int numPlayersNoJogo;

	private float temp;

	// Use this for initialization
	void Start () {
		numPlayersNoJogo = 0;
		temp = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start")){
			if (numPlayersNoJogo >= 2) {
				somTiro.Play ();
				GameController.tempoPartida = cameraBehaviour.duracao;
				StartCoroutine (ChangeLevel ());
			} else {
				aviso.text = "Voce precisa de pelos menos 2 \n jogadores para iniciar uma partida!";
			}
		}
		if(numPlayersNoJogo >= 2){
			temp += Time.deltaTime;
			if (temp < 4) {
				aviso.text = "Presione Start quando todos\n os tanques estiverem ligados";
			} else {
				aviso.text = "";
			}
		}
	
	}
		
	IEnumerator ChangeLevel(){
		float fadeTime = GameObject.Find ("chaoTanqueNovo").GetComponent<Fading>().BeginFade (1);//Fading.BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel("maze");
	}
}
