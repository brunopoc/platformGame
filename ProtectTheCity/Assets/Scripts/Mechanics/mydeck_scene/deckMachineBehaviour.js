#pragma strict

// -------------------------------------------------------- Código do 24/05/2017 ------------------------------------------
// -------------------------------------------------------- Salvar as posições para ocorrer as instâncias em seus devidos lugares
static var allPosition : Vector3[]; // Váriavel que guarda a posição de todas as cartas


 // ------------------------------------------------------- >>> Váriaveis para as instancias das cartas e relativos <<< ----------------------------------------
 var contObj : GameObject;
 var newcontObj : GameObject;
 var excludCard : GameObject; // -------------------------- Botão que exclui -1 do contador das cartas
 var candNameBack : String;
 var contAmount : int;
 // ------------------------------------------------------- >>> Váriaveis para a função loadDeck() <<< ---------------------------------------------
var cardName : String;
static var cardPosition : Vector3;
var cardObj: GameObject;
var newCard: GameObject;
var cardCont : GameObject;
var i : int;

static var currentDeck : String[];
static var currentDeckPosition : Vector3[];
var dontHave : boolean;

var onDeckBehaviour : boolean;
var collDeckBehaviour : GameObject;

function Start () {



}


function Update () {
								if(Input.GetMouseButtonUp(0) && onDeckBehaviour == true) {
								addToDeck(collDeckBehaviour.name);
								}
}

function addToDeck (cardName : String) {
		var cardNameClone : String = cardName + "(Clone)";
		for(var i: int; i < currentDeck.Length; i++){ // ----------------- Um laço para percorrer o array com nomes de cartas em jogo
			if(cardNameClone == currentDeck[i]){ // --------------------------- Trecho que realiza a ação caso exista alguma carta com o mesmo nome
			contObj = GameObject.Find("cardBehaviour(Clone)");
			contObj = contObj.transform.Find(cardNameClone).gameObject;
			Debug.Log(contObj);
			contObj = contObj.transform.Find("qtdCard").gameObject;
			contObj = contObj.transform.Find("txtQtdCard").gameObject;
			contAmount = int.Parse(contObj.GetComponent.<UnityEngine.UI.Text>().text);
					if( contAmount < 4 ) {
					contAmount++;
					contObj.GetComponent.<UnityEngine.UI.Text>().text = contAmount.ToString();
					} else {
					Debug.Log("Você já atingiu o limite máxímo desssa carta!");
					}
			dontHave = true;
			} else if (dontHave == false) { // --------------------------- Se não houver nenhum card com o msm nome ele continua valendo false
					dontHave = false;
					}
		}

		if(dontHave == false) { // ---------------------------------------- Será necessário uma nova instância do card
				cardName = "Prefabs/" + "Deck" + "/Card/" + cardName; // ----------------------------------------- Monta o caminho onde estão os prefabs da carta
				var newCard : GameObject = Resources.Load(cardName) as GameObject; // --------------------------------------------- Carrega a carta de acordo com o caminho
				newCard = Instantiate(newCard, cardPosition, Quaternion.identity); // ---------------------------- Instancia a carta
				cardCont = Instantiate (cardCont, new Vector3(0,0,1), Quaternion.identity); // ------------------- Instãncia uma IMG com TEXT (todos componentes do CANVAS)
				cardCont.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false); // --- Tira o img do canvas
				cardCont.transform.position = cardPosition; // ---------------------------------------------------- Posicioina o obj junto com sua respectiva carta
				cardCont.transform.position.y += -0.35f; // -------------------------------------------------------- Ajuste da posição
				cardCont.transform.position.x += -0.22f; // -------------------------------------------------------- Ajuste da posição
				currentDeck[i] = newCard.name; // ---------------------------------------------- Manda para o array de controle
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
				i++;


		}

}

function OnTriggerEnter2D(coll: Collider2D){   
    if(coll.gameObject.tag == "card"){
   		onDeckBehaviour = true;
   		collDeckBehaviour = coll.gameObject;
    }
}

function OnTriggerExit2D(coll: Collider2D){
 	if(coll.gameObject.tag == "card"){
   		onDeckBehaviour = false;
   		collDeckBehaviour = coll.gameObject;
    }
}