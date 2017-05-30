#pragma strict

static var sceneCheck : String;

var onDeckManager : boolean; // ------------- VERIFICA SE ESTA NA SEÇÃO DE GERENCIAMENTO DE CARD
private var startPosition : Vector3; // ------------ POSIÇÃO INICAL DA CARTA
private var startScale: Vector3; // ---------------- TAMANHO INICAL DA CARTA

private var velocidadeDaTransicao:float; // -------- VELOCIDADE DA MOVIMENTAÇÃO DA CARTA NO EIXO X
static var notMoveInY:boolean; // ------------------ BLOQUEAR A MOVIMENTAÇÃO DA CARTA EM Y

var deckNumber: int; // ---------------------------- NUMERO DO CARD
var finalSlotRight : boolean; // ------------------- VERIFICA SE ESTÁ NO ULTIMO SLOT DA DIREITA
var finalSlotLeft : boolean; // -------------------- VERIFICA SE ESTÁ NO ULTIMO SLOT DA ESQUERDA
var inMouse : boolean; // -------------------------- VERIFICA SE ESSA CARTA ESTÁ NO MOUSE
var cardSelected : boolean;

private var renderthis: Renderer; // --------------- RENDER PRA ALTERAR A ORDER IN LAYER

static var controlStartPosition:boolean; // -------- CONTROLE DAS VEZES QUE SALVA A POSIÇÃO INICIAL



function Start () {
	startScale = new Vector3(0.08f, 0.07f, 0.615f);
	startPosition = this.gameObject.transform.position;
	velocidadeDaTransicao = 5;
	renderthis = GetComponent(Renderer);	
} // ---------------------------------------------------------FIM DA FUNÇÃO Start ------------------------------------------------------------



function Update () {

		this.GetComponent.<Rigidbody2D>().velocity = new Vector2(0,0); // -------------- NÃO PERMITE A CARTA FICAR EM MOVIMENTAÇÃO POR COLIDIR
			if(sceneCheck != "freeze"){
							if(sceneCheck == "my_decks"){
								CardWithMouse();
								CardToBackPosition();



							} else {
							if(stateMachine.stateCheck != "pause"){ // ------------------------- VERIFICA O ESTADO DO JOGO
							CardWithMouse(); // COMPORTAMENTO DA CARTA COM O MOUSE
							CardInX(); // COMPORTAMENTO DA ROLAGEM DA CARTA
							CardToBackPosition(); // COMPORTAMENTE QUE FAZ A CARTA RETORNAR
							} else {

							} // -------------------------------------- FIM DA VERIFICAÇÃO DE PAUSE -----------------------------------------------
				} // ------------------------------ FIM DA VERIFICAÇÃO DA CENA MY DECKS -----------------------------------------------------------
			} else {

			}
} // -------------------------------------- FIM DA FUNÇÃO UPDATE --------------------------------------------------------------------------


function CardWithMouse() {
		  /* ######################################### CONTROLE DE FAZER A CARTA SE MOVIMENTAR DE ACORDO COM O MOUSE ##################### 
			 ############################ FUNCIONA QUANDO O BOTÃO DO MOUSE ESTÁ PRESSIONADO ##############################################
			 ############################ FUNCIONA ENQUANTO O MOUSE ESTÁ EM COLISÃO COM A CARTA ##########################################
			 ############################ QUANDO NÃO ESTÁ MOVENDO EM Y ###################################################################
			 */

			 if(Input.GetMouseButton(0) && mouseBehaviour.moveCard != null && notMoveInY == false && mouseBehaviour.onButtonPositionL == false && mouseBehaviour.onButtonPositionR == false){
			 	cardSelected = true;
			 	mouseBehaviour.moveCard.GetComponent.<Renderer>().sortingOrder = 10;
		    	mouseBehaviour.moveCard.transform.position = mouseBehaviour.position.transform.position;
		    	mouseBehaviour.moveCard.transform.position.z = -5;
		    	mouseBehaviour.moveCard.transform.localScale = new Vector3(0.16f, 0.14f, 1f); // AUMENTA O TAMANHO DA CARTA
		    	mouseBehaviour.moveCard.GetComponent.<Collider2D>().isTrigger = true; // RETIRA A COLISÃO DA CARTA
		    }
} // ---------------------------------------- FIM DA FUNÇÃO CardWithMouse ------------------------------------------------------------------


function AllowCardForX(){
	/* ######################################### PERMITIR QUE O CARD SE MOVIMENTE OU NÃO DE ACORDO COM OS LIMITES #########################
	*/
			if(finalSlotRight == true && deckNumber == 0){
		    	deckBehaviour.blockMovementRight = true; // ------------ Bloqueia o deck
		    }
		     if(finalSlotRight == false && deckNumber == 0){
		    	deckBehaviour.blockMovementRight = false; // ----------- Desbloqueia o deck
		    }
		    if(finalSlotLeft == true && deckNumber == 19){
		   		deckBehaviour.blockMovementLeft = true; // ----------------- Bloqueia o deck
		    }
		     if(finalSlotLeft == false && deckNumber == 19){
		    	deckBehaviour.blockMovementLeft = false; // ------------ Desbloqueia o deck
		    }
} // --------------------------------------- FIM DA FUNÇÃO AllowCardForX ------------------------------------------------------------------


