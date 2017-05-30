#pragma strict


var player:Rigidbody2D;  //Personagem
var velocidade:float; //Velocidade
var anime:Animator; //Animação do personagem
var newScene:UnityEngine.SceneManagement.SceneManager;
var onPhase_1:boolean;
static var phase1_clear:boolean;
var phase1_block:GameObject;
var duration : float;
var checkTrans : boolean;
function Start () {
	player = GetComponent(Rigidbody2D);
	velocidade = 0.5;
	anime = GetComponent(Animator);
	FadeInOutBehaviour.playfadeIn = true;
	checkTrans = false;
}

function Update () {
	playerWalkonMap();
	animaController();
	phase_1_clear();
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

function playerWalkonMap(){

	player.velocity.x = velocidade * Input.GetAxis("Horizontal");
	player.velocity.y = velocidade * Input.GetAxis("Vertical");
}

function animaController(){

	if (player.velocity.x != 0){
	anime.SetFloat("horizontal", Input.GetAxis("Horizontal"));
	anime.SetBool("direction", false);
	}
	if (player.velocity.y != 0){
	anime.SetFloat("vertical", Input.GetAxis("Vertical"));
	anime.SetBool("direction", true);
	}
}

function OnTriggerEnter2D (coll: Collider2D){
    if(coll.gameObject.tag == "phase_1"){
    	onPhase_1 = true;
    }
    }

function OnTriggerExit2D (coll: Collider2D){
    if(coll.gameObject.tag == "phase_1"){
    	onPhase_1 = false;
    }
    }

function phase_1_clear(){
		if(Input.GetKeyDown("m")){
		yield WaitForSeconds(6);
			phase1_block.SetActive(false);
		}
}