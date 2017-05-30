#pragma strict
var mouseIsHere : boolean;

var deckId: int; // ---------------------------------------- Verifica qual deck esse objeto corresponde, o valor é alterado no script "deck_label"
// var deckAmount : int; // -------------------------------- Váriavel que corresponde a quantidade de decks


var cardLabel: GameObject; // ----------------------------- Precisa ser linkado na Unity, corresponde ao campo das cartas
var deckLabel : GameObject; // ---------------------------- relacionado ao campo dos decks
 // ------------------------------------------------------- >>> Váriaveis para as instancias das cartas e relativos <<< ----------------------------------------
 var contObj : GameObject;
 var newcontObj : GameObject;
 var excludCard : GameObject; // -------------------------- Botão que exclui -1 do contador das cartas
 var candNameBack : String;
 var contAmount : int;
 // ------------------------------------------------------- >>> Váriaveis para a função loadDeck() <<< ---------------------------------------------
var  cardList : String[];
var  cardInstantiate : String[];
var cardName : String;
var cardPosition : Vector3;
var cardObj: GameObject;
var newCard: GameObject;
var cardCont : GameObject;
var i : int;
// ------------------------------------------------------- >>> FIM DO CAMPO DE VÁRIAVEIS <<< ---------------------------------------------




function Start () {
cardPosition = new Vector3(-0.8f, 1.35f, 1); // ------------------ A Posição incial do Deck
/* ------------------------- COMEÇA SALVANDO EM DISCO OS DECKS QUE ESTÃO NO ARRAY AFIM DE REALIZAR TESTES */
	PlayerPrefsX.SetStringArray("Deck1", PlayerPrefsX.GetStringArray("currentDeck"));
	PlayerPrefsX.SetStringArray("Deck2", PlayerPrefsX.GetStringArray("currentDeck"));
	PlayerPrefsX.SetStringArray("Deck3", PlayerPrefsX.GetStringArray("currentDeck")); 
}

function Update () {
		if(mouseIsHere == true && Input.GetMouseButtonDown(0)){ // ----- Mouse colidiu com o Deck e o botão foi pressionado
				// deckAmount = PlayerPrefs.GetInt("deck_Amount"); // ----- Busca no disco a quantidade de decks
				deckLabel = GameObject.Find("deckBehaviour"); // ------- Busca em cena o objeto que corresponde ao campo ds decks
				Instantiate(cardLabel); // ----------------------------- Instancia um campo para as cartas
				loadDeck(); // ----------------------------------------- Carrega as cartas do deck
				deckLabel.SetActive(false); // ------------------------- Desativa o campo do deck
		}

}



