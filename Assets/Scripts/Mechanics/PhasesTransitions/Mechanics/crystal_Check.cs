using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal_Check : MonoBehaviour {

    public GameObject letterZ;
    public bool  canFinish;
    public float duration = 0;

    public dataBehaviour dataBehaviour;
    public scenesManager ScenesManager;

    void Start (){

        dataBehaviour = GameObject.Find("DateBehaviour").GetComponent<dataBehaviour>();
        dataBehaviour.player_lifebar = GameObject.Find("Player").GetComponent<player_Lifebar>();

        ScenesManager = GameObject.Find("SceneManager").GetComponent<scenesManager>();
        ScenesManager.loadCurrentScene();
        ScenesManager.loadCurrentFade();
        ScenesManager.callFadeIn();

    }

    void Update (){
	    if (canFinish == true){
	        letterZ.SetActive(true);
	    }
	    if (canFinish == false){
	        letterZ.SetActive(false);
	    }
	    if (Input.GetKeyDown("z") && canFinish == true){
		    if(dataBehaviour.levelsFinish <= 1){
				dataBehaviour.levelsFinish = 1;
				dataBehaviour.crystalCollect = 1;
				dataBehaviour.masterCrystals = 1;
				dataBehaviour.currentLifes = 1;
				dataBehaviour.savePlease = true;
			} else {
				dataBehaviour.crystalCollect = 1;
				dataBehaviour.masterCrystals = 1;
				dataBehaviour.currentLifes = 1;
				dataBehaviour.savePlease = true;
			}
	    }
    }

    void OnTriggerEnter2D ( Collider2D coll  ){
        if(coll.gameObject.tag == "Player"){
            canFinish = true;
        }
    }
    void OnTriggerExit2D ( Collider2D coll  ){
        if(coll.gameObject.tag == "Player"){
            canFinish = false;
        }
    }
}