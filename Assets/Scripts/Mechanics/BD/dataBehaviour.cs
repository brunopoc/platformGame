using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dataBehaviour : MonoBehaviour {

    public bool  loadingOption;
    public bool  newgameOption;

    Scene activeScene;
    public Animator anima;
    static dataBehaviour instanceRef;

    public Text btnText1; //texto do botão 1 (para ativa-lo)
    public Text btnText2; //texto do botão 2 (para ativa-lo)
    public Text btnText3; //texto do botão 3 (para ativa-lo)

    public float duration; // Contador

    public bool  wantBack; // Voltar tela Inicial
    public bool  callFadeInAgain;
    public bool  goToWorldMap;

    public bool  savePlease; //Salve o game
    bool  hasDate1; //Booleana para verificar se possui data nos slot
    bool  hasDate2; //Booleana para verificar se possui data nos slot
    bool  hasDate3; //Booleana para verificar se possui data nos slot

    public int slotToSave;
    public int levelsFinish;
    public int crystalCollect;
    public int masterCrystals;
    public int currentLifes;

    public FadeInOutBehaviour FadeInOutBehaviour;
    public player_lifebar player_lifebar;
    public ScenesManager ScenesManager;


    /*
	    Tipos de dados:
	    Níveis Concluidos (levelFinish)
	    Cristais Coletados (crystalCollect)
	    Master Cristais Coletados (masterCrystals)
	    Vidas (currentLifes)

	    ----
	    Futuramente também registrar:
	    Armas Desbloqueadas[]
		    Canhão -> Desbloq
	    Cenas Desbloqueadas []
		    Allye - > Desbloq
	    Relacionamento[]
    */

    void Start (){

        FadeInOutBehaviour = GameObject.Find("back").GetComponent<FadeInOutBehaviour>(); // Isso precisa sair daqui
        activeScene = SceneManager.GetActiveScene();
        ScenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        ScenesManager.activeScene = SceneManager.GetActiveScene();

        slotToSave = 1;
	    duration = 0;

	    FadeInOutBehaviour.playfadeIn = true;

	    if(instanceRef == null){
	        instanceRef = this;
	        DontDestroyOnLoad(this);
	    }

        checkNLoadText();

    }

    void Update (){

	    checkToDestroy(); //Verifica a permanência entre as cenas e se precisa salvar

	    buttonIsPress(); //Verifica se o butão está pressionado 
	
	    checkFadeIn();
	    
 	
    }

    /*########################## FUNÇÕES PARA EXECUÇÃO DO SCRIPT ############################
    ########################### VERIFICAÇÕES RELACIONADO A PERMANENCIA DE DADOS #############
    */

    void checkNLoadText (){ //Checar os campos do slot e mandar o texto
	    if(PlayerPrefs.GetInt("Níveis Concluidos 1") >= 1){
	        levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 1");
	        crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 1");
	        masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 1");
	        currentLifes = PlayerPrefs.GetInt("Vidas 1");
	        btnText1.text = "Níveis Concluidos: " + levelsFinish + " \n" + "Cristais Coletados: " + crystalCollect + " \n" + "Master Crystals : " + masterCrystals;
	    }
	    if(PlayerPrefs.GetInt("Níveis Concluidos 2") >= 1){
	        levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 2");
	        crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 2");
	        masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 2");
	        currentLifes = PlayerPrefs.GetInt("Vidas 2");
	        btnText2.text = "Níveis Concluidos: " + levelsFinish + " \n" + "Cristais Coletados: " + crystalCollect + " \n" + "Master Crystals : " + masterCrystals;
	    }
	    if(PlayerPrefs.GetInt("Níveis Concluidos 3") >= 1){
	        levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 3");
	        crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 3");
	        masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 3");
	        currentLifes = PlayerPrefs.GetInt("Vidas 3");
	        btnText3.text = "Níveis Concluidos: " + levelsFinish + " \n" + "Cristais Coletados: " + crystalCollect + " \n" + "Master Crystals : " + masterCrystals;
	    }
    }

    void checkToDestroy (){ //Verifica permanência
		checkIfIsSave();
		if(activeScene.name == "Menu"){
			instanceRef = null;
			Destroy(this.gameObject);
		}
    }

    void checkFadeIn (){
	    if(callFadeInAgain == true){
 		    FadeInOutBehaviour.playfadeIn = true;
 		    callFadeInAgain = false;
 	    }
    }

    void checkIfIsSave (){
	    if(savePlease == true){ //Salve o Game
			saveDate();
			savePlease = false;
 	    }
    }

    void checkDate (){ //Verifica se tem algum conteúdo no Slot
	    if(PlayerPrefs.GetInt("Níveis Concluidos 1") >= 1){
		    hasDate1 = true;
	    }
	    if(PlayerPrefs.GetInt("Níveis Concluidos 2") >= 1){
		    hasDate2 = true;
	    }
	    if(PlayerPrefs.GetInt("Níveis Concluidos 3") >= 1){
		    hasDate3 = true;
	    }

    }

    void destroyDate (){
        switch(slotToSave){
	        case 1:
			    PlayerPrefs.SetInt("Níveis Concluidos 1", 0);
			    PlayerPrefs.SetInt("Cristais Coletados 1", 0);
			    PlayerPrefs.SetInt("Master Cristais Coletados 1", 0);
			    PlayerPrefs.SetInt("Vidas 1", 3);
			    PlayerPrefs.SetInt("HP 1", 3);
	        break;
	        case 2:
			    PlayerPrefs.SetInt("Níveis Concluidos 2", 0);
			    PlayerPrefs.SetInt("Cristais Coletados 2", 0);
			    PlayerPrefs.SetInt("Master Cristais Coletados 2", 0);
			    PlayerPrefs.SetInt("Vidas 2", 0);
			    PlayerPrefs.SetInt("HP 2", 3); 
	        break;
	        case 3:
			    PlayerPrefs.SetInt("Níveis Concluidos 3", 0);
			    PlayerPrefs.SetInt("Cristais Coletados 3", 0);
			    PlayerPrefs.SetInt("Master Cristais Coletados 3", 0);
			    PlayerPrefs.SetInt("Vidas 3", 0);
			    PlayerPrefs.SetInt("HP 3", 3); 
	        break;
	    }
    }

    void saveDate (){ //Lógica do Save APENAS inGame
	    switch(slotToSave){
			case 1:
				PlayerPrefs.SetInt("Níveis Concluidos 1", levelsFinish);
				PlayerPrefs.SetInt("Cristais Coletados 1", crystalCollect);
				PlayerPrefs.SetInt("Master Cristais Coletados 1", masterCrystals);
				PlayerPrefs.SetInt("Vidas 1", currentLifes);
				PlayerPrefs.SetInt("HP 1", player_lifebar.currentlife);
				savePlease = false; 
			break;
			case 2:
				PlayerPrefs.SetInt("Níveis Concluidos 2", levelsFinish);
				PlayerPrefs.SetInt("Cristais Coletados 2", crystalCollect);
				PlayerPrefs.SetInt("Master Cristais Coletados 2", masterCrystals);
				PlayerPrefs.SetInt("Vidas 2", currentLifes);
				PlayerPrefs.SetInt("HP 2", player_lifebar.currentlife);
				savePlease = false;  
			break;
			case 3:
				PlayerPrefs.SetInt("Níveis Concluidos 3", levelsFinish);
				PlayerPrefs.SetInt("Cristais Coletados 3", crystalCollect);
				PlayerPrefs.SetInt("Master Cristais Coletados 3", masterCrystals);
				PlayerPrefs.SetInt("Vidas 3", currentLifes);
				PlayerPrefs.SetInt("HP 3", player_lifebar.currentlife);
				savePlease = false;  
			break;
		}
    }
    void fadeOut (){
	    FadeInOutBehaviour.playfadeOut = true;
    }

    /* ############################## FUNÇÕES PARA OS BOTÕES DA CENA "SAVENINGAME" #######
    ################################# EXCLUSIVAMENTE PARA AÇÃO DOS BOTÕES ################
    */

    void buttonIsPress (){  //Verifica se algum botão foi pressionado
		    //O Jogo deve ser Salvo
		if(goToWorldMap == true){
			if(duration >= 1){
			    ScenesManager.sceneWorldMap = true;
			    duration = 0;
			    goToWorldMap = false;
			} else {
			    duration += Time.deltaTime;
			}
		}
		if(wantBack == true){
			if(duration >= 1){
				ScenesManager.sceneMenu = true;
				wantBack = false;
				duration = 0;
			} else {
			    duration += Time.deltaTime;
			}
		}
    }

    public void loadDate1 (){  //Lógica do loading APENAS para Botões
	    checkDate();
		if(loadingOption == true){
            levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 1");
            crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 1");
            masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 1");
            currentLifes = PlayerPrefs.GetInt("Vidas 1");
            slotToSave = 1;
            goToWorldMap =  true;
            loadingOption = false;
            newgameOption = false;
	    }
    }

    public void loadDate2 (){  //Lógica do loading APENAS para Botões
	    checkDate();
		if(loadingOption == true){
            levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 2");
            crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 2");
            masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 2");
            currentLifes = PlayerPrefs.GetInt("Vidas 2");
            slotToSave = 2;
            goToWorldMap =  true;
            loadingOption = false;
            newgameOption = false;
	    }
    }

    public void loadDate3 (){  //Lógica do loading APENAS para Botões
	    checkDate();
		if(loadingOption == true){
	        levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 3");
	        crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 3");
	        masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 3");
	        currentLifes = PlayerPrefs.GetInt("Vidas 3");
	        slotToSave = 3;
	        goToWorldMap =  true;
	        loadingOption = false;
	        newgameOption = false;
	    }
    }

    public void saveDate1 (){ //Lógica do Save APENAS para Botões
	    checkDate();
	    if (newgameOption == true && hasDate1 == false){
	        slotToSave = 1;
	        destroyDate();
	        fadeOut();
	        goToWorldMap = true;
	        loadingOption = false;
	        newgameOption = false;
		}else if(loadingOption == true) {
			loadDate1();
		} else if (newgameOption == true && hasDate1 == true){
			slotToSave = 1;
			anima.SetBool("transition", true);
		}
    }

    public void saveDate2 (){ //Lógica do Save APENAS para Botões
	    checkDate();
	    if (newgameOption == true && hasDate2 == false){
	        slotToSave = 2;
	        destroyDate();
	        fadeOut();
	        goToWorldMap = true;
	        loadingOption = false;
	        newgameOption = false;
		} else if(loadingOption == true) {
			loadDate2();
		} else if (newgameOption == true && hasDate2 == true){
			slotToSave = 2;
			anima.SetBool("transition", true);
		}
    }
    public void saveDate3 (){ //Lógica do Save APENAS para Botões
	    checkDate();
	    if (newgameOption == true && hasDate3 == false){
	        slotToSave = 3;
	        destroyDate();
	        fadeOut();
	        goToWorldMap = true;
	        loadingOption = false;
	        newgameOption = false;
		}else if(loadingOption == true) {
		    loadDate3();
		} else if (newgameOption == true && hasDate3 == true){
			slotToSave = 3;
			anima.SetBool("transition", true);
		}
    }


   public void ConfirmSaveIn (){ //Botão de salvar do dialogo se quer sobrescrever o dado
		switch(slotToSave){
		case 1:
			hasDate1 = false;
			PlayerPrefs.SetInt("Níveis Concluidos 1", 0);
			newgameOption = true;
			saveDate1();
		break;
		case 2:
			hasDate2 = false;
			PlayerPrefs.SetInt("Níveis Concluidos 2", 0);
			newgameOption = true;
			saveDate2();
		break;
		case 3:
			hasDate3 = false;
			PlayerPrefs.SetInt("Níveis Concluidos 3", 0);
			newgameOption = true;
			saveDate3();
		break;
		}
    }

    public void CancelSaveIn (){ //Botão de cancelar do dialogo se quer sobrescrever o dado
        anima.SetBool("transition", false);
        hasDate1 = false;
        hasDate2 = false;
        hasDate3 = false;
    }

    void backToMenu (){ //Botão de voltar ao menu inicial
        fadeOut();
        wantBack = true;
        ScenesManager.callFadeInAgain = true;
        loadingOption = false;
        newgameOption = false;
    }
}