using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dialog : MonoBehaviour {

Text dialogText; //Campo de Texo do Hud
private string str; //Variável que o método utiliza para passar o texto para o componente de texto do HUD
Animator anima; //Animação que exibe o campo de dialogo

public static bool  hasFinishCheck; //Booleana que verifica se o dialogo já acabou
public bool  pressButtonZ; //Verifica se o botão Z foi pressionado e pula a conversa para o fim
public bool  checkButtonZ;
public int i;
public int timeToCont;
public float countDelay;
public static string animaAction;


public bool  showDiolog; //Booleana que controlá quando  dialogo deve ser mostrado
public string MyMsg; //Variável que pode ser manipulado atravéz de outro script pra passar o dialogo
public string nameChar; //O Nome do personagem que está falando, caso não tenha nome deixar vazio


void Start (){ //Comçe setando algumas váriaveis
	hasFinishCheck = false;
	MyMsg = null;
	str = null;
	anima = GetComponent<Animator>();
	showDiolog = false;
	i = 0;
	timeToCont = 0;
	countDelay = 0;
	checkButtonZ = false;
}

void Update (){ //Verifica constantemente se existe texto a exibir e se o botão z está sendo pressionado

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

void animationDialog ( string animaAction  ){
	if(animaAction == "play"){
	anima.SetBool("playanima",true); //Começa a animação
	}
	if(animaAction == "end"){
	anima.SetBool("endanima",true);
	}
}

IEnumerator AnimateText ( string strComplete  ){ //Função que exibe o texto
    MyMsg = null;
    animationDialog("play"); //Começa a animação

   	str = nameChar; // adiciona o nome do personagem
   	checkButtonZ = true;

    
    while( i < strComplete.Length && hasFinishCheck == false ) { //Enquanto houver informação na váriavel, ela é passada uma a uma para a 'str'
        	str += strComplete[i++];
			yield return new WaitForSeconds(0.15f);
			if(pressButtonZ == true) { //Se o botão Z é pressionado o texto pula para o final
			    str = nameChar + strComplete; //Adiciona o nome do personagem também
			    dialogText.text = str;
			    yield return new WaitForSeconds(0.15f);
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

}