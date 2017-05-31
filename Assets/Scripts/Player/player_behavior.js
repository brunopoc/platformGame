#pragma strict
var velocidade:float; //Velocidade
var jumpvel:float; //Velocidade de pulo
var player:Rigidbody2D;  //Persongaem
var anime:Animator; //Animação do personagem
static var canDamage:boolean; //Pode Receber Dano
static var canMove:boolean;
var canDash:boolean;
var contDamage:float; // Contador para o dano

var bullet: GameObject; //Munição do personagem
var bullet_position:Vector3; //Posição onde a bala é instanciada

var OnGround:boolean; //Esta no chão
var ponto1:Transform; //Posição do Physics2D.LineCast
var ponto2:Transform;
var respawn:GameObject;

// CONTROLAR INFORMAÇÕES DA TELA

static var collectedcrystals: int;
static var lifes: int;
static var extralifes: int;



function Start () { //Dando valor as variaveis

	FadeInOutBehaviour.playfadeIn = true; // transação da tela

    canDamage = true;
    canMove = true;
    canDash = true;

    player = GetComponent(Rigidbody2D);
    anime = GetComponentInChildren(Animator);

    contDamage = 0;
    
}


function Update () {
               
    PlayerJump(); //Função que habilita o pulo
    
    PlayerWalk(); //Player Andando
    
    PlayerReceiveDamage(); //Jogador Recebendo Dano
    
    PlayerShooting(); //Jogador Atirando

    PlayerDash();
             
}



function PlayerReceiveDamage(){
        if(canDamage == false){ //CONTADOR PARA RECEBER DANO
        		anime.SetBool("damage", true);
            	contDamage += Time.deltaTime;                 
	            if(contDamage > 0.5){
	                player_lifebar.currentlife--;
	                canDamage = true;
	                anime.SetBool("damage", false); //Animação
	                contDamage = 0;
            } 
        }
        if(player_lifebar.currentlife == 0){
            canDamage = true;
            anime.SetBool("damage", true); //Animação
            contDamage = 0;
            yield WaitForSeconds(1.5);
            anime.SetBool("damage", false);
            player_lifebar.currentlife = 3;
            player.transform.position = respawn.transform.position;

        }
}

function PlayerDash(){ 
		if(Input.GetKey("x") && OnGround == true && canDash == true){
				velocidade = 10;
				canDash = false;
		    	yield WaitForSeconds(0.5);
		    	velocidade = 4;
		    	yield WaitForSeconds(0.5);
		    	canDash = true;
		    }
}

function PlayerCheckDash(){
				
}

function PlayerOnWall(){}

function PlayerShooting(){
    if(canDamage == false || canMove == false){}else{
    if(Input.GetKeyDown("z")){ //ATIRANDO
                    bullet_position = player.transform.position;
                    if(player.transform.localScale.x == 1){
                        bullet_position.x = 0.41 + player.transform.position.x;
                        bullet_position.y = 0.15 + player.transform.position.y;
                        bullet.transform.localScale.x = 1; 
                    } else {
                        bullet_position.x = -0.41 + player.transform.position.x;
                        bullet_position.y = 0.15 + player.transform.position.y;
                        bullet.transform.localScale.x = -1;
                    }
                    Instantiate(bullet, bullet_position,  Quaternion.identity);
    }
    animatorPlayerControl();

        //FIM DO SCRIPT DE TIRO
        }
}



function PlayerJump(){
if(canMove == false){}else{
    if(Physics2D.Linecast(ponto1.position, ponto2.position) && canDamage == true){ //Avalia se está no chão
        OnGround = true;
    }else{
        OnGround = false;
    }

    if(OnGround == true){  //PULO
        if(Input.GetButtonDown("Jump")){
            player.velocity.y = jumpvel;
        }
    }

    animatorPlayerControl();
	}
}

function PlayerWalk(){ //Função do andar
    if(canDamage == false || canMove == false){ //FICA PARADO ENQUANTO NÃO PUDER RECEBER DANO
    	player.GetComponent.<Rigidbody2D>().velocity.x = 0;
    	animatorPlayerControl(); 
    } else {
        player.velocity.x = velocidade * Input.GetAxis("Horizontal"); //MOVIMENTAÇÃO DO PLAYER
            if(Input.GetAxis("Horizontal") > 0){
                player.transform.localScale.x = 1;
                animatorPlayerControl();               
            } else if(Input.GetAxis("Horizontal") < 0) {
                player.transform.localScale.x = -1;
                animatorPlayerControl();                     
            } //FIM DA MOVIMENTAÇÃO
    }
}

function OnCollisionEnter2D (coll: Collision2D){
    if(coll.gameObject.tag == "Enemy" && canDamage == true){
        anime.SetBool("damage", true);
        canDamage = false;
    }
}

function animatorPlayerControl(){
if(canDamage == false || canMove == false){ //FICA PARADO ENQUANTO NÃO PUDER RECEBER DANO
		anime.SetFloat("walk", 0); //ANIMAÇÃO
		anime.SetBool("parado", true);
    }else{
					if(Input.GetAxis("Horizontal") == 0){   //Animação Andando
					anime.SetFloat("walk", 0); //ANIMAÇÃO
	                anime.SetBool("parado", true);
		            }else if(Input.GetAxis("Horizontal") > 0){ 			
	                anime.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal"))); //ANIMAÇÃO
	                anime.SetBool("parado", false);
	                }else{
	                anime.SetFloat("walk", Input.GetAxis("Horizontal")); //ANIMAÇÃO
	                anime.SetBool("parado", false);
	                }

	                if(player.transform.localScale.x == 1){
	                anime.SetBool("mode_s", false);
	                }else if(player.transform.localScale.x == -1){
	                anime.SetBool("mode_s", true);
	                }

                    if(player.velocity.y > 1) { //ANIMAÇÃO DO PULO
			        anime.SetBool("jumping", true);
			        anime.SetBool("falling", false);
				    } else if (player.velocity.y < -1){
			        anime.SetBool("jumping", false);
			        anime.SetBool("falling", true);
			  		} else if(player.velocity.y == 0) {
			        anime.SetBool("jumping", false);
			        anime.SetBool("falling", false);
			  		}  //FIM DO  PULO

			        if(Input.GetKeyDown("z") && anime.GetBool("fire") == false){
			        anime.SetBool("fire", true);
			        yield WaitForSeconds (0.4);
			        anime.SetBool("fire", false);
						}
				}

}