#pragma strict
var life00:GameObject;
var life01:GameObject;
var life02:GameObject;
var life03:GameObject;
static var currentlife:int;


function Start () {
    currentlife = 3;
}

function Update () {

    lifeBehavior();
}

function lifeBehavior () {
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
