using UnityEngine;
using System.Collections;

public class bullet_behavior : MonoBehaviour {

    GameObject bullet;
    float timecont = 0;
    public float velocidade;
    public bool  bullet_die;
    public float destroyItNow;

    void Start (){
        bullet = this.gameObject;
    }

    void Update (){
        if(bullet.transform.localScale.x == 1){ //Faz a bala Andar
            bullet.transform.Translate(velocidade*Time.deltaTime,0,0);
        } else{ //Inverte o sentido
            bullet.transform.Translate(-velocidade*Time.deltaTime,0,0);
        }

        timecont += Time.deltaTime; //Destrói a bala
        if(timecont > destroyItNow){
            Destroy(bullet);
        } 
        if(bullet_die == true){ //Se a variavél receber true a bala também é destruida
            Destroy(bullet);
            bullet_die = false;
        }
    }
}