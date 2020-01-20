using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    private Rigidbody2D rb;

    // For movement and control
    public float moveSpeed = 10f;
    public float jumpForce = 10f;


    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode throwAttack;
    public KeyCode meleeAttack;

    // GroundChecks
    public Transform groundCheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    // Animator ref
    private Animator anim;

    public Transform throwPoint;
    public Transform meleePoint;

    public GameObject kunai;

    public float attackRange;
    public LayerMask whatIsEnemy;
    public float attackTime;
    float attackTimeReset;

    // Activating platforms
    public LayerMask whatIsActivator;

    public bool isDead;
    public int health;
    public float hitForce;
    public float hitUpForce;
    bool isHit;

    public float isHitTime = 1f;
    float isHitTimeReset;

    sceneManager sceneController;

    Vector2 moveVelocity;

    SpriteRenderer sr;





    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        attackTimeReset = attackTime;

        isHitTimeReset = isHitTime;

        sceneController = GameObject.Find("scenemanager").GetComponent<sceneManager>();


    }
    // void Update()
    // {
    //     Vector2 moveInput = new Vector2()
    // }

    private void FixedUpdate()
    {
        checkGround();
        if (!isDead)
        {
            controlChar();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("isDead", true);
            anim.SetBool("isHurt", false);
            anim.SetBool("New Bool", false);
            gameObject.layer = 0;
        }





    }

    public void controlChar()
    {
        bool isThrowing = Input.GetKeyDown(throwAttack);
        // bool isMelee = Input.GetKeyDown(meleeAttack);

        if (Input.GetKey(left))
        {

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            // rb.MovePosition(rb.position + new Vector2(-moveSpeed, 0) * Time.fixedDeltaTime);
            anim.SetBool("New Bool", true);

        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            // rb.MovePosition(rb.position + new Vector2(moveSpeed, 0) * Time.fixedDeltaTime);
            anim.SetBool("New Bool", true);
        }
        else
        {
            anim.SetBool("New Bool", false);
            // if (isHitTime > 0 && isHit)
            // {
            //     isHitTime -= Time.deltaTime;
            // }
            // if (isHitTime <= 0)
            // {
            //     isHit = false;
            //     isHitTime = isHitTimeReset;
            // }
            // if (!isHit)
            // {
            //     rb.velocity = new Vector2(0, rb.velocity.y);

            // }
            rb.velocity = new Vector2(0, rb.velocity.y);


        }

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // if (Input.GetKeyDown(throwAttack))
        // {
        //     throwKunai();
        // }
        if (isHit)
        {
            if (isHitTime > 0)
            {
                isHitTime -= Time.deltaTime;
            }
            else
            {
                isHit = false;
                isHitTime = isHitTimeReset;
                gameObject.layer = 10;
                anim.SetBool("isHurt", false);
            }

        }


        // anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        // anim.SetBool("isMelee", isMelee);
        anim.SetBool("isThrowing", isThrowing);

    }


    public void checkGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public void throwKunai()
    {
        GameObject kunaiClone = (GameObject)Instantiate(kunai, throwPoint.position, throwPoint.rotation);
        kunaiClone.transform.localScale = transform.localScale;
    }

    public void meleeHit()
    {

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, whatIsEnemy);
        Collider2D[] activatorToActivate = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, whatIsActivator);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<enemyController>().takeHit();
        }

        for (int i = 0; i < activatorToActivate.Length; i++)
        {
            activatorToActivate[i].GetComponent<platformActivator>().activatePlatform();
        }


    }
    // Modify to add for more enemy types and forces (bool enemyFacingLeft, hitStrength, isTrap)
    public void playerHit(bool enemyFacingLeft)
    {
        // KNOCKBACK MECHANICS <= player <-enemy
        // enemyleft & isLeft <=< -bounce left
        // !enemyLeft & isLeft-><= bounce right
        // enemyleft & !isLeft =>< -bounce left
        // !enemyLeft & !isLeft->=> bounce Right
        bool isFacingLeft = transform.localScale.x < 0;
        // float isHitTime = 1f;
        // float isHitTimeReset = isHitTime;
        isHit = true;
        health -= 1;
        gameObject.layer = 0;
        anim.SetBool("isHurt", true);

        sceneController.playerTakeDamage();

        if (enemyFacingLeft && isFacingLeft || enemyFacingLeft && !isFacingLeft)
        {
            rb.velocity = new Vector2(-hitForce, hitUpForce);

        }
        if (!enemyFacingLeft && !isFacingLeft || !enemyFacingLeft && isFacingLeft)
        {
            rb.velocity = new Vector2(hitForce, hitUpForce);
        }
        // Destroy(gameObject);
        // gameObject.SetActive(false);
        // anim.SetBool("isDead", true);
        // isDead = true;
        // anim.SetBool("isHurt", true);

    }
    // public void activatePlat()
    // {

    //     Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, whatIsEnemy);

    //     for (int i = 0; i < enemiesToDamage.Length; i++)
    //     {
    //         enemiesToDamage[i].GetComponent<enemyController>().takeHit();
    //     }
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            // Debug.Log("THe player collided with me");
            transform.SetParent(collision.collider.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            // Debug.Log("THe player collided left me");
            transform.parent = null;
        }

    }

}
