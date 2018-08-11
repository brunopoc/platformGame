using UnityEngine;
using System.Collections;

public class walk : MonoBehaviour {

float velocidade; //Velocidade
float jumpvel; //Velocidade de pulo
Rigidbody2D player;  //Personaem
Animator anime; //Animação do personagem
bool  canDamage; //Pode Receber Dano
float contDamage; // Contador para o dano

GameObject bullet; //Munição do personagem
Vector3 bullet_position; //Posição onde a bala é instanciada

bool  OnGround; //Esta no chão
Transform ponto1; //Posição do Physics2D.LineCast
Transform ponto2;
GameObject respawn;



void Start (){ //Dando valor as variaveis

    canDamage = true;

    player = GetComponent<Rigidbody2D>();
    anime = GetComponentInChildren<Animator>();

    velocidade = 2;
    jumpvel = 3;
    contDamage = 0;
    
}


void Update (){
               
    PlayerJump(); //Função que habilita o pulo
    
    PlayerWalk(); //Player Andando
    
    StartCoroutine(PlayerReceiveDamage()); //Jogador Recebendo Dano
    
    PlayerShooting(); //Jogador Atirando
             
}



IEnumerator PlayerReceiveDamage (){
        if(canDamage == false){ //CONTADOR PARA RECEBER DANO
            	contDamage += Time.deltaTime;                 
	            if(contDamage > 0.5f){
	                canDamage = true;
	                anime.SetBool("damage", false); //Animação
	                contDamage = 0;
            } 
        }
        if(player_lifebar.currentlife == 0){
            canDamage = true;
            anime.SetBool("damage", true); //Animação
            contDamage = 0;
            yield return WaitForSeconds(1.5f);
            anime.SetBool("damage", false);
            player_lifebar.currentlife = 3;
            player.transform.position = respawn.transform.position;

        }
}

void PlayerShooting (){
    if(canDamage == false){}else{
    if(Input.GetKeyDown("z")){ //ATIRANDO
                    bullet_position = player.transform.position;
                    if(player.transform.localScale.x == 1){
                        bullet_position.x = 0.3f + player.transform.position.x;
                        bullet_position.y = 0.05f + player.transform.position.y;
                        bullet.transform.localScale.x = 1; 
                    } else {
                        bullet_position.x = -0.3f + player.transform.position.x;
                        bullet_position.y = 0.05f + player.transform.position.y;
                        bullet.transform.localScale.x = -1;
                    }
                    Instantiate(bullet, bullet_position,  Quaternion.identity);
    }
            StartCoroutine(animatorPlayerControl());

            //FIM DO SCRIPT DE TIRO
        }
}



void PlayerJump (){
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

    StartCoroutine(animatorPlayerControl());

    }

void PlayerWalk (){ //Função do andar
    if(canDamage == false){ //FICA PARADO ENQUANTO NÃO PUDER RECEBER DANO

    }else{
        player.velocity.x = velocidade * Input.GetAxis("Horizontal"); //MOVIMENTAÇÃO DO PLAYER
            if(Input.GetAxis("Horizontal") > 0){
                player.transform.localScale.x = 1;
                StartCoroutine(animatorPlayerControl());               
            } else if(Input.GetAxis("Horizontal") < 0) {
                player.transform.localScale.x = -1;
                StartCoroutine(animatorPlayerControl());                     
            } //FIM DA MOVIMENTAÇÃO
    }
}

IEnumerator OnCollisionEnter2D ( Collision2D coll  ){
    if(coll.gameObject.tag == "Enemy" && canDamage == true){
        anime.SetBool("damage", true);
        canDamage = false;
        yield return WaitForSeconds(0.4f);
        player_lifebar.currentlife--;
    }
}

IEnumerator animatorPlayerControl (){
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
                    yield return WaitForSeconds (0.4f);
			        anime.SetBool("fire", false);
				    }
}
}