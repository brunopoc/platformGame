#pragma strict

var standartPosition : Vector3;
var deckInstantiaed : GameObject[];
var deckAmount : int;
var count : int;
var instAdd : boolean;

var deckAddButton : GameObject;
var parentOfLabel : Transform;


function Start () {

	PlayerPrefs.SetInt("deck_Amount", 2);
	deckAmount = PlayerPrefs.GetInt("deck_Amount");
	standartPosition = new Vector3(-0.7f, 1.35f, 1);
	count = 0;
	instAdd = false;


}


function Update () {

	checkAmount();

}

function checkAmount(){
	while(deckAmount != 0){
	deckAmount--;
	deckInstantiaed[count] = Instantiate(Resources.Load("Prefabs/Mechanics/decks/deckObj") as GameObject, standartPosition, Quaternion.identity);
	deckInstantiaed[count].transform.parent = parentOfLabel;
	deckInstantiaed[count].GetComponent.<openDeck>().deckId = count;
	standartPosition.x += 0.6;
	count++;
	}
	if(instAdd == false){
	instAdd = true;
	deckInstantiaed[count] = Instantiate(deckAddButton, standartPosition, Quaternion.identity);
	deckInstantiaed[count].transform.parent = parentOfLabel;
	count++;
	}
}


