#pragma strict
var newScene:UnityEngine.SceneManagement.SceneManager; //Variável para a troca de cenas
static var instanceRef : ScenesManager; //Instancia do próprio script (que possui o nome de ScenesManager)

var duration : float; //Variavel para fazer contagem
var newGame : boolean; // Booleana para troca de cena
var loadGame : boolean; // Booleana para a troca de cena

static var callFadeInAgain : boolean; //CHAMAR O FADE IN (SERVE APENAS PARA A CENA "MENU")

// ################ VARIÁVEIS QUE CONTROLAM A ENTRADA DE CENAS ##########################

static var sceneMenu : boolean;
static var sceneSavenLoad : boolean;
static var sceneWorldMap : boolean;
static var scenePhase1 : boolean;


function Start () { //Começa chamando o fadeIn e setando algumas variáveis

	FadeInOutBehaviour.playfadeIn = true;

	duration = 0;
	newGame = false;
	loadGame = false;

		if(instanceRef == null){ //Também verifica se o objeto deve ser mantido
		DontDestroyOnLoad(this);
		}
}

function Update () {

	PermanentInGame(); //Verifica se a cena deve permanecer em game;

	CheckButtonWasPress(); // Realiza a ação de acordo com o botão que foi pressionado

	CallFadeInd(); // Chama o FadeIn quando necessário

	checkFunctions(); // Verifica quais funções precisam ser chamadas
 	
}

/* ######################## FUNÇÕES PARA EXECUÇÃO DO SCRIPT ############################
########################### AS FUNÇÕES DESSE PONTO EM DIANTE SÃO RELACIONADAS A: #######
########################### CHAMAR FADEIND/OUT #########################################
########################### VERIFICAR QUAL BOTÃO FOI PRESSIONADO #######################
########################### GARANTIR A PERMANENCIA DO SCRIPT DURANTE A TROCA DE CENAS ## 
########################### VERIFICAR A NECESSIDADE DE CHAMAR ALGUM MÉTODO #############*/

function checkFunctions(){ // Verifica quais funções precisam ser chamadas

		if(sceneMenu == true){
			sceneMenu = false;
			callMenu();
		}
		if(sceneSavenLoad == true){
			sceneSavenLoad = false;
			callSavenLoad();
		}
		if(sceneWorldMap == true){
			sceneWorldMap = false;
			callWorldMap();
		}
		if(scenePhase1 == true){
			scenePhase1 = false;
			callPhase1();
		}

}

function CallFadeInd(){
	if(callFadeInAgain == true){
	 		FadeInOutBehaviour.playfadeIn = true;
	 		callFadeInAgain = false;
	 	}
}

function CheckButtonWasPress(){

if( newGame == true){
		 		if(duration >= 1){
		 			newGame = false;
		 			dataBehaviour.newgameOption = true;
		 			duration = 0;
		 			callSavenLoad();
		 		} else {
		 			duration += Time.deltaTime;
		 		}
 	}
 	if( loadGame == true){
		 		if(duration >= 1){
		 			loadGame = false;
		 			dataBehaviour.loadingOption = true;
		 			duration = 0;
		 			callSavenLoad();
		 		} else {
		 			duration += Time.deltaTime;
		 		}
 	}

}

function PermanentInGame(){
if(newScene.GetActiveScene().name == "SavenLoad"){
		instanceRef = this;
		}

		if(newScene.GetActiveScene().name == "Menu" && instanceRef != null){
		callFadeInAgain = true;
		instanceRef = null;
		Destroy(this.gameObject);
		}
		if(instanceRef == null){
		DontDestroyOnLoad(this);
		}
}

/* ############## FUNÇÕES QUE ESTÃO PROGRAMADAS NOS BOTÕES DA TELA MENU ##########
################# ESSAS SÃO AS FUNÇÕES QUE FUNCIONAM APENAS NA CENA "MENU"########
################# TODAS ESTÃO SETADAS EM BOTÕES ################################## */


function NewGame(){
	newGame = true;
	dataBehaviour.callFadeInAgain = true;
}
function LoadGame(){
	loadGame = true;
	dataBehaviour.callFadeInAgain = true;
}

function fadeOut(){
	FadeInOutBehaviour.playfadeOut = true;
}

function QuitGame(){
				if(duration >= 1){
				Application.Quit();
				duration = 0;
			 		} else {
		 			duration += Time.deltaTime;
		 		}
}

/* ---------------- CONTROLE DA TRANSIÇÃO DE CENAS -----------------------*/

function callWorldMap(){
	newScene.LoadScene("worldmap");
}
function callPhase1(){
	newScene.LoadScene("phase_1");
}
function callMenu(){
	newScene.LoadScene("Menu");
}
function callSavenLoad(){
	newScene.LoadScene("SavenLoad");
}