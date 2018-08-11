using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesManager : MonoBehaviour {

    UnityEngine.SceneManagement.SceneManager newScene; //Variável para a troca de cenas
    static ScenesManager instanceRef; //Instancia do próprio script (que possui o nome de ScenesManager)

    float duration; //Variavel para fazer contagem
    bool  newGame; // Booleana para troca de cena
    bool  loadGame; // Booleana para a troca de cena

    public bool  callFadeInAgain; //CHAMAR O FADE IN (SERVE APENAS PARA A CENA "MENU")

    // ################ VARIÁVEIS QUE CONTROLAM A ENTRADA DE CENAS ##########################

    public bool  sceneMenu;
    public bool  sceneSavenLoad;
    public bool  sceneWorldMap;
    public bool  scenePhase1;

    public FadeInOutBehaviour FadeInOutBehaviour;
    public dataBehaviour dataBehaviour;

    Scene activeScene;



    void Start (){ //Começa chamando o fadeIn e setando algumas variáveis
        FadeInOutBehaviour = GameObject.Find("back").GetComponent<FadeInOutBehaviour>(); // Isso precisa sair daqui
        dataBehaviour = GameObject.Find("DateBehaviour").GetComponent<dataBehaviour>();
        activeScene = SceneManager.GetActiveScene();
        FadeInOutBehaviour.playfadeIn = true;

	    duration = 0;
	    newGame = false;
	    loadGame = false;

		    if(instanceRef == null){ //Também verifica se o objeto deve ser mantido
		    DontDestroyOnLoad(this);
		    }
    }

    void Update (){

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

    void checkFunctions (){ // Verifica quais funções precisam ser chamadas

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

    void CallFadeInd (){
	    if(callFadeInAgain == true){
	 		    FadeInOutBehaviour.playfadeIn = true;
	 		    callFadeInAgain = false;
	 	    }
    }

    void CheckButtonWasPress (){

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

    void PermanentInGame (){
    if(activeScene.name == "SavenLoad"){
		    instanceRef = this;
		    }

		    if(activeScene.name == "Menu" && instanceRef != null){
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


    void NewGame (){
	    newGame = true;
	    dataBehaviour.callFadeInAgain = true;
    }
    void LoadGame (){
	    loadGame = true;
	    dataBehaviour.callFadeInAgain = true;
    }

    void fadeOut (){
	    FadeInOutBehaviour.playfadeOut = true;
    }

    void QuitGame (){
				    if(duration >= 1){
				    Application.Quit();
				    duration = 0;
			 		    } else {
		 			    duration += Time.deltaTime;
		 		    }
    }

    /* ---------------- CONTROLE DA TRANSIÇÃO DE CENAS -----------------------*/

    void callWorldMap (){
        SceneManager.LoadScene("worldmap");
    }
    void callPhase1 (){
        SceneManager.LoadScene("phase_1");
    }
    void callMenu (){
        SceneManager.LoadScene("Menu");
    }
    void callSavenLoad (){
        SceneManager.LoadScene("SavenLoad");
    }
}