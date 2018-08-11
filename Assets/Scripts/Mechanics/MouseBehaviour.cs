using UnityEngine;
using System.Collections;

public class MouseBehaviour : MonoBehaviour {

bool  mouseOnPosition;
GameObject position;
float sensibilidade;
Vector2 newMousePosition;


void Start (){
    sensibilidade = 10;
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