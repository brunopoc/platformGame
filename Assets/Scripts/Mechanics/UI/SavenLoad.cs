using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavenLoad : MonoBehaviour
{
    DataBehaviour dataBehaviour;
    DataSavenload dataSaveNLoad;
    ScenesManager scenesManager;
    FadeBehaviour fadeInOutBehaviour;

    int slotToSave = 1;

    public void SaveLoadOne()
    {
        dataBehaviour = GameObject.Find("DateBehaviour").GetComponent<DataBehaviour>();
        scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        dataSaveNLoad = GameObject.Find("UI").GetComponent<DataSavenload>();
        slotToSave = 1;
        dataSaveNLoad.checkSaveData(slotToSave);
        dataBehaviour.slotToSave = slotToSave;
        dataBehaviour.loadData();
        if (!dataSaveNLoad.hasData)
        {
            scenesManager.sceneGamePlay = true;
        }
    }

    public void SaveLoadTwo()
    {
        dataBehaviour = GameObject.Find("DateBehaviour").GetComponent<DataBehaviour>();
        scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        dataSaveNLoad = GameObject.Find("UI").GetComponent<DataSavenload>();
        slotToSave = 2;
        dataSaveNLoad.checkSaveData(slotToSave);
        dataBehaviour.slotToSave = slotToSave;
        dataBehaviour.loadData();
        if (!dataSaveNLoad.hasData)
        {
            scenesManager.sceneGamePlay = true;
        }
    }

    public void SaveLoadThree()
    {
        dataBehaviour = GameObject.Find("DateBehaviour").GetComponent<DataBehaviour>();
        scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        dataSaveNLoad = GameObject.Find("UI").GetComponent<DataSavenload>();
        slotToSave = 3;
        dataSaveNLoad.checkSaveData(slotToSave);
        dataBehaviour.slotToSave = slotToSave;
        dataBehaviour.loadData();
        if (!dataSaveNLoad.hasData)
        {
            scenesManager.sceneGamePlay = true;
        }
    }

    public void BackToMenu()
    {
        scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        fadeInOutBehaviour = GameObject.Find("FadeInOut").GetComponent<FadeBehaviour>();
        dataBehaviour = GameObject.Find("DateBehaviour").GetComponent<DataBehaviour>();
        dataBehaviour.DestroyThis();
        scenesManager.sceneMenu = true;
        fadeInOutBehaviour.playfadeOut = true;
    }

    public void Confirm()
    {
        dataSaveNLoad = GameObject.Find("UI").GetComponent<DataSavenload>();
        scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        dataSaveNLoad.ConfirmSaveIn(slotToSave);
        scenesManager.sceneGamePlay = true;
    }

    public void Cancel()
    {
        dataSaveNLoad = GameObject.Find("UI").GetComponent<DataSavenload>();
        dataSaveNLoad.CancelSaveIn();
    }

}
