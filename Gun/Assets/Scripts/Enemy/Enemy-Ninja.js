#pragma strict
var enemy:Rigidbody2D; // Inimigo - NINJA
var velocidade:float;
var timecont:float; //contagem para a virar para o outro lado
var life:int; //vida do inimigo
var timeToAttack:float;
var timeToWalk:float;
var attackPlayer:boolean;
var timeToReceive:float;

static var anima:Animator;

function Start () {
	FadeInOutBehaviour.playfadeIn = true; // transação da tela
    anima = GetComponentInChildren(Animator);
    enemy = GetComponent(Rigidbody2D);
    velocidade = 1;
    timecont = 1;
    life = 3;
    timeToWalk = 2;
    timeToAttack = 0;
    timeToReceive = 0;
    attackPlayer = false;
    //enemy.constraints = RigidbodyConstraints2D.FreezePositionX; 
    enemy.constraints = RigidbodyConstraints2D.FreezeRotation; 
}

function Update () {
   
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
   
        if(timeToAttack > 0.2){
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
		            enemy.transform.localScale.x = -1;
		            timecont += Time.deltaTime;
		        } else{
		            if(timecont > 5 && timecont <= 10){ // TROCA DE LADO
		                enemy.transform.Translate(velocidade*Time.deltaTime,0,0);
		                enemy.transform.localScale.x = 1;
		                timecont += Time.deltaTime;
		            } else{
		                timecont = 0;
		            } 
		        } // FIM DA MOVIMENTAÇÃO   
        }
    }
  

}

function OnTriggerEnter2D (coll: Collider2D){
    if(coll.gameObject.tag == "HeroBullet"){
        life--;
        anima.SetBool("recebendo", true);
        bullet_behavior.bullet_die = true;
        timeToReceive = 0;
    }
    if(coll.gameObject.tag == "Player" && attackPlayer == false){       
        attackPlayer = true;
        enemy.transform.Translate (0,0,0);
        timeToAttack = 0;
        
    }
}