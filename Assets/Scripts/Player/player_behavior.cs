using UnityEngine;
using System.Collections;

public class player_behavior : MonoBehaviour {

    public player_lifebar player_lifebar;
    public float velocidade;                //Velocidade
    public float jumpvel;                   //Velocidade de pulo
    public Rigidbody2D player;              //Persongaem
    public Animator anime;                  //Animação do personagem
    public bool canDamage = true;           //Pode Receber Dano
    public bool canMove = true;
    public bool canDash = true;             //Esta no chão
    public bool OnGround;                   //Esta no chão
    bool canShootAgain = true;
    float contDamage;                       // Contador para o dano
    float timeToShootAgain;
    public GameObject bullet;               //Munição do personagem
    public Vector3 bullet_position;         //Posição onde a bala é instanciada

    public Transform ponto1;                //Posição do Physics2D.LineCast
    public Transform ponto2;
    public GameObject respawn;

    // CONTROLAR INFORMAÇÕES DA TELA

    static int collectedcrystals;
    static int lifes;
    static int extralifes;

    void Start (){ //Dando valor as variaveis

        player_lifebar = GameObject.Find("Player").GetComponent<player_lifebar>();

        player = GetComponent<Rigidbody2D>();
        anime = GetComponentInChildren<Animator>();

    }

    void Update (){
        PlayerJump();                           //Função que habilita o pulo
        PlayerWalk();                           //Player Andando
        PlayerShooting();                       //Jogador Atirando
        StartCoroutine(PlayerDash());
        StartCoroutine(PlayerReceiveDamage());  //Jogador Recebendo Dano
    }

    IEnumerator PlayerReceiveDamage (){
        if(canDamage == false){ //CONTADOR PARA RECEBER DANO
        	anime.SetBool("damage", true);
            contDamage += Time.deltaTime;   
	        if(contDamage > 0.5f){
	            player_lifebar.currentlife--;
	            canDamage = true;
	            anime.SetBool("damage", false); //Animação
	            contDamage = 0;
                yield break;
            } 
        }
        if(player_lifebar.currentlife == 0){
            canDamage = true;
            anime.SetBool("damage", true); //Animação
            contDamage = 0;
            yield return new WaitForSeconds(1.5f);
            anime.SetBool("damage", false);
            player_lifebar.currentlife = 3;
            player.transform.position = respawn.transform.position;
        }
    }

    IEnumerator PlayerDash (){ 
		if(Input.GetKeyDown("x") && OnGround == true && canDash == true && Input.GetAxis("Horizontal") != 0)
        {
			velocidade = 10;
			canDash = false;
		    yield return new WaitForSeconds(0.4f);
		    velocidade = 4;
		    yield return new WaitForSeconds(0.1f);
		    canDash = true;
		}
    }

    void PlayerShooting (){
        if(canDamage == false || canMove == false){}else{
            if(Input.GetKeyDown("z")){ //ATIRANDO
                bullet_position = player.transform.position;
                if(player.transform.localScale.x == 1){
                    bullet_position.x = 0.41f + player.transform.position.x;
                    bullet_position.y = 0.15f + player.transform.position.y;
                    bullet.transform.localScale = new Vector3(1, 1, 1);
                } else {
                    bullet_position.x = -0.41f + player.transform.position.x;
                    bullet_position.y = 0.15f + player.transform.position.y;
                    bullet.transform.localScale = new Vector3(-1, 1, 1);
                }
                Instantiate(bullet, bullet_position,  Quaternion.identity);
            }
            StartCoroutine(animatorPlayerControl());
        }
    }//FIM DO SCRIPT DE TIRO

    void PlayerJump (){
        if(canMove == true){
            if(Physics2D.Linecast(ponto1.position, ponto2.position) && canDamage == true){ //Avalia se está no chão
                OnGround = true;
            }else{
                OnGround = false;
            }
            if(OnGround == true){  //PULO
                if(Input.GetButtonDown("Jump")){
                    player.velocity = new Vector2(player.velocity.x, jumpvel);
                }
            }
            StartCoroutine(animatorPlayerControl());
        }
    }

    void PlayerWalk (){ //Função do andar
        if(canDamage == false || canMove == false){ //FICA PARADO ENQUANTO NÃO PUDER RECEBER DANO
    	    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(animatorPlayerControl());
        } else {
            player.velocity = new Vector2(velocidade * Input.GetAxis("Horizontal"), player.velocity.y); //MOVIMENTAÇÃO DO PLAYER
            if(Input.GetAxis("Horizontal") > 0){
                player.transform.localScale = new Vector3(1,1,1);
                StartCoroutine(animatorPlayerControl());               
            } else if(Input.GetAxis("Horizontal") < 0) {
                player.transform.localScale = new Vector3(-1,1,1);
                StartCoroutine(animatorPlayerControl());                     
            }
        }
    } //FIM DA MOVIMENTAÇÃ

    void OnCollisionEnter2D ( Collision2D coll  ){
        if(coll.gameObject.tag == "Enemy" && canDamage == true){
            anime.SetBool("damage", true);
            canDamage = false;
        }
    }

    IEnumerator animatorPlayerControl (){
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

		    if(Input.GetKeyDown("z")){
                canShootAgain = false;
                timeToShootAgain = 0;
            }

            if (canDash != true) {
                anime.SetBool("dash", true);
            }
            else {
                anime.SetBool("dash", false);
            }

            if (canShootAgain == false){ //CONTADOR PARA RECEBER DANO
                anime.SetBool("fire", true);
                timeToShootAgain += Time.deltaTime;
                if (timeToShootAgain > 1.0f)
                {
                    canShootAgain = true;
                    anime.SetBool("fire", false); //Animação
                    timeToShootAgain = 0;
                    yield break;
                }
            }
        }
        yield return 0;
    }
}