using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D myRigidBody;

    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Transform standPoint;
    private bool canStand;

    public Animator anim;

    public BulletController shotToFire;
    public Transform shotPoint;
    public Transform shotUpPoint;

    private bool canDoubleJump;

    public float dashSpeed, dashTime;
    private float dashCounter;

    public SpriteRenderer mySpriteRenderer, afterImage;
    public float afterImageLifeTime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;

    public GameObject standing, ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    public Transform bombPoint;
    public GameObject bomb;

    private PlayerAbilityTracker abilities;

    public bool canMove;

    public float lookUpBound;
    private bool isLookingUp;

    public float fireRate;
    private float shotCounter;

    private void Awake()//~Note~ "Awake" is a prebuild function that runs before the start function.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//"oh look theres already an instance let me just destroy my self to prevent two players from spawning"
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        abilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0)
        {

            //limits how frequent player can dash
            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;

            }
            else
            {
                // enables dash ability
                if ((Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.P)) && standing.activeSelf && abilities.canDash)//~Note~ "Fire2" has a default setting linked to right click on mouse
                {
                    dashCounter = dashTime;

                    ShowAfterImage();

                    AudioManager.instance.PlaySFXAdjusted(7);//playes dash sound effect
                }
            }

            if (dashCounter > 0)
            {
                dashCounter = dashCounter - Time.deltaTime; //~Note~ "Time.deltaTime" is the amount of time it takes for your game to update - ex.) 60 fps = 1/60th

                myRigidBody.velocity = new Vector2(dashSpeed * transform.localScale.x, myRigidBody.velocity.y); //dash speed boost

                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    ShowAfterImage();
                }

                dashRechargeCounter = waitAfterDashing;
            }
            else
            {

                //handles horizontal player movements
                myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidBody.velocity.y);

                //handles direction changes
                if (myRigidBody.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);//-1f in x value flips sprite 
                }
                else if (myRigidBody.velocity.x > 0) //makes characters keep looking the same direction after moveing
                {
                    transform.localScale = Vector3.one; //~Note~ "Vector3.one" sets (x,y,z) all to 1
                }
            }

            //creates the point at which we can check if character is on the ground
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            //creates the point at which we can check if character is on the ground
            canStand = Physics2D.OverlapCircle(standPoint.position, .2f, whatIsGround);

            //handles vertical player movement "jumping" + double jump ability
            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)) && !isLookingUp)
            {
                if (isOnGround)
                {
                    canDoubleJump = true;

                    AudioManager.instance.PlaySFXAdjusted(2);//playes jump sound effect
                }
                else
                {
                    canDoubleJump = false;

                    anim.SetTrigger("doubleJump");

                    AudioManager.instance.PlaySFXAdjusted(5);//playes double jump sound effect
                }

                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
            }

            if (shotCounter > 0)//slows the fire rate of player
            {
                shotCounter -= Time.deltaTime;
            } else
            {
                //handles player shooting + bomb dropping
                if (Input.GetButtonDown("Fire1"))//~Note~ "Fire1" has a default setting linked to left click on mouse
                {
                    if (standing.activeSelf)//if standing, on "fire" shoot
                    {
                        if ((Input.GetAxisRaw("Vertical") > .9f) && isOnGround == true)//if looking up, shoot up
                        {
                            Instantiate(shotToFire, shotUpPoint.position, shotUpPoint.rotation).moveDirX = new Vector2(transform.localScale.y, 0f);
                        }
                        else
                        { //shoot regular
                            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDirY = new Vector2(transform.localScale.x, 0f);

                            anim.SetTrigger("shotFired");
                        }

                        AudioManager.instance.PlaySFXAdjusted(0);//playes shooting sound effect
                    }
                    else if (ball.activeSelf && abilities.canDropBomb)//if rolling, on "fire" place bomb
                    {
                        Instantiate(bomb, bombPoint.position, bombPoint.rotation);

                        AudioManager.instance.PlaySFXAdjusted(1);//playes mine drop sound effect
                    }
                    shotCounter = fireRate;
                }
            }

            //ball mode
            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") > .9f && isOnGround == true)//Look up function
                {
                    isLookingUp = true;
                    anim.SetBool("lookingUp", true);
                    myRigidBody.velocity = Vector2.zero; //stop movement speed

                    if(Camera.main.transform.position.x < lookUpBound)
                    {
                        Camera.main.transform.position += new Vector3(0f, 0.5f, 0f);
                    }
                } else
                {
                    isLookingUp = false;
                    anim.SetBool("lookingUp", false);
                }


                if (Input.GetAxisRaw("Vertical") < -.9f && abilities.canBecomeBall)//turn to ball
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);

                        AudioManager.instance.PlaySFXAdjusted(8);//playes ball mode effect
                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > .9f && canStand == false)//stand back up
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);
                        AudioManager.instance.PlaySFXAdjusted(4);//playes out of ball mode effect
                    }
                } else
                {
                    ballCounter = waitToBall;
                }
            }
        } else
        {
            myRigidBody.velocity = Vector2.zero; //stops player movement
        }

            if (standing.activeSelf)
            {
                //checks if character is on the ground but linked to jump and idle animations
                anim.SetBool("isOnGround", isOnGround);

                //checks if character is moving - linked between idle and movement animations
                //~Note~ "Mathf.Abs" makes it so we are checking for only a positive number (Absolute Values)
                anim.SetFloat("speed", Mathf.Abs(myRigidBody.velocity.x));
            }

            if (ball.activeSelf)
            {
                //checks if character is moving in ball mode- linked between idle and movement animations for ball
                ballAnim.SetFloat("speed", Mathf.Abs(myRigidBody.velocity.x));
            }
    }

    // function that enables after image effect linked to dash ability
    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation); // creates object "image" that copies whatever movement your player is currently in
        image.sprite = mySpriteRenderer.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifeTime);//delete after image after some time has passed

        afterImageCounter = timeBetweenAfterImages;
    }
}