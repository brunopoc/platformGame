using UnityEngine;
using System.Collections;

public class player_lifebar : MonoBehaviour {

    public GameObject life00;
    public GameObject life01;
    public GameObject life02;
    public GameObject life03;
    public int currentlife;

    void Start (){
        currentlife = 3;
    }

    void Update (){
        lifeBehavior();
    }

    void lifeBehavior (){
        if(currentlife == 0){
            life00.SetActive(true);
            life01.SetActive(false);
            life02.SetActive(false);
            life03.SetActive(false);
        }
        if(currentlife == 1){
            life00.SetActive(false);
            life01.SetActive(true);
            life02.SetActive(false);
            life03.SetActive(false);
        }
        if(currentlife == 2){
            life00.SetActive(false);
            life01.SetActive(false);
            life02.SetActive(true);
            life03.SetActive(false);
        }
        if(currentlife == 3){
            life00.SetActive(false);
            life01.SetActive(false);
            life02.SetActive(false);
            life03.SetActive(true);
        }
    }
}