using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_n_load : MonoBehaviour
{
    data_behaviour dataBehaviour;
    scenes_manager ScenesManager;
    fade_behaviour FadeInOutBehaviour;

    public void save_load_1 () {
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        dataBehaviour.slotToSave = 1;
        dataBehaviour.checkSaveData();
        dataBehaviour.loadData();
        if(!dataBehaviour.hasData) {
            ScenesManager.sceneGamePlay = true;
        }
    }

    public void save_load_2 () {
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        dataBehaviour.slotToSave = 2;
        dataBehaviour.checkSaveData();
        dataBehaviour.loadData();
        if(!dataBehaviour.hasData) {
            ScenesManager.sceneGamePlay = true;
        }
    }

    public void save_load_3 () {
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        dataBehaviour.slotToSave = 3;
        dataBehaviour.checkSaveData();
        dataBehaviour.loadData();
        if(!dataBehaviour.hasData) {
            ScenesManager.sceneGamePlay = true;
        }
    }

    public void back_to_menu () {
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        FadeInOutBehaviour = GameObject.Find("fade_in_out").GetComponent<fade_behaviour>();
        ScenesManager.sceneMenu = true;
        FadeInOutBehaviour.playfadeOut = true;
    }

}
