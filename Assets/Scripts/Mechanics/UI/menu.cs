using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    ScenesManager ScenesManager;
    FadeBehaviour FadeInOutBehaviour;

    public void NewGame()
    {
        ScenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        FadeInOutBehaviour = GameObject.Find("FadeInOut").GetComponent<FadeBehaviour>();
        ScenesManager.newGame = true;
        FadeInOutBehaviour.playfadeOut = true;
    }

    public void LoadGame()
    {
        ScenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        FadeInOutBehaviour = GameObject.Find("FadeInOut").GetComponent<FadeBehaviour>();
        ScenesManager.loadGame = true;
        FadeInOutBehaviour.playfadeOut = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
