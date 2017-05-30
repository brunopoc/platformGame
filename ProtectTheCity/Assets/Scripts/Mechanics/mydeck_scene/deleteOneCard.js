#pragma strict
var card : GameObject;
var contAmount : int;

function Start () {
	
}

function Update () {
}

function delete_0ne () {
				card = this.transform.parent.gameObject;
				card = card.transform.Find("qtdCard").gameObject;
				card = card.transform.Find("txtQtdCard").gameObject;
				contAmount = int.Parse(card.GetComponent.<UnityEngine.UI.Text>().text);
					if(contAmount < 1){
					Destroy(this.transform.parent.gameObject);
					} else {
				contAmount--;
				card.GetComponent.<UnityEngine.UI.Text>().text = contAmount.ToString();
				}
				for(var i: int; i < deckMachineBehaviour.currentDeck.Length; i++){
					if(card.name + "(Clone)" == deckMachineBehaviour.currentDeck[i]){
						
					}
				}
}
function OnTriggerEnter2D(coll: Collider2D){   
		if(coll.gameObject.tag == "mouse"){
		  		baseCardBehaviour.sceneCheck = "freeze";
		    } 
}

function OnTriggerExit2D(coll: Collider2D){   
		if(coll.gameObject.tag == "mouse"){
		  		baseCardBehaviour.sceneCheck = "my_decks";
		    } 
}