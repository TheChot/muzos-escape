using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float walkSpeed;

    // check ahead of character
    public Transform groundDetector;
    public LayerMask whatIsGround;
    public float groundDistance = 2f;

    // For f@$%ing up the player
    public Transform hitPoint;
    public LayerMask whatIsPlayer;
    bool isPlayer;
    public float hitRadius;
    public float hitTime;
    float hitTimeReset;
    // bool playerDetected = false;
    public float attackWait = 2.0f;
    bool isAttacking;


    // Check Ground in front
    public bool isGround;
    public float groundCheckRadius;
    public Transform groundCheck;

    // Animator ref
    private Animator anim;

    bool isDead = false;

    // Destroy zombie
    Transform fowardSpawn;
    Transform backSpawn;

    // Player Detection
    public float myBackDistance;
    public Transform backPoint;
    public float backPointRadius;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        fowardSpawn = GameObject.Find("FowardSpawner").transform;
        backSpawn = GameObject.Find("BackSpawner").transform;
        hitTimeReset = hitTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // attackPlayer();


        // checkGround();
        RaycastHit2D isThereGround = Physics2D.Raycast(groundDetector.position, Vector2.down, groundDistance);
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isPlayer = Physics2D.OverlapCircle(hitPoint.position, hitRadius, whatIsPlayer);


        bool isPlayerBehindMe = Physics2D.OverlapCircle(backPoint.position, backPointRadius, whatIsPlayer);
        // if (!isDead && !playerDetected)
        if (!isDead)
        {

            // checkPlayer();

            if (transform.localScale.x < 0)
            {

                moveLeft();
            }
            else if (transform.localScale.x > 0)
            {

                moveRight();
            }

            if (isThereGround.collider == false || isPlayerBehindMe || isGround)
            {

                turnAround();
            }
            // Debug.Log(isThereGround.collider);
            // if (isPlayerBehindMe)
            // {
            //     turnAround();

            // }

            // if (isGround)
            // {
            //     turnAround();
            // }
        }

        // For attacking the player
        // if (isPlayer || playerDetected)
        // {
        //     if (hitTime > 0)
        //     {

        //         if (!isAttacking)
        //         {
        //             playerDetected = true;
        //             isAttacking = true;
        //             rb.velocity = new Vector2(0, rb.velocity.y);
        //             anim.SetTrigger("attackPlayer");
        //             Invoke("attackPlayer", attackWait);
        //         }

        //         hitTime -= Time.deltaTime;

        //     }
        // }

        // if (hitTime <= 0 && isAttacking)
        // {
        //     playerDetected = false;
        //     isAttacking = false;
        //     hitTime = hitTimeReset;
        // }

        if (isDead)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            gameObject.layer = 14;

        }




        if (transform.position.x > fowardSpawn.position.x || transform.position.x < backSpawn.position.x)
        {
            destroyZombie();
        }

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        // anim.SetBool("isAttacking", isPlayer);
        anim.SetBool("isDead", isDead);

    }

    void turnAround()
    {
        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            moveLeft();
        }
        else if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            moveRight();
        }
    }

    void moveLeft()
    {
        rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
    }

    void moveRight()
    {
        rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
    }

    public void takeHit()
    {
        // Destroy(gameObject);
        isDead = true;
        Invoke("destroyZombie", 3.0f);
    }

    // void checkGround()
    // {
    //     isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    // }

    // void checkPlayer()
    // {
    //     isPlayer = Physics2D.OverlapCircle(hitPoint.position, hitRadius, whatIsPlayer);

    // }

    public void attackPlayer()
    {
        // playerDetected = true;


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius, whatIsPlayer);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (transform.localScale.x > 0)
            {
                enemiesToDamage[i].GetComponent<characterController>().playerHit(false);
            }
            else
            {
                enemiesToDamage[i].GetComponent<characterController>().playerHit(true);
            }

        }

        anim.ResetTrigger("attackPlayer");

    }

    // public void stopTheAttack()
    // {
    //     isAttacking = false;
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            if (collision.gameObject.layer == 10)
            {
                // Debug.Log("THe player collided with me");
                // transform.SetParent(collision.collider.transform);
                if (transform.localScale.x > 0)
                {
                    collision.gameObject.GetComponent<characterController>().playerHit(false);
                }
                else
                {
                    collision.gameObject.GetComponent<characterController>().playerHit(true);
                }
            }
        }
    }

    public void destroyZombie()
    {
        Destroy(gameObject);
    }
}
