using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{
    float startPositionX;
    float startPositionY;

    float endPositionX;
    float endPositionY;

    public float moveSpeed;
    float move;
    public float moveDistance;
    public bool movesHorizontal;

    bool activatePlatform;

    public bool activePlatform;

    // Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;

        endPositionX = startPositionX + moveDistance;
        endPositionY = startPositionY + moveDistance;

        move = moveSpeed;

        if (activePlatform)
        {
            activatePlatform = true;
        }
    }

    void Update()
    {

        if (movesHorizontal)
        {
            if (moveDistance > 0)
            {
                if (transform.position.x >= endPositionX)
                {
                    move = -moveSpeed;
                    // transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

                }
                else if (transform.position.x <= startPositionX)
                {
                    move = moveSpeed;
                    // transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

                }
            }
            else
            {
                if (transform.position.x <= endPositionX)
                {
                    move = moveSpeed;
                    // transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

                }
                else if (transform.position.x >= startPositionX)
                {
                    move = -moveSpeed;
                    // transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

                }
            }

        }
        else
        {
            if (moveDistance > 0)
            {
                if (transform.position.y >= endPositionY)
                {
                    move = -moveSpeed;
                    // transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);

                }
                else if (transform.position.y <= startPositionY)
                {
                    move = moveSpeed;
                    // transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

                }
            }
            else
            {
                if (transform.position.y <= endPositionY)
                {
                    move = moveSpeed;
                    // transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);

                }
                else if (transform.position.y >= startPositionY)
                {
                    move = -moveSpeed;
                    // transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

                }

            }

        }


    }



    void FixedUpdate()
    {
        if (activatePlatform)
        {
            if (movesHorizontal)
            {
                // rb.velocity = new Vector2(move, 0);
                // rb.MovePosition(rb.position + new Vector2(move, 0) * Time.fixedDeltaTime);
                transform.position += (new Vector3(move, 0, 0) * Time.fixedDeltaTime);
            }
            else
            {
                // rb.MovePosition(rb.position + new Vector2(0, move) * Time.fixedDeltaTime);
                transform.position += (new Vector3(0, move, 0) * Time.fixedDeltaTime);
            }
        }

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, startPositionX, endPositionX);
        pos.y = Mathf.Clamp(pos.x, startPositionY, endPositionY);
    }

    public void activateThePlatform()
    {
        activatePlatform = true;
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.layer == 9)
    //     {
    //         Debug.Log("THe player collided with me");
    //     }
    // }
}
