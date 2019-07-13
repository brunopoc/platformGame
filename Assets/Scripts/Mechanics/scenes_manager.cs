using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scenes_manager : MonoBehaviour {
    static scenes_manager instanceRef; //Instancia do próprio script (que possui o nome de ScenesManager)

    public bool  newGame; // Booleana para troca de cena
    public bool  loadGame; // Booleana para a troca de cena

    // ################ VARIÁVEIS QUE CONTROLAM A ENTRADA DE CENAS ##########################

    public bool sceneMenu;
    public bool sceneGamePlay;

    fade_behaviour FadeInOutBehaviour;
    data_behaviour dataBehaviour;

    public Scene activeScene;

    void Start (){ //Começa chamando o fadeIn e setando algumas variáveis
        loadCurrentFade();
        permanentInGame(); //Verifica se a cena deve permanecer em game;

	    newGame = false;
	    loadGame = false;
    }

    void Update (){
	    sceneAction();
        sceneControl();
    }

    void loadCurrentFade(){
        FadeInOutBehaviour = GameObject.Find("fade_in_out").GetComponent<fade_behaviour>();
    }

    void callFadeOut(){
        FadeInOutBehaviour.playfadeOut = true;
    }

    void callFadeIn(){
        FadeInOutBehaviour.playfadeIn = true;
    }

    // FUNÇÃO PARA MANDAR DADO ATRAVEZ DAS CENAS

    public void sceneControl() {
        if(activeScene != SceneManager.GetActiveScene()) {
            activeScene = SceneManager.GetActiveScene();
            loadCurrentFade();
            switch (activeScene.name){
                case "Menu":
                    sceneMenu = false;
                    callFadeIn();
                break;
                case "SavenLoad":
                    dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
                    if (newGame == true){
                        dataBehaviour.newgameOption = true;
                        newGame = false;
                    }
                    if (loadGame == true){
                        dataBehaviour.loadingOption = true;
                        loadGame = false;
                    }
                    callFadeIn();
                break;
                case "Gameplay":
                    sceneGamePlay = false;
                    callFadeIn();
                break;
            }
        }
    }

    /* ######################## FUNÇÕES PARA EXECUÇÃO DO SCRIPT ############################
    ########################### AS FUNÇÕES DESSE PONTO EM DIANTE SÃO RELACIONADAS A: #######
    ########################### VERIFICAR QUAL BOTÃO FOI PRESSIONADO #######################
    ########################### GARANTIR A PERMANENCIA DO SCRIPT DURANTE A TROCA DE CENAS ## 
    ########################### VERIFICAR A NECESSIDADE DE CHAMAR ALGUM MÉTODO #############*/  

    void sceneAction (){ // Verifica quais funções precisam ser chamadas
		if(sceneMenu == true){
            callMenu();
            instanceRef = null;
            Destroy(this.gameObject);
        }
		if(sceneGamePlay == true){
            callGamePlay();	
        }
        if(newGame == true || loadGame == true){
            callSavenLoad();
 	    }
    }

    void permanentInGame (){
	    if(instanceRef == null){
	        DontDestroyOnLoad(this);
            instanceRef = this;
        }
    }

    /* ---------------- CONTROLE DA TRANSIÇÃO DE CENAS -----------------------*/

    void callMenu (){
        SceneManager.LoadScene("Menu");
    }
    void callGamePlay(){
        SceneManager.LoadScene("Gameplay");
    }
    void callSavenLoad (){
        SceneManager.LoadScene("SavenLoad");
    }
}