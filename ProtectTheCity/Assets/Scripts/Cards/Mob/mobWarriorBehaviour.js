#pragma strict

var velAttack: float;
var power : float;
var velocidade : float;
var cont: float;
var canAttack: boolean;
var anima: Animator;

var targetEnemy: GameObject;
var lifeBack: float;

function Start () {
	this.gameObject.GetComponent.<lifeBehaviour>().life = 50;
	anima = GetComponentInChildren(Animator);
}

function Update () {
	if(stateMachine.stateCheck != "pause"){
		this.gameObject.GetComponent.<Rigidbody2D>().velocity.y = - velocidade * Time.deltaTime;

		attackNow();
	} else {
		this.GetComponent.<Rigidbody2D>().velocity = new Vector2(0,0); // -------------- NÃO PERMITE A CARTA FICAR EM MOVIMENTAÇÃO
	}
}

function attackNow(){
if(canAttack == true){
this.gameObject.GetComponent.<Rigidbody2D>().velocity.y = 0;
			if( cont > 100 ){
			anima.SetBool("attack", true);
			 lifeBack -= power;
			 targetEnemy.GetComponent.<lifeBehaviour>().life = lifeBack;
				 if(lifeBack <= 0){
				 //targetEnemy.GetComponent.<lifeBehaviour>().life = lifeBack;
				 canAttack = false;
				 anima.SetBool("attack", false);
				 }
			 cont = 0;
			} else {
			cont += Time.deltaTime + velAttack;
			}
		}
}

function OnCollisionEnter2D(coll: Collision2D){
		if(coll.gameObject.tag == "enemy"){
		 canAttack = true;
		 targetEnemy = coll.gameObject;
		 lifeBack = coll.gameObject.GetComponent.<lifeBehaviour>().life;
		}
}