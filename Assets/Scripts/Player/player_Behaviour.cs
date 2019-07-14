using UnityEngine;
using System.Collections;

[RequireComponent (typeof (controller2D))]
public class player_behaviour :  MonoBehaviour {

    float jumpHeight = 1.5f;
    float timeToJumpApex = .4f;

    float gravity;
    float jumpVelocity;
    float moveSpeed = 6;

 
    Vector3 velocity;

    controller2D controller;
    player_lifebar player_lifebar;
    Animator anime;

    void Start (){ 
        player_lifebar = GameObject.Find("player").GetComponent<player_lifebar>();
        controller = GetComponent<controller2D>();
        anime = GetComponentInChildren<Animator>();
        gravity = -(2 * jumpHeight)/Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update (){

        if(controller.collisions.above || controller.collisions.below){
            velocity.y = 0;
        }


        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below){
            velocity.y = jumpVelocity;
        }

        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


/*     IEnumerator animatorPlayerControl (){
        if(canDamage == false || canMove == false){
		    anime.SetFloat("walk", 0); 
		    anime.SetBool("parado", true);
        }else{
		    if(Input.GetAxis("Horizontal") == 0){ 
		        anime.SetFloat("walk", 0);
	            anime.SetBool("parado", true);
		    }else if(Input.GetAxis("Horizontal") > 0){ 			
	            anime.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal")));
	            anime.SetBool("parado", false);
	        }else{
	            anime.SetFloat("walk", Input.GetAxis("Horizontal"));
	            anime.SetBool("parado", false);
	        }

	        if(player.transform.localScale.x == 1){
	            anime.SetBool("mode_s", false);
	        }else if(player.transform.localScale.x == -1){
	            anime.SetBool("mode_s", true);
	        }

            if(player.velocity.y > 1) {
		        anime.SetBool("jumping", true);
		        anime.SetBool("falling", false);
		    } else if (player.velocity.y < -1){
		        anime.SetBool("jumping", false);
		        anime.SetBool("falling", true);
		    } else if(player.velocity.y == 0) {
		        anime.SetBool("jumping", false);
		        anime.SetBool("falling", false);
		    }

		    if(Input.GetKeyDown("z")){
                canShootAgain = false;
                timeToShootAgain = 0;
            }

            if (canDash != true) {
                anime.SetBool("dash", true);
            }
            else {
                anime.SetBool("dash", false);
            }

            if (canShootAgain == false){
                anime.SetBool("fire", true);
                timeToShootAgain += Time.deltaTime;
                if (timeToShootAgain > 1.0f)
                {
                    canShootAgain = true;
                    anime.SetBool("fire", false);
                    timeToShootAgain = 0;
                    yield break;
                }
            }
            if (onWall == true) {
                anime.SetBool("wall_jump", true);
            }
            if (onWall == false)
            {
                anime.SetBool("wall_jump", false);
                canJumpWall = false;
            }
        }
        yield return 0;
    } */
}