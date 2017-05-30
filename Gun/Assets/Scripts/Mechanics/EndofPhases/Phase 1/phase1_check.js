#pragma strict
var canFinish : boolean;
var letterZ: GameObject;
var duration : float;


function Start () {
	duration = 0;
}

function Update () {
	if (canFinish == true){
	letterZ.SetActive(true);
	}
	if (canFinish == false){
	letterZ.SetActive(false);
	}
	if (Input.GetKeyDown("z") && canFinish == true){
								if(dataBehaviour.levelsFinish <= 1){
										dataBehaviour.levelsFinish = 1;
										dataBehaviour.crystalCollect = 1;
										dataBehaviour.masterCrystals = 1;
										dataBehaviour.currentLifes = 1;
										dataBehaviour.savePlease = true;
										dataBehaviour.goToWorldMap = true;
										} else {
									    dataBehaviour.crystalCollect = 1;
										dataBehaviour.masterCrystals = 1;
										dataBehaviour.currentLifes = 1;
										dataBehaviour.savePlease = true;
										dataBehaviour.goToWorldMap = true;
										}

	}
}

function OnTriggerEnter2D (coll: Collider2D){
    if(coll.gameObject.tag == "Player"){
        canFinish = true;
    }
}
function OnTriggerExit2D (coll: Collider2D){
    if(coll.gameObject.tag == "Player"){
        canFinish = false;
    }
}
