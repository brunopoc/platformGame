using UnityEngine;
using System.Collections;

public class bullet_behavior : MonoBehaviour {

    GameObject bullet;
    float timecont;
    float velocidade;
    public bool  bullet_die;
    float destroyItNow;
    public dialog dialog;
    public player_behavior player_behavior;

    void Start (){
        timecont = 0;
        dialog = GameObject.Find("Player").GetComponent<dialog>();
        player_behavior = GameObject.Find("Player").GetComponent<player_behavior>();
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

    void OnTriggerEnter2D ( Collider2D coll  ){
        if(coll.gameObject.tag == "Player"){       
    	    player_behavior.canDamage = false;
    	    bullet_die = true;
        }
    }
}