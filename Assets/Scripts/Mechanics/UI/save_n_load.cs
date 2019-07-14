using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_n_load : MonoBehaviour
{
    data_behaviour dataBehaviour;
    data_savenload dataSaveNLoad;
    scenes_manager ScenesManager;
    fade_behaviour FadeInOutBehaviour;

    int slotToSave = 1; 

    public void save_load_1 () {
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        dataSaveNLoad = GameObject.Find("UI").GetComponent<data_savenload>();
        slotToSave = 1;
        dataSaveNLoad.checkSaveData(slotToSave);
        dataBehaviour.slotToSave = slotToSave;
        dataBehaviour.loadData();
        if(!dataSaveNLoad.hasData) {
            ScenesManager.sceneGamePlay = true;
        }
    }

    public void save_load_2 () {
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        dataSaveNLoad = GameObject.Find("UI").GetComponent<data_savenload>();
        slotToSave = 2;
        dataSaveNLoad.checkSaveData(slotToSave);
        dataBehaviour.slotToSave = slotToSave;
        dataBehaviour.loadData();
        if(!dataSaveNLoad.hasData) {
            ScenesManager.sceneGamePlay = true;
        }
    }

    public void save_load_3 () {
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        dataSaveNLoad = GameObject.Find("UI").GetComponent<data_savenload>();
        slotToSave = 3;
        dataSaveNLoad.checkSaveData(slotToSave);
        dataBehaviour.slotToSave = slotToSave;
        dataBehaviour.loadData();
        if(!dataSaveNLoad.hasData) {
            ScenesManager.sceneGamePlay = true;
        }
    }

    public void back_to_menu () {
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        FadeInOutBehaviour = GameObject.Find("fade_in_out").GetComponent<fade_behaviour>();
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        dataBehaviour.DestroyThis();
        ScenesManager.sceneMenu = true;
        FadeInOutBehaviour.playfadeOut = true;
    }

    public void confirm () {
        dataSaveNLoad = GameObject.Find("UI").GetComponent<data_savenload>();
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        dataSaveNLoad.ConfirmSaveIn(slotToSave);
        ScenesManager.sceneGamePlay = true;
    }

    public void cancel () {
        dataSaveNLoad = GameObject.Find("UI").GetComponent<data_savenload>();
        dataSaveNLoad.CancelSaveIn();
    }

}
