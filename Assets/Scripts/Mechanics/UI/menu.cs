using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    scenes_manager ScenesManager;
    fade_behaviour FadeInOutBehaviour;

    public void NewGame()
    {
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        FadeInOutBehaviour = GameObject.Find("fade_in_out").GetComponent<fade_behaviour>();
        ScenesManager.newGame = true;
        FadeInOutBehaviour.playfadeOut = true;
    }

    public void LoadGame()
    {
        ScenesManager = GameObject.Find("scene_manager").GetComponent<scenes_manager>();
        FadeInOutBehaviour = GameObject.Find("fade_in_out").GetComponent<fade_behaviour>();
        ScenesManager.loadGame = true;
        FadeInOutBehaviour.playfadeOut = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
