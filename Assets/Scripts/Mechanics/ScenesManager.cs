using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

    UnityEngine.SceneManagement.SceneManager newScene; //Variável para a troca de cenas
    static ScenesManager instanceRef; //Instancia do próprio script (que possui o nome de ScenesManager)

    float duration; //Variavel para fazer contagem
    bool  newGame; // Booleana para troca de cena
    bool  loadGame; // Booleana para a troca de cena

    // ################ VARIÁVEIS QUE CONTROLAM A ENTRADA DE CENAS ##########################

    public bool  sceneMenu;
    public bool  sceneSavenLoad;
    public bool  sceneWorldMap;
    public bool  scenePhase1;

    public FadeInOutBehaviour FadeInOutBehaviour;
    public dataBehaviour dataBehaviour;

    public Scene activeScene;

    void Start (){ //Começa chamando o fadeIn e setando algumas variáveis
        loadCurrentFade();
        loadCurrentScene();
        FadeInOutBehaviour.playfadeIn = true;

	    duration = 0;
	    newGame = false;
	    loadGame = false;

		if(instanceRef == null){    //Também verifica se o objeto deve ser mantido
		    DontDestroyOnLoad(this);
		}
    }

    void Update (){

	    PermanentInGame(); //Verifica se a cena deve permanecer em game;

	    CheckButtonWasPress(); // Realiza a ação de acordo com o botão que foi pressionado

	    checkFunctions(); // Verifica quais funções precisam ser chamadas

    }

    public void loadCurrentFade(){
        FadeInOutBehaviour = GameObject.Find("back").GetComponent<FadeInOutBehaviour>();
    }

    public void callFadeOut(){
        FadeInOutBehaviour.playfadeOut = true;
    }

    public void callFadeIn(){
        FadeInOutBehaviour.playfadeIn = true;
    }

    // FUNÇÃO PARA MANDAR DADO ATRAVEZ DAS CENAS

    public void sceneControl() {
        switch (activeScene.name) {
            case "SavenLoad":
                dataBehaviour = GameObject.Find("DateBehaviour").GetComponent<dataBehaviour>();
                if (newGame == true){
                    dataBehaviour.newgameOption = true;
                    loadCurrentFade();
                    callFadeIn();
                    newGame = false;
                }
                if (loadGame == true){
                    dataBehaviour.loadingOption = true;
                    loadCurrentFade();
                    callFadeIn();
                    loadGame = false;
                }
            break;
        }
    }

    public void loadCurrentScene() {
        activeScene = SceneManager.GetActiveScene();
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
            callFadeIn();
            instanceRef = null;
            Destroy(this.gameObject);
        }
		if(sceneSavenLoad == true){
            sceneSavenLoad = false;
			callSavenLoad();
        }
		if(sceneWorldMap == true){
			sceneWorldMap = false;
			callWorldMap();
            callFadeIn();
        }
		if(scenePhase1 == true){
			scenePhase1 = false;
			callPhase1();
            callFadeIn();
        }

    }

    void CheckButtonWasPress (){

        if( newGame == true){
		    if(duration >= 1){
		 	    duration = 0;
		 	    callSavenLoad();
		    } else {
		 	    duration += Time.deltaTime;
		    }
 	    }
 	    if( loadGame == true){
		    if(duration >= 1){
		 	    duration = 0;
		 	    callSavenLoad();
		    } else {
		 	    duration += Time.deltaTime;
		    }
 	    }

    }

    void PermanentInGame (){
	    if(instanceRef == null){
	        DontDestroyOnLoad(this);
            instanceRef = this;
        }
    }

    /* ############## FUNÇÕES QUE ESTÃO PROGRAMADAS NOS BOTÕES DA TELA MENU ##########
    ################# ESSAS SÃO AS FUNÇÕES QUE FUNCIONAM APENAS NA CENA "MENU"########
    ################# TODAS ESTÃO SETADAS EM BOTÕES ################################## */


    public void NewGame (){
        callFadeOut();
	    newGame = true;
    }

    public void LoadGame (){
        callFadeOut();
        loadGame = true;
    }

    public void QuitGame (){
	    Application.Quit();
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