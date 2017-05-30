#pragma strict
var bullet:GameObject;
var timecont:float;
var velocidade:float;
static var bullet_die:boolean;
var destroyItNow:float;


function Start () {
    timecont = 0;
}



function Update () {
            if(bullet.transform.localScale.x == 1){ //Faz a bala Andar
            bullet.transform.Translate(velocidade*Time.deltaTime,0,0);
            } else{ //Inverte o sentido
            bullet.transform.Translate(-velocidade*Time.deltaTime,0,0);
            }

    timecont += Time.deltaTime; //Destrói a bala
    if(timecont > destroyItNow){
        Destroy(bullet);
    } 
    if(bullet_die == true){ //Se a variavél receber true a bala também é destruida
        Destroy(bullet);
        bullet_die = false;
    }
}
function OnTriggerEnter2D (coll: Collider2D){

    if(coll.gameObject.tag == "Player"){       

    	player_behavior.canDamage = false;
    	bullet_die = true;
        
    }
}