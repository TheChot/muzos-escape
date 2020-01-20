using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    public float moveSpeed;

    public float destroyTime = 5f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;

        rb.velocity = new Vector2(moveSpeed * transform.localScale.x, 0);

        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.layer == 9)
        {
            // Debug.Log("I hit a Zombie");

            other.gameObject.GetComponent<enemyController>().takeHit();
        }

        if (other.gameObject.layer == 12)
        {

            other.gameObject.GetComponent<platformActivator>().activatePlatform();
        }
        if (other.gameObject.layer != 10)
        {
            Destroy(gameObject);
        }


    }
}
