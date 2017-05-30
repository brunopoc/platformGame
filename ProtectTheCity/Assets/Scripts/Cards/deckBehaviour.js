#pragma strict

static var onMouse:boolean;

var deckToSave : GameObject[];
private var cardSelect: GameObject;

var deckName : String[];
private var cardName : String;

var positionInGame : Transform[];

static var blockMovementRight : boolean;
static var blockMovementLeft : boolean;


private var i : int;




function Start () {

	saveCardInDeck();
	loadCardOnDeck();

}


function loadCardOnDeck(){
		
/* ############################################### SCRIPT RESPONSÁVEL POR INSTANCIAR O CONTEUDO DENTRO DO PLAYERPREFS ################################### 
   ###################################################################################################################################################### 
   ###################################################################################################################################################### 
   ###################################################################################################################################################### */

    i = 0;
		deckName = PlayerPrefsX.GetStringArray("currentDeck"); // PEGA O DECK ATUAL DO JOGADOR COM A FUNÇÃO DO PLAYERPREFSX
		Debug.Log(deckName);
		for(cardName in deckName){ // --------------------------------------------------------- PARA CADA STRING DENTRO DA MATRIZ "deckName"
				cardName = "Prefabs/" + "Deck" + "/Card/" + cardName; // -------------------------------- MONTA O CAMINHO DA CARTA
				cardSelect = Resources.Load(cardName) as GameObject; // ----------------------- FAZ A BUSCA DO GAMEOBJECT (CARTA)
				cardSelect = Instantiate(cardSelect, positionInGame[i].position, Quaternion.identity); // -- INSTANCIA DE ACORDO COM A MATRIZ DE TRANSFORMS
				cardSelect.GetComponent.<baseCardBehaviour>().deckNumber = i;
				i++; // ----------------------------------------------------------------------- A VÁRIAVEL QUE CONTA AS POSIÇÕES RECEBE +1

		}
}

function saveCardInDeck(){
		i = 0;
		for (cardSelect in deckToSave){
		deckName[i] = cardSelect.name;
			i++;
		}
		PlayerPrefsX.SetStringArray("currentDeck", deckName);
}

// ############################# VERIFICA SE O MOUSE ESTÁ NO CAMPO DO DECK ########################################
// ############################# NECESSÁRIO PARA A ROLAGEM DAS CARTAS  NA HORIZONTAL ##############################

function OnTriggerEnter2D(coll: Collider2D){ 
	if(coll.gameObject.tag == "mouse"){
		onMouse = true;
	}
}

function OnTriggerExit2D(coll: Collider2D){
	if(coll.gameObject.tag == "mouse"){
		onMouse = false;
	}
}