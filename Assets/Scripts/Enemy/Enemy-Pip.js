#pragma strict
static var anima:Animator;
var enemy:Rigidbody2D; 
var timeToAttack:float;
var countToAttack:float;
var timeToReceive:float;
var life:int;
var bullet: GameObject;
var bullet_position:Vector3;
var contAnimator: float;



function Start () {
	countToAttack = 0;
    timeToReceive = 0;
    enemy = GetComponent(Rigidbody2D);
    anima = GetComponent(Animator);
}

function FixedUpdate () {
	enemy = GetComponent(Rigidbody2D);
	if(countToAttack > timeToAttack){
	anima.SetBool("shooting", true); //(-0.11f + enemy.transform.position.x, -0.17f + enemy.transform.position.y, 0);
	bullet_position = Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
	bullet.transform.localScale.x = -1;
	Instantiate(bullet, bullet_position, new Quaternion(0,0,0,0));
	countToAttack = 0;
	} else {
		
		countToAttack += Time.deltaTime;
	}
	if(anima.GetBool("shooting") == true){
		contAnimator += Time.deltaTime;
	}
	if(contAnimator > 1){
		anima.SetBool("shooting", false);
		contAnimator = 0;
	}
	    if(life <= 0){
        Destroy(gameObject);
    }
}
function OnTriggerEnter2D (coll: Collider2D){
    if(coll.gameObject.tag == "HeroBullet"){
        life--;
        bullet_behavior.bullet_die = true;
        timeToReceive = 0;
    }
    }