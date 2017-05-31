#pragma strict
var renderer1:SpriteRenderer;
var renderer2:SpriteRenderer;
var renderer3:SpriteRenderer;
var renderer4:SpriteRenderer;
function Start () {
	
}

function Update () {
	
	if(player_onmap.phase1_clear == true){
		renderer1.color = new Color(0f, 0f, 0f, 1f);
		renderer2.color = new Color(0f, 0f, 0f, 1f);
		renderer3.color = new Color(0f, 0f, 0f, 1f);
		renderer4.color = new Color(0f, 0f, 0f, 1f);
	}
}
