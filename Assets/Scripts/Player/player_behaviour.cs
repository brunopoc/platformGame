using UnityEngine;
using System.Collections;

[RequireComponent(typeof(controller2D))]
public class player_behaviour : MonoBehaviour
{

    public float maxJumpHeight = 1.5f;
    public float minJumpHeight = .5f;
    public float timeToJumpApex = .4f;
    public float dashSpeed = 30;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 1;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    int bullet_max = 3;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float moveSpeed = 2.8f;
    float velocityXSmoothing;
    Vector3 velocity;

    Animator anime;
    controller2D controller;
    player_lifebar player_lifebar;
    bullet_behavior bullet_instance;
    public bullet_behavior bullet;

    Vector2 directionalInput;
    bool wallSliding;
    bool dashing;
    int wallDirX;
    float timeToDash;
    public float initTimeToDash;

    void Start()
    {
        player_lifebar = GameObject.Find("player").GetComponent<player_lifebar>();
        bullet_instance = null;
        timeToDash = initTimeToDash;
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
        animatorPlayerControl();
        HandleDashing();

        transform.localScale = new Vector3(controller.collisions.faceDir, 1, 1);
        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (!controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }

    public void Shoot()
    {
        if (GameObject.FindGameObjectsWithTag("HeroBullet").Length < bullet_max)
        {
            Vector3 bullet_position = (transform.localScale.x == 1) ? new Vector3(0.41f, 0.15f, 0) : new Vector3(-0.41f, 0.15f, 0);
            bullet_position += transform.position;
            bullet_instance = Instantiate(bullet, bullet_position, Quaternion.identity);
            bullet_instance.velocity = (transform.localScale.x == 1) ? new Vector3(10, 0, 0) : new Vector3(-10, 0, 0);
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

    public void Dash()
    {
        if (controller.collisions.below && velocity.x < 10)
        {
            dashing = true;
        }
    }

    void HandleDashing(){
        if(dashing){
            int faceDir = controller.collisions.faceDir;
            if(timeToDash <= 0){
                dashing = false;
                timeToDash = initTimeToDash;
            } else {
                timeToDash -= Time.deltaTime;
                velocity.x = Mathf.SmoothDamp(velocity.x, dashSpeed * faceDir, ref velocityXSmoothing, accelerationTimeGrounded);
            }
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
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below || dashing) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }


    void animatorPlayerControl()
    {
        anime.SetFloat("walk", directionalInput.x);
        anime.SetBool("wall_jump", wallSliding);
        anime.SetBool("dash", dashing);
        anime.SetBool("fire", (bullet_instance) ? true : false);
        anime.SetBool("parado", directionalInput.x == 0);
        anime.SetBool("mode_s", controller.collisions.faceDir == -1);

        if (controller.collisions.below)
        {
            anime.SetBool("jumping", false);
            anime.SetBool("falling", false);
        }
        else
        {
            anime.SetBool("jumping", (velocity.y >= 0));
            anime.SetBool("falling", (velocity.y < 0));
        }
    }
}