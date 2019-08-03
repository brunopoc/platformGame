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
    float moveSpeed = 2.8f;
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
        animatorPlayerControl();

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


    void animatorPlayerControl()
    {
        anime.SetFloat("walk", directionalInput.x);
        anime.SetBool("wall_jump", wallSliding);
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

        /*          
                    if (transform.localScale.x == 1)
                    {
                        anime.SetBool("mode_s", false);
                    }
                    else if (transform.localScale.x == -1)
                    {
                        anime.SetBool("mode_s", true);
                    }
                    if (Input.GetKeyDown("z"))
                    {
                        canShootAgain = false;
                        timeToShootAgain = 0;
                    }
                    if (canShootAgain == false)
                    {
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

                    if (canDash != true)
                    {
                        anime.SetBool("dash", true);
                    }
                    else
                    {
                        anime.SetBool("dash", false);
                    } */
    }
}