function loadDeck(){ // --------------------------------------- função responsável por instanciar os decks ---------------------

			switch(deckId){
				case 0:
					cardList = PlayerPrefsX.GetStringArray("Deck1");
				break;
				case 1:
					cardList = PlayerPrefsX.GetStringArray("Deck2");
				break;
				case 2:
					cardList = PlayerPrefsX.GetStringArray("Deck3");
				break;
			}
			i = 0;
			var dontload : boolean;
			dontload = false;

			for(cardName in cardList){ // ------------------------------------------------ Para cada string no deck
			// Antes de decidir se vai ser instanciada é necessário verificar se já possúi uma carta
			// Caso tenho uma carta o laço apenas adiciona +1 ao campo text que deve ser instanciado ao lado das cartas
					for(var g: int; g < cardInstantiate.Length; g++){
						candNameBack = cardName + "(Clone)";
						if(candNameBack == cardInstantiate[g]){
							contObj = GameObject.Find("cardBehaviour(Clone)");
							contObj = contObj.transform.Find(candNameBack).gameObject;
							contObj = contObj.transform.Find("qtdCard").gameObject;
							contObj = contObj.transform.Find("txtQtdCard").gameObject;
						contAmount = int.Parse(contObj.GetComponent.<UnityEngine.UI.Text>().text);
						contAmount++;
						contObj.GetComponent.<UnityEngine.UI.Text>().text = contAmount.ToString();
						dontload = true;
						}
					}
					if (dontload == false){
					/*########################################################################################################################################
					NESSA REGIÃO EU TIVE PROBLEMAS COM A INSTANCIA DA UI, A SOLUÇÃO FOI MANDAR A CARTA TODA PARA DENTRO DO CANVAS, ASSIM CONSIGO INSTANCIAR A UI E MANTER ELA
					COMO FILHA DE UM GAMEOBJECT SEM SE PERDER NA CENA.
					########################################################################################################################################*/
								cardName = "Prefabs/" + "Deck" + "/Card/" + cardName; // ----------------------------------------- Monta o caminho onde estão os prefabs da carta
								newCard = Resources.Load(cardName) as GameObject; // --------------------------------------------- Carrega a carta de acordo com o caminho
								newCard = Instantiate(newCard, cardPosition, Quaternion.identity); // ---------------------------- Instancia a carta
								newCard.GetComponent.<baseCardBehaviour>().onDeckManager = true;
								cardCont = Instantiate (cardCont, new Vector3(0,0,1), Quaternion.identity); // ------------------- Instãncia uma IMG com TEXT (todos componentes do CANVAS)
								cardCont.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false); // --- Tira o img do canvas
								cardCont.transform.position = cardPosition; // ---------------------------------------------------- Posicioina o obj junto com sua respectiva carta
								cardCont.transform.position.y += -0.35f; // -------------------------------------------------------- Ajuste da posição
								cardCont.transform.position.x += -0.22f; // -------------------------------------------------------- Ajuste da posição
								cardInstantiate[i] = newCard.name; // ------------------------------------------------------------- Salva o nome da carta num array
								cardObj = GameObject.Find("cardBehaviour(Clone)"); // --------------------------------------------- Procura o componente que abriga as cartas
								cardObj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform); // ------------- Adiciona esse componente no canvas
								newCard.transform.parent = cardObj.transform; // -------------------------------------------------- Adiciona a carta no componente que abriga elas
								cardCont.transform.SetParent(newCard.transform); // ----------------------------------------------- Define o componente de IMG e texto como filho da carta
								cardCont.name = "qtdCard"; // --------------------------------------------------------------------- Edita o nome da carta
								cardCont.GetComponent.<RectTransform>().sizeDelta = new Vector2 (25, 25); // ---------------------- Controla o tomanho da IMG e texto
								cardCont.GetComponent.<RectTransform>().localScale = new Vector3 (0.0625f, 0.0714f, 1); // ---------- Controla o tamanho da IMG e texto
								newcontObj = cardCont.transform.Find("txtQtdCard").gameObject; // --------------------------------- Procura o componente de texto
								contAmount = 1; // -------------------------------------------------------------------------------- Adiciona o numero 1 a uma váriavel
							    newcontObj.GetComponent.<UnityEngine.UI.Text>().text = contAmount.ToString(); // ------------------ Coloca esse 1 no componente de texto
							    excludCard = Instantiate (excludCard, new Vector3(0,0,0), Quaternion.identity);
							    excludCard.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
							    excludCard.transform.SetParent(newCard.transform);
							    excludCard.GetComponent.<RectTransform>().sizeDelta = new Vector2 (25, 25);
								excludCard.GetComponent.<RectTransform>().localScale = new Vector3 (0.0625f, 0.0714f, 1);
								excludCard.transform.position = cardPosition;
								excludCard.transform.position.y += 0.35f; // -------------------------------------------------------- Ajuste da posição
								excludCard.transform.position.x += 0.22f; // -------------------------------------------------------- Ajuste da posição
								cardPosition.x += 0.6f; // -------------------------------------------------------------------------- Avança a posição para a proxíma carta
								deckMachineBehaviour.currentDeck = cardInstantiate; // ---------------------------------------------- Manda para o script de controle
								Debug.Log(newCard.transform.position);
								deckMachineBehaviour.allPosition[i] = newCard.transform.position;
							    i++; // --------------------------------------------------------------------------------------------- Roda o contaddor para o array
					}
					dontload = false;
					deckMachineBehaviour.cardPosition = cardPosition;
			} // ------------------------------------------------------------------------- FIM DO LAÇO FOR
} // ------------------------------------------------------------------------------------- FIM DA FUNÇÃO LOAD DECK



function OnTriggerEnter2D(coll: Collider2D){    
    if(coll.gameObject.tag == "mouse" ){ // --------------------------------------------- Verifica se o mouse está colidindo com esse objeto
    mouseIsHere = true;
    }
 }

 function OnTriggerExit2D(coll: Collider2D){    
    if(coll.gameObject.tag == "mouse" ){ // --------------------------------------------- Verifica se o mouse está colidindo com esse objeto
    mouseIsHere = false;
    }
 }

 /* ------------------------------- AREA PROS CÓDIGOS FRACASS .... TESTADOS E NÃO FUNCIONAIS ----------------------------------------------
 			var dontload : boolean;
			dontload = false;
			switch (cardName) {
			case "darf_card" :
			contObj = GameObject.Find("darf_card(Clone)/qtdCard(Clone)/txtQtdCard");
			contObj.GetComponent.<UnityEngine.UI.Text>().text += 1;
			dontload = true;
			break;
			case "examplecard" :
			dontload = true;
			break;
			case "warrior_card" :
			dontload = true;
			break;
			}
 */