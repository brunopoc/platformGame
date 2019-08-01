using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scenes_manager : MonoBehaviour
{
    static scenes_manager instanceRef;

    public bool newGame;
    public bool loadGame;

    public bool sceneMenu;
    public bool sceneGamePlay;

    fade_behaviour FadeInOutBehaviour;
    data_savenload dataSaveNLoad;

    public Scene activeScene;

    void Start()
    {
        loadCurrentFade();
        permanentInGame();

        newGame = false;
        loadGame = false;
    }

    void Update()
    {
        sceneAction();
        sceneControl();
    }

    void loadCurrentFade()
    {
        FadeInOutBehaviour = GameObject.Find("fade_in_out").GetComponent<fade_behaviour>();
    }

    void callFadeOut()
    {
        FadeInOutBehaviour.playfadeOut = true;
    }

    void callFadeIn()
    {
        FadeInOutBehaviour.playfadeIn = true;
    }

    public void sceneControl()
    {
        if (activeScene != SceneManager.GetActiveScene())
        {
            activeScene = SceneManager.GetActiveScene();
            loadCurrentFade();
            switch (activeScene.name)
            {
                case "Menu":
                    sceneMenu = false;
                    callFadeIn();
                    break;
                case "SavenLoad":
                    dataSaveNLoad = GameObject.Find("UI").GetComponent<data_savenload>();
                    if (newGame == true)
                    {
                        dataSaveNLoad.newgameOption = true;
                        newGame = false;
                    }
                    if (loadGame == true)
                    {
                        dataSaveNLoad.loadingOption = true;
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

    void sceneAction()
    {
        if (sceneMenu == true)
        {
            callMenu();
            instanceRef = null;
            Destroy(this.gameObject);
        }
        if (sceneGamePlay == true)
        {
            callGamePlay();
        }
        if (newGame == true || loadGame == true)
        {
            callSavenLoad();
        }
    }

    void permanentInGame()
    {
        if (instanceRef == null)
        {
            DontDestroyOnLoad(this);
            instanceRef = this;
        }
    }

    void callMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void callGamePlay()
    {
        SceneManager.LoadScene("Gameplay");
    }
    void callSavenLoad()
    {
        SceneManager.LoadScene("SavenLoad");
    }
}