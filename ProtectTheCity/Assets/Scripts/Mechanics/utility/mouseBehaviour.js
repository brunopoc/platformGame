#pragma strict
static var mouseOnPosition:boolean;
var TheMouse:GameObject;
static var position:GameObject;
var sensibilidade:float;
var newMousePosition:Vector2;
static var onButtonPositionL: boolean;
static var onButtonPositionR: boolean;

static var moveCard : GameObject;
static var kingField: boolean;

function Start () {
    sensibilidade = 10;
    position = TheMouse;
    position.transform.position = Input.mousePosition ;
    // Cursor.visible = false;
}

function Update () {
    var ray: Vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    newMousePosition.x = ray.x;
    newMousePosition.y = ray.y;
    position.transform.position = newMousePosition;

}

function OnTriggerEnter2D(coll: Collider2D){
    if(coll.gameObject.tag == "card" && baseCardBehaviour.notMoveInY == false){
        mouseOnPosition = true;
        if(moveCard == null){
        moveCard = coll.gameObject;
        }
    }
    if(coll.gameObject.tag == "king"){
    kingField = true;
    }

    if(coll.gameObject.tag == "firstslot"){
    	onButtonPositionL = true;
    }
    if(coll.gameObject.tag == "lastslot"){
    	onButtonPositionR = true;
    }

}
    
function OnTriggerExit2D(coll: Collider2D){

        if(coll.gameObject.tag == "card" && coll.gameObject == moveCard){ 
            mouseOnPosition = false;
            moveCard = null;

        }

		if(coll.gameObject.tag == "king"){
	    	kingField = false;
	    }

	    if(coll.gameObject.tag == "firstslot"){
	    onButtonPositionL = false;
	    }
	     if(coll.gameObject.tag == "lastslot"){
		    onButtonPositionR = false;
	    }

}