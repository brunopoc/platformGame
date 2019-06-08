using UnityEngine;
using System.Collections;

public class jennyDialog : MonoBehaviour {

    int contScene;
    int contDialog;

    public player_behaviour player_behavior;
    public dialog dialog;

    Animator anima; //Animação que exibe o campo de dialogo

    void Start (){
	    contDialog = 0;
	    anima = GetComponent<Animator>();
        player_behavior = GameObject.Find("Player").GetComponent<player_behaviour>();
        dialog = GameObject.Find("HUD").GetComponent<dialog>();
    }

    void Update (){
		sceneOne ();
    }

    void OnTriggerEnter2D ( Collider2D coll  ){
        if(coll.gameObject.tag == "Player" && contDialog == 0){
            contDialog = 1;
            player_behavior.canMove = false;
        }
    }

    void sceneOne (){
	    if(contDialog == 1){
	        dialog.showDiolog = true;
		    dialog.MyMsg = "Ah, quase acreditei que você hávia desistido";
		    contDialog = 2;
	    }
	    if(contDialog == 2 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    dialog.showDiolog = true;
		    dialog.hasFinishCheck = false;
		    dialog.MyMsg = "Se bem que seria melhor se fosse assim, não quero perder você também.";
		    contDialog = 3;
		    anima.SetBool("fumando", true);
	    }
	    if(contDialog == 3 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    dialog.showDiolog = true;
		    dialog.hasFinishCheck = false;
		    dialog.MyMsg = "Mas você não vai mudar de idéia não é mesmo? ...";
		    contDialog = 4;

	    }
	    if(contDialog == 4 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    dialog.showDiolog = true;
		    dialog.hasFinishCheck = false;
		    dialog.MyMsg = "Pois bem, é aqui que detectei uma energia além do normal,";
		    contDialog = 5;
		    anima.SetBool("fumando", false);
		    anima.SetBool("movfumando", true);
	    }
	    if(contDialog == 5 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    dialog.showDiolog = true;
		    dialog.hasFinishCheck = false;
		    dialog.MyMsg = " e também noto que o pessoal da Green Skull também estão por aqui.";
		    contDialog = 6;
		    anima.SetBool("movfumando", false);
		    anima.SetBool("fumando", true);
	    }
	    if(contDialog == 6 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    dialog.showDiolog = true;
		    dialog.hasFinishCheck = false;
		    dialog.MyMsg = "Provavelmente um daqueles valiosos cristais que você procura está por aqui, mas vai precisar ter cuidado. ";
		    contDialog = 7;
		    anima.SetBool("movfumando", false);
		    anima.SetBool("fumando", true);
	    }
	    if(contDialog == 7 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    dialog.showDiolog = true;
		    dialog.hasFinishCheck = false;
		    dialog.MyMsg = "Não preciso lembra-lo sobre o poder de fogo da Green Skull, certo?";
		    contDialog = 8;
		    anima.SetBool("fumando", false);
		    anima.SetBool("parado", true);
	    }
	    if(contDialog == 8 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    dialog.showDiolog = true;
		    dialog.hasFinishCheck = false;
		    dialog.MyMsg = "Vou estar pela cidade por um tempo, caso tenha problemas me procure.";
		    contDialog = 9;
	    }
	    if(contDialog == 9 && Input.GetKeyDown("z") && dialog.hasFinishCheck == true){
		    contDialog = 10;
		    dialog.animaAction = "end";
		    player_behavior.canMove = true;
		    anima.SetBool("pulando", true);
	    }
    }
}