using UnityEngine;
using System.Collections;

public class Enemy_Pip : MonoBehaviour {

    public Animator anima;
    public GameObject bullet;

    public int life;
    public float timeToAttack;
    public float countToAttack;
    public float timeToReceive;
    public float contAnimator;
    Rigidbody2D enemy;
    Vector3 bullet_position;

    void Start (){

        countToAttack = 0;
        timeToReceive = 0;
        enemy = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }

    void FixedUpdate (){
	    enemy = GetComponent<Rigidbody2D>();
	    if(countToAttack > timeToAttack){
	        anima.SetBool("shooting", true); //(-0.11ff + enemy.transform.position.x, -0.17ff + enemy.transform.position.y, 0);
	        bullet_position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0);
	        bullet.transform.localScale = new Vector3 (-1, 1, 1);
	        Instantiate(bullet, bullet_position, new Quaternion(0,0,0,0));
	        countToAttack = 0;
	    } else {
		    countToAttack += Time.deltaTime;
	    }
	    if(anima.GetBool("shooting") == true){
		    contAnimator += Time.deltaTime;
	    }
	    if(contAnimator > 1){
		    anima.SetBool("shooting", false);
		    contAnimator = 0;
	    }
	        if(life <= 0){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D ( Collider2D coll  ){
        if(coll.gameObject.tag == "HeroBullet"){
            life--;
            coll.gameObject.GetComponent<bullet_behavior>().bullet_die = true;
            timeToReceive = 0;
        }
    }
}