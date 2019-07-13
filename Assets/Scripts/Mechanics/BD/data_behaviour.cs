using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class data_behaviour : MonoBehaviour {

	public bool newgameOption;
	public bool loadingOption;

    public Animator anima;
    static data_behaviour instanceRef;

    public Text btnText1; //texto do botão 1 (para ativa-lo)
    public Text btnText2; //texto do botão 2 (para ativa-lo)
    public Text btnText3; //texto do botão 3 (para ativa-lo)

    public bool  savePlease; //Salve o game
    public bool  hasData; //Booleana para verificar se possui data nos slot

    public float duration = 0; // Contador
    public int slotToSave = 1;
    public int levelsFinish;
    public int crystalCollect;
    public int masterCrystals;
    public int currentLifes;

    public player_lifebar player_lifebar;
    public scenes_manager ScenesManager;

    void Start (){
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        if (instanceRef == null){
	        instanceRef = this;
	        DontDestroyOnLoad(this);
	    }
        checkNLoadText();
    }
    void Update (){
        checkIfIsSave();
		if(loadingOption) {
			loadData();
		}
    }
    void checkNLoadText (){
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
    void checkToDestroy (){
		checkIfIsSave();
		instanceRef = null;
		Destroy(this.gameObject);
    }
    void checkIfIsSave (){
	    if(savePlease == true){
			saveDate();
			savePlease = false;
 	    }
    }

    void checkDate (){
		switch(slotToSave){
			case 1:
				if(PlayerPrefs.GetInt("Níveis Concluidos 1") >= 1){
					hasData = true;
				}
			break;
			case 2:
				if(PlayerPrefs.GetInt("Níveis Concluidos 2") >= 1){
					hasData = true;
				}
			break;
			case 3:
				if(PlayerPrefs.GetInt("Níveis Concluidos 3") >= 1){
					hasData = true;
				}
			break; 
		}
    }

    void destroyDate (){
		levelsFinish = 0;
		crystalCollect = 0;
		masterCrystals = 0;
		currentLifes = 0;
		saveDate ();
    }

    void saveDate (){
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

    public void loadData(){
		checkDate();	
		if (loadingOption){
			switch(slotToSave){
				case 1:
					levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 1");
					crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 1");
					masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 1");
					currentLifes = PlayerPrefs.GetInt("Vidas 1");	
					savePlease = false; 
				break;
				case 2:
					levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 2");
					crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 2");
					masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 2");
					currentLifes = PlayerPrefs.GetInt("Vidas 2");
					savePlease = false;  
				break;
				case 3:
					levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 3");
					crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 3");
					masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 3");
					currentLifes = PlayerPrefs.GetInt("Vidas 3");
					savePlease = false;  
				break;
			}
		}	
    }

    public void checkSaveData(){
	    checkDate();
	    if (hasData && newgameOption){
			anima.SetBool("transition", true);
		}
    }


    public void ConfirmSaveIn (){
		switch(slotToSave){
		case 1:
			hasData = false;
			PlayerPrefs.SetInt("Níveis Concluidos 1", 0);
		break;
		case 2:
			hasData = false;
			PlayerPrefs.SetInt("Níveis Concluidos 2", 0);
		break;
		case 3:
			hasData = false;
			PlayerPrefs.SetInt("Níveis Concluidos 3", 0);
		break;
		}
    }

    public void CancelSaveIn (){
        anima.SetBool("transition", false);
    }
}