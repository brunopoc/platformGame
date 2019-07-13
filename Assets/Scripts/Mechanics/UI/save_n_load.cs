using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_n_load : MonoBehaviour
{
    data_behaviour dataBehaviour;
    scenes_manager ScenesManager;
    fade_behaviour FadeInOutBehaviour;

    public void save1 () {
        dataBehaviour = GameObject.Find("date_behaviour").GetComponent<data_behaviour>();
        dataBehaviour.slotToSave = 1;
    }
    public void back_to_menu () {
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        FadeInOutBehaviour = GameObject.Find("fade_in_out").GetComponent<fade_behaviour>();
        ScenesManager.sceneMenu = true;
        FadeInOutBehaviour.playfadeOut = true;
    }

}
