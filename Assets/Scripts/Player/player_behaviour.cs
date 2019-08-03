using UnityEngine;
using System.Collections;

[RequireComponent(typeof(controller2D))]
public class player_behaviour : MonoBehaviour
{

    public float maxJumpHeight = 1.5f;
    public float minJumpHeight = .5f;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 1;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float moveSpeed = 4;
    Vector3 velocity;
    float velocityXSmoothing;

    controller2D controller;
    player_lifebar player_lifebar;
    Animator anime;

    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;

    void Start()
    {
        player_lifebar = GameObject.Find("player").GetComponent<player_lifebar>();
        controller = GetComponent<controller2D>();
        anime = GetComponentInChildren<Animator>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {

        CalculateVelocity();
        HandleWallSliding();

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }

        public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    void HandleWallSliding()
    {
        wallSliding = false;
        wallDirX = (controller.collisions.left) ? -1 : 1;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;
            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {

                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
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