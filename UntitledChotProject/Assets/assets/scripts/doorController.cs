using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{


    float startPositionX;
    float startPositionY;

    float endPositionX;
    float endPositionY;

    public float moveSpeed;
    float move;
    public float moveDistance;
    public bool movesHorizontal;

    bool activateDoor;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;

        endPositionX = startPositionX + moveDistance;
        endPositionY = startPositionY + moveDistance;

        if (moveDistance > 0)
        {
            move = moveSpeed;
        }
        else
        {
            move = -moveSpeed;
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activateDoor)
        {
            if (movesHorizontal)
            {

                if (moveDistance > 0 && transform.position.x >= endPositionX)
                {
                    activateDoor = false;
                    rb.velocity = new Vector2(0, 0);

                }
                else if (moveDistance < 0 && transform.position.x <= endPositionX)
                {
                    activateDoor = false;
                    rb.velocity = new Vector2(0, 0);
                }
                else
                {
                    rb.velocity = new Vector2(move, 0);
                }

            }
            else
            {

                if (moveDistance > 0 && transform.position.y >= endPositionY)
                {
                    activateDoor = false;
                    rb.velocity = new Vector2(0, 0);

                }
                else if (moveDistance < 0 && transform.position.y <= endPositionY)
                {
                    activateDoor = false;
                    rb.velocity = new Vector2(0, 0);
                }
                else
                {
                    rb.velocity = new Vector2(0, move);
                }

            }
        }

        Vector3 pos = rb.position;

        pos.x = Mathf.Clamp(pos.x, startPositionX, endPositionX);
        pos.y = Mathf.Clamp(pos.x, startPositionY, endPositionY);
    }

    public void activateTheDoor()
    {
        activateDoor = true;
    }
}
