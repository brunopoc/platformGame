#pragma strict
var colorStart : Color;
var colorEnd : Color;
var duration : float = 1.0;
var t : float;
var renderoption : Renderer;
static var playfadeIn : boolean;
static var playfadeOut: boolean;

function Start () {
	colorStart = renderoption.material.color;
	colorEnd = Color(colorStart.r, colorStart.g, colorStart.b, 0);
	//renderoption.material.color.a = 0;
	playfadeIn = false;
	playfadeOut = false;
}

 function Update () {
 	if(playfadeIn == true){
 		callFadeIn();
 		playfadeIn = false;
 	}
 	if(playfadeOut == true){
 		callFadeOut();
 		playfadeOut = false;
 	}

}

function callFadeIn(){

	fadeIn();
}

function callFadeOut(){
	fadeOut();

}

function fadeIn(){
	for(t = 0; t < duration; t+= Time.deltaTime){

	renderoption.material.color = Color.Lerp(colorStart, colorEnd, t/duration);
	yield;

	}
}

function fadeOut(){
	for(t = 0; t < duration; t+= Time.deltaTime){

	renderoption.material.color = Color.Lerp(colorEnd, colorStart, t/duration);
	yield;

	}
}