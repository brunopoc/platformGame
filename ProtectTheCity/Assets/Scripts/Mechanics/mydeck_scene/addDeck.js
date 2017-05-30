#pragma strict
var mouseIsHere : boolean;
var deckAmount : int;
var cardLabel: GameObject;
var deckLabel : GameObject;
function Start () {
	
}

function Update () {
	if(mouseIsHere == true && Input.GetMouseButtonDown(0)){
		deckAmount = PlayerPrefs.GetInt("deck_Amount");
		if(deckAmount < 3){
			deckLabel = GameObject.Find("deckBehaviour");
			Instantiate(cardLabel);
			deckLabel.SetActive(false);
		   }
	}
}

function OnTriggerEnter2D(coll: Collider2D){    
    if(coll.gameObject.tag == "mouse" ){
    mouseIsHere = true;
    }
 }

 function OnTriggerExit2D(coll: Collider2D){    
    if(coll.gameObject.tag == "mouse" ){
    mouseIsHere = false;
    }
 }