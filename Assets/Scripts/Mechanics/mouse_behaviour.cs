using UnityEngine;
using System.Collections;

public class mouse_behaviour : MonoBehaviour {

    public GameObject position;
    bool  mouseOnPosition;
    Vector2 newMousePosition;


    void Start (){
        position.transform.position = Input.mousePosition ;
        // Cursor.visible = false;
    }

    void Update (){
        Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        newMousePosition.x = ray.x;
        newMousePosition.y = ray.y;
        position.transform.position = newMousePosition;

        if(mouseOnPosition == true && Input.GetMouseButtonDown(0)){
    	
        }
    }

    void OnCollisionEnter2D ( Collision2D coll  ){
        if(coll.gameObject.tag == "OnMenuEnter"){
            mouseOnPosition = true;
        }
    }
    
        void OnCollisionExit2D ( Collision2D coll  ){
            if(coll.gameObject.tag == "OnMenuEnter") 
                mouseOnPosition = false;
    }
}