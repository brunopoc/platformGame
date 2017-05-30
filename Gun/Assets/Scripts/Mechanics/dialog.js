#pragma strict
var dialogText: UI.Text; //Campo de Texo do Hud
private var str: String; //Variável que o método utiliza para passar o texto para o componente de texto do HUD
var anima: Animator; //Animação que exibe o campo de dialogo

static var hasFinishCheck: boolean; //Booleana que verifica se o dialogo já acabou
var pressButtonZ: boolean; //Verifica se o botão Z foi pressionado e pula a conversa para o fim
var checkButtonZ: boolean;
var i: int;
var timeToCont: int;
var countDelay:float;
static var animaAction: String;


static var showDiolog: boolean; //Booleana que controlá quando  dialogo deve ser mostrado
static var MyMsg: String; //Variável que pode ser manipulado atravéz de outro script pra passar o dialogo
static var nameChar: String; //O Nome do personagem que está falando, caso não tenha nome deixar vazio


function Start () { //Comçe setando algumas váriaveis
	hasFinishCheck = false;
	MyMsg = null;
	str = null;
	anima = GetComponent(Animator);
	showDiolog = false;
	i = 0;
	timeToCont = 0;
	countDelay = 0;
	checkButtonZ = false;
}

function Update () { //Verifica constantemente se existe texto a exibir e se o botão z está sendo pressionado

	if(Input.GetKeyDown("z") && hasFinishCheck == false && showDiolog == true && checkButtonZ == true){
    pressButtonZ = true;
    checkButtonZ = false;
    }
    dialogText.text = str;
    if(MyMsg != null && showDiolog == true){
	AnimateText(MyMsg);
	}
	if( animaAction == "end" ){
       animationDialog("end");
       }
}

function animationDialog(animaAction: String){
	if(animaAction == "play"){
	anima.SetBool("playanima",true); //Começa a animação
	}
	if(animaAction == "end"){
	anima.SetBool("endanima",true);
	}
}

function AnimateText(strComplete: String){ //Função que exibe o texto
    MyMsg = null;
    animationDialog("play"); //Começa a animação

   	str = nameChar; // adiciona o nome do personagem
   	checkButtonZ = true;

    
    while( i < strComplete.Length && hasFinishCheck == false ) { //Enquanto houver informação na váriavel, ela é passada uma a uma para a 'str'
        	str += strComplete[i++];
			yield WaitForSeconds(0.15f);
			if(pressButtonZ == true) { //Se o botão Z é pressionado o texto pula para o final
			    str = nameChar + strComplete; //Adiciona o nome do personagem também
			    dialogText.text = str;
			    yield WaitForSeconds(0.2f);
			    hasFinishCheck = true;
			    showDiolog = false;
			    pressButtonZ = false;
			    i = 0;   
			    }
        	if(i >= strComplete.Length) { //Quando chegar na ultima ela avisa que terminou
					   	 hasFinishCheck = true;
					   	 showDiolog = false;
					   	 i = 0;       
					    }					    	 
        }
       
}