function CardInX(){
		    /*######################################### MOVIMENTAÇÃO DO DECK NO EIXO X ####################################################
		    ############### FUNCIONA QUANDO O MOUSE ESTÁ SENDO PRESSIONADO ################################################################
		    ############### QUANDO ESTÁ NO CAMPO DO DECK (OnMouse) ########################################################################
		    ############### QUANDO O MOUSE NÃO ESTÁ PEGANDO NENHUM OBJETO/CARTA ###########################################################
		    ############### QUANDO O MOUSE EM X ESTÁ E MOVIMENTO ##########################################################################*/

		    AllowCardForX();

			if(Input.GetMouseButton(0) && deckBehaviour.onMouse == true && cardSelected == false){
							if(Input.GetAxis("Mouse X") > 0 && deckBehaviour.blockMovementRight == false){
							this.gameObject.transform.position.x += Input.GetAxis("Mouse X") * velocidadeDaTransicao * Time.deltaTime;
							notMoveInY = true;

							} else if(Input.GetAxis("Mouse X") > 0 && deckBehaviour.blockMovementLeft == false){
							this.gameObject.transform.position.x += Input.GetAxis("Mouse X") * velocidadeDaTransicao * Time.deltaTime;
							notMoveInY = true;

							}
			}
			if(Input.GetMouseButton(0) && mouseBehaviour.onButtonPositionL == true && deckBehaviour.blockMovementLeft == false){
				this.gameObject.transform.position.x -= velocidadeDaTransicao * 0.2 * Time.deltaTime;
			}
			if(Input.GetMouseButton(0) && mouseBehaviour.onButtonPositionR == true && deckBehaviour.blockMovementRight == false){
				this.gameObject.transform.position.x += velocidadeDaTransicao * 0.2 * Time.deltaTime;
			}


} // --------------------------------------- FIM DA FUNÇÃO CardInX -----------------------------------------------------------------------


function CardToBackPosition(){
		 /* ######################################### CONTROLE DE FAZER A CARTA VOLTAR A SUA POSIÇÃO ORIGINAL ##########################
		    ############################# FUNCIONA QUANDO O BOTÃO DO MOUSE É SOLTO ######################################################## 
		    ############################# QUANDO NÃO ESTÁ NO CAMPO DO REI #################################################################
		    ############################# QUANDO O MOUSE ESTÁ DETECTANDO A CARTA ##########################################################
		    ############################# QUANDO NÃO ESTÁ SENDO MOVIMENTADO EM Y ##########################################################
		    */
		    if(Input.GetMouseButtonUp(0) && mouseBehaviour.kingField == false && inMouse == true  && mouseBehaviour.onButtonPositionL == false && mouseBehaviour.onButtonPositionR == false){
		    renderthis.sortingOrder = 6;
		    this.gameObject.transform.position = startPosition;
		   	this.gameObject.transform.localScale = startScale;
		   	this.gameObject.GetComponent.<Collider2D>().isTrigger = false;
		   	controlStartPosition = false;
		   	notMoveInY = false;
		   	cardSelected = false;
		    }
} // ------------------------------------------------- FIM DA FUNÇÃO CardToBackPosition --------------------------------------------------


function OnTriggerEnter2D(coll: Collider2D){    
 	
    if(coll.gameObject.tag == "slot" && controlStartPosition == false && cardSelected == false){
				    startPosition = coll.gameObject.transform.position;		    	  
    } // --------- VERIFICA A COLISÃO COM O SLOT
    if(coll.gameObject.tag == "mouse"){
    	inMouse = true;
    } // --------- VERIFICA A COLISÃO COM O MOUSE
    if(coll.gameObject.name == "slot_11"){
    	finalSlotRight = true;
    } 
    if(coll.gameObject.name == "slot_09"){
   		finalSlotLeft = true;
    }


}  // -------------------------------------- FIM DA FUNÇÃO OnTriggerEnter2D ----------------------------------------------------------------

function OnTriggerExit2D(coll: Collider2D){
	if(coll.gameObject.tag == "mouse"){
  		inMouse = false;
    }    
    if(coll.gameObject.name == "slot_11"){
    	finalSlotRight = false;
    }
    if(coll.gameObject.name == "slot_09"){
   		finalSlotLeft = true;
    }
       
} // --------------------------------------- FIM DA FUNÇÃO OnTriggerExit2D ----------------------------------------------------------------