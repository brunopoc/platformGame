using UnityEngine;
using System.Collections;

public class player_onmap : MonoBehaviour {



Rigidbody2D player;  //Personagem
float velocidade; //Velocidade
Animator anime; //Animação do personagem
UnityEngine.SceneManagement.SceneManager newScene;
bool  onPhase_1;
static bool  phase1_clear;
GameObject phase1_block;
float duration;
bool  checkTrans;
void Start (){
	player = GetComponent<Rigidbody2D>();
	velocidade = 0.5f;
	anime = GetComponent<Animator>();
	FadeInOutBehaviour.playfadeIn = true;
	checkTrans = false;
}

void Update (){
	playerWalkonMap();
	animaController();
	StartCoroutine(phase_1_clear());
	if(onPhase_1 == true && Input.GetKeyDown("z")){
	FadeInOutBehaviour.playfadeOut = true;
	checkTrans = true;
	}
	if(duration >= 1 && checkTrans == true){
		 			newScene.LoadScene("phase_1");
		 		} else if(checkTrans == true) {
		 			duration += Time.deltaTime;
		 		}
}

void playerWalkonMap (){

	player.velocity.x = velocidade * Input.GetAxis("Horizontal");
	player.velocity.y = velocidade * Input.GetAxis("Vertical");
}

void animaController (){

	if (player.velocity.x != 0){
	anime.SetFloat("horizontal", Input.GetAxis("Horizontal"));
	anime.SetBool("direction", false);
	}
	if (player.velocity.y != 0){
	anime.SetFloat("vertical", Input.GetAxis("Vertical"));
	anime.SetBool("direction", true);
	}
}

void OnTriggerEnter2D ( Collider2D coll  ){
    if(coll.gameObject.tag == "phase_1"){
    	onPhase_1 = true;
    }
    }

void OnTriggerExit2D ( Collider2D coll  ){
    if(coll.gameObject.tag == "phase_1"){
    	onPhase_1 = false;
    }
    }

IEnumerator phase_1_clear (){
		if(Input.GetKeyDown("m")){
		yield return WaitForSeconds(6);
			phase1_block.SetActive(false);
		}
}
}