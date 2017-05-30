#pragma strict

var velAttack: float;
var power : float;
var velocidade : float;
var cont: float;
var canAttack: boolean;

var targetEnemy: GameObject;
var lifeBack: float;

function Start () {
	this.gameObject.GetComponent.<lifeBehaviour>().life = 15;
}

function Update () {
		if(stateMachine.stateCheck != "pause"){
			this.gameObject.GetComponent.<Rigidbody2D>().velocity.y = velocidade * Time.deltaTime;

			attackNow();

		} else {
			this.GetComponent.<Rigidbody2D>().velocity = new Vector2(0,0); // -------------- NÃO PERMITE A CARTA FICAR EM MOVIMENTAÇÃO
		}
}

function attackNow(){
if(canAttack == true){
this.gameObject.GetComponent.<Rigidbody2D>().velocity.y = 0;
			if( cont > velAttack ){
			 lifeBack -= power;
			 targetEnemy.GetComponent.<lifeBehaviour>().life = lifeBack;
				 if(lifeBack <= 0){
				 //targetEnemy.GetComponent.<lifeBehaviour>().life = lifeBack;
				 canAttack = false;
				 }
			 cont = 0;
			} else {
			cont += Time.deltaTime;
			}
		}
}

function OnCollisionEnter2D(coll: Collision2D){
		if(coll.gameObject.tag == "king" ){
		 canAttack = true;
		 targetEnemy = coll.gameObject;
		 lifeBack = coll.gameObject.GetComponent.<lifeBehaviour>().life;
		}
		if(coll.gameObject.tag == "kingaCard" ){
		 canAttack = true;
		 targetEnemy = coll.gameObject;
		 lifeBack = coll.gameObject.GetComponent.<lifeBehaviour>().life;
		}
}