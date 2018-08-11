using UnityEngine;
using System.Collections;

public class enemy_ninja : MonoBehaviour {

    Rigidbody2D enemy; // Inimigo - NINJA
    float velocidade;
    float timecont; //contagem para a virar para o outro lado
    int life; //vida do inimigo
    float timeToAttack;
    float timeToWalk;
    bool  attackPlayer;
    float timeToReceive;
    public FadeInOutBehaviour FadeInOutBehaviour;

    static Animator anima;

    void Start (){
        FadeInOutBehaviour = GameObject.Find("back").GetComponent<FadeInOutBehaviour>(); // Isso precisa sair daqui

        FadeInOutBehaviour.playfadeIn = true; // transação da tela
        anima = GetComponentInChildren<Animator>();
        enemy = GetComponent<Rigidbody2D>();
        velocidade = 1;
        timecont = 1;
        life = 3;
        timeToWalk = 2;
        timeToAttack = 0;
        timeToReceive = 0;
        attackPlayer = false; 
        enemy.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }

    void Update (){
   
        if(timeToReceive >= 1){
  		    anima.SetBool("recebendo", false);
        }else{
            timeToReceive += Time.deltaTime;
            enemy.transform.Translate (0,0,0);
        }

        if(life <= 0){
            Destroy(gameObject);
        }
        if(attackPlayer == true) {
            timeToAttack += Time.deltaTime;
   
            if(timeToAttack > 0.2f){
                anima.SetBool("atacando", true);
                anima.SetBool("andando", false);
                timeToWalk = 0;
                attackPlayer = false;
            }     
        } else {
            if(timeToWalk < 1) {
                timeToWalk += Time.deltaTime;
            } else { // MOVIMENTAÇÃO DO PERSONAGEM

        		anima.SetBool("atacando", false);
		        anima.SetBool("andando", true);

		        if(timecont >= 0 && timecont <=5){ 
		            enemy.transform.Translate(-velocidade*Time.deltaTime,0,0);
		            enemy.transform.localScale = new Vector3 (-1, 1, 1);
		            timecont += Time.deltaTime;
		        } else{
		            if(timecont > 5 && timecont <= 10){ // TROCA DE LADO
		                enemy.transform.Translate(velocidade*Time.deltaTime,0,0);
		                enemy.transform.localScale = new Vector3(1, 1, 1);
		                timecont += Time.deltaTime;
		            } else{
		                timecont = 0;
		            } 
		        } // FIM DA MOVIMENTAÇÃO   
            }
        }
  

    }

    void OnTriggerEnter2D ( Collider2D coll  ){
        if(coll.gameObject.tag == "HeroBullet"){
            life--;
            anima.SetBool("recebendo", true);
            coll.gameObject.GetComponent<bullet_behavior>().bullet_die = true;
            timeToReceive = 0;
        }
        if(coll.gameObject.tag == "Player" && attackPlayer == false){       
            attackPlayer = true;
            enemy.transform.Translate (0,0,0);
            timeToAttack = 0;
        
        }
    }
}