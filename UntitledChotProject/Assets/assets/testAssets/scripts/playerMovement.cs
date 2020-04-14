using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    float moveInput;

    // For combos 
    public float fallSpeed;

    // For jumping
    public float jumpForce;
    bool isGrounded;

    // Ground check
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Vector2 hitDistance;

    Rigidbody2D rb;

    // WHen hit
    public bool cancelMovement = false;
    bool playerHit;
    public float hitTime = 0.3f;
    float hitTimeReset;

    // For animation
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        hitTimeReset = hitTime;
    }

    void Update()
    {
        // if (!cancelMovement)
        // {
        //     moveInput = Input.GetAxisRaw("Horizontal");
        // }
        // else
        // {
        //     moveInput = 0;
        // }

        moveInput = Input.GetAxisRaw("Horizontal");
    }


    void FixedUpdate()
    {
        checkGround();

        if (!playerHit)
        {
            if (isGrounded)
            {
                moveCharacter();
                charJump();
            }
        }


        if (playerHit)
        {
            hitTime -= Time.deltaTime;
            anim.SetFloat("run", 0);
            if (hitTime < 0)
            {
                playerHit = false;
                hitTime = hitTimeReset;
            }
        }


        anim.SetBool("grounded", isGrounded);



    }

    void moveCharacter()
    {

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // float moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("run", Mathf.Abs(moveInput));
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


    }

    void charJump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void checkGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public void isHit(bool isLeft)
    {
        bool facingLeft = transform.localScale.x < 0;
        playerHit = true;

        if (facingLeft && isLeft || isLeft && !facingLeft)
        {
            // rb.MovePosition(rb.position + hitDistance);
            rb.velocity = hitDistance;
        }
        else if (!facingLeft && !isLeft || facingLeft && !isLeft)
        {
            // rb.MovePosition(rb.position + new Vector2(-hitDistance.x, hitDistance.y));
            rb.velocity = new Vector2(-hitDistance.x, hitDistance.y);
        }

    }

    // For Debugging the areas 
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    // }
}
