#pragma strict
var mouseOnPosition:boolean;
var position:GameObject;
var sensibilidade:float;
var newMousePosition:Vector2;


function Start () {
    sensibilidade = 10;
    position.transform.position = Input.mousePosition ;
    // Cursor.visible = false;
}

function Update () {
    var ray: Vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    newMousePosition.x = ray.x;
    newMousePosition.y = ray.y;
    position.transform.position = newMousePosition;

    if(mouseOnPosition == true && Input.GetMouseButtonDown(0)){
    	
    }
}

function OnCollisionEnter2D(coll: Collision2D){
    if(coll.gameObject.tag == "OnMenuEnter"){
        mouseOnPosition = true;
    }
}
    
    function OnCollisionExit2D(coll: Collision2D){
        if(coll.gameObject.tag == "OnMenuEnter") 
            mouseOnPosition = false;
}