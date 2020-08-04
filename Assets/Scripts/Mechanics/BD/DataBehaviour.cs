using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataBehaviour : MonoBehaviour
{

    static DataBehaviour instanceRef;

    int levelsFinish;
    int crystalCollect;
    int masterCrystals;
    int currentLifes;

    public int slotToSave = 1;
    public PlayerLifebar player_lifebar;
    public ScenesManager scenesManager;

    void Start()
    {
        scenesManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(this);
        }
    }

    public void DestroyThis()
    {
        instanceRef = null;
        Destroy(this.gameObject);
    }

    public void saveDate(int slotToSave = 1, int levelsFinish = 0, int crystalCollect = 0, int masterCrystals = 0, int currentLifes = 0)
    {
        PlayerPrefs.SetInt("Níveis Concluidos " + slotToSave, levelsFinish);
        PlayerPrefs.SetInt("Cristais Coletados " + slotToSave, crystalCollect);
        PlayerPrefs.SetInt("Master Cristais Coletados " + slotToSave, masterCrystals);
        PlayerPrefs.SetInt("Vidas " + slotToSave, currentLifes);
        PlayerPrefs.SetInt("HP " + slotToSave, player_lifebar.currentlife);
    }

    public void loadData(int slotToSave = 1)
    {
        levelsFinish = PlayerPrefs.GetInt("Níveis Concluidos " + slotToSave);
        crystalCollect = PlayerPrefs.GetInt("Cristais Coletados " + slotToSave);
        masterCrystals = PlayerPrefs.GetInt("Master Cristais Coletados " + slotToSave);
        currentLifes = PlayerPrefs.GetInt("Vidas " + slotToSave);
    }
}