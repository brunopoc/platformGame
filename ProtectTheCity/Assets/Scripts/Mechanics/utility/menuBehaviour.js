#pragma strict
var newScene:UnityEngine.SceneManagement.SceneManager; //Variável para a troca de cenas
function Start () {
	
}

function Update () {
	
}

function behaviour_inMyDeck(){
		if(newScene.GetActiveScene() == "my_decks"){
		baseCardBehaviour.sceneCheck = "my_decks";
		}
}

function campaing() {
newScene.LoadScene("world_map");
}

function load_lvl_1_1() {
newScene.LoadScene("phase_1-1");
}

function menu() {
newScene.LoadScene("menu");
}

function call_myDeck(){
newScene.LoadScene("my_decks");
}

function call_quit(){
Application.Quit();
}