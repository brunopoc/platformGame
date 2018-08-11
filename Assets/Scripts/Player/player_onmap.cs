using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class player_onmap : MonoBehaviour {

    UnityEngine.SceneManagement.SceneManager newScene;

    Rigidbody2D player;  //Personagem
    Animator anime; //Animação do personagem
    GameObject phase1_block;

    float velocidade = 0.5f; //Velocidade
    float duration;
    bool onPhase_1;
    bool checkTrans;
    bool phase1_clear;

    // ----------------- VARIAVEIS PARA ARMAZENAR SCRIPTS ---------------

    public FadeInOutBehaviour FadeInOutBehaviour;

    void Start (){

        FadeInOutBehaviour = GameObject.Find("back").GetComponent<FadeInOutBehaviour>(); // Isso precisa sair daqui

        player = GetComponent<Rigidbody2D>();
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
            SceneManager.LoadScene("phase_1");
		} else if(checkTrans == true) {
		 	duration += Time.deltaTime;
		}
    }

    void playerWalkonMap (){
	    player.velocity = new Vector2(velocidade * Input.GetAxis("Horizontal"), player.velocity.y);
	    player.velocity = new Vector2 (player.velocity.x, velocidade * Input.GetAxis("Vertical"));
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
		yield return new WaitForSeconds(6);
			phase1_block.SetActive(false);
		}
    }
}