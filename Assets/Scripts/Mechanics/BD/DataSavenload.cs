using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSavenload : MonoBehaviour
{
    int levelsFinish;
    int crystalCollect;
    int masterCrystals;
    int currentLifes;
    Text btnText1;
    Text btnText2;
    Text btnText3;

    public Animator anima;
    public bool newgameOption;
    public bool loadingOption;
    public bool hasData;

    void Start()
    {
        checkNLoadText();
        btnText1 = GameObject.Find("save1").GetComponentInChildren<Text>();
        btnText2 = GameObject.Find("save2").GetComponentInChildren<Text>();
        btnText3 = GameObject.Find("save3").GetComponentInChildren<Text>();
    }

    void checkDate(int slotToSave = 1)
    {
        if (PlayerPrefs.GetInt("Níveis Concluidos " + slotToSave) >= 1)
        {
            hasData = true;
        }
    }

    void checkNLoadText()
    {
        if (PlayerPrefs.GetInt("Níveis Concluidos 1") >= 1)
        {
            levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 1");
            crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 1");
            masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 1");
            currentLifes = PlayerPrefs.GetInt("Vidas 1");
            btnText1.text = "Níveis Concluidos: " + levelsFinish + " \n" + "Cristais Coletados: " + crystalCollect + " \n" + "Master Crystals : " + masterCrystals;
        }
        if (PlayerPrefs.GetInt("Níveis Concluidos 2") >= 1)
        {
            levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 2");
            crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 2");
            masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 2");
            currentLifes = PlayerPrefs.GetInt("Vidas 2");
            btnText2.text = "Níveis Concluidos: " + levelsFinish + " \n" + "Cristais Coletados: " + crystalCollect + " \n" + "Master Crystals : " + masterCrystals;
        }
        if (PlayerPrefs.GetInt("Níveis Concluidos 3") >= 1)
        {
            levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos 3");
            crystalCollect = PlayerPrefs.GetInt("Cristais Coletados 3");
            masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados 3");
            currentLifes = PlayerPrefs.GetInt("Vidas 3");
            btnText3.text = "Níveis Concluidos: " + levelsFinish + " \n" + "Cristais Coletados: " + crystalCollect + " \n" + "Master Crystals : " + masterCrystals;
        }
    }

    public void checkSaveData(int slotToSave = 1)
    {
        checkDate(slotToSave);
        if (hasData && newgameOption)
        {
            anima.SetBool("transition", true);
        }
    }


    public void ConfirmSaveIn(int slotToSave = 1)
    {
        hasData = false;
        PlayerPrefs.SetInt("Níveis Concluidos " + slotToSave, 0);
        PlayerPrefs.SetInt("Cristais Coletados " + slotToSave, 0);
        PlayerPrefs.SetInt("Master Cristais Coletados " + slotToSave, 0);
        PlayerPrefs.SetInt("Vidas " + slotToSave, 0);
        PlayerPrefs.SetInt("HP " + slotToSave, 3);
    }

    public void CancelSaveIn()
    {
        anima.SetBool("transition", false);
    }
}
