using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;
    // should fallspeed accelarate over time?
    public float fallSpeed;
    // For jumping
    public float jumpForce;
    bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Vector2 hitDistance;

    Rigidbody2D rb;



    bool cancelMovement = false;
    public float hitTime = 0.3f;
    float hitTimeReset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitTimeReset = hitTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        checkGround();
        if (!cancelMovement)
        {
            moveCharacter();
            charJump();
        }
        else
        {
            hitTime -= Time.deltaTime;
            if (hitTime < 0)
            {
                cancelMovement = false;
                hitTime = hitTimeReset;
            }
        }



    }

    void moveCharacter()
    {

        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            // rb.MovePosition(rb.position + new Vector2(-speed, -fallSpeed) * Time.fixedDeltaTime);
            // rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            // rb.MovePosition(rb.position + new Vector2(speed, -fallSpeed) * Time.fixedDeltaTime);
            // rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);

        }
        // else
        // {
        //     rb.velocity = new Vector2(0, rb.velocity.y);
        // }

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


    }

    void charJump()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
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
        cancelMovement = true;
        // Debug.Log(rb.position.x);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
