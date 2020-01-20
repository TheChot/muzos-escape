using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    public GameObject zombie;
    public float spawnTime;
    public float spawnLimit;
    public float firstSpawnTime;

    float spawnReset;
    float spawnCount = 0;

    // Manage spawning
    bool shouldSpawn;
    Transform fowardSpawn;
    Transform backSpawn;

    private Animator anim;
    // bool isOpen;

    public float playerRadius;
    public LayerMask whatIsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        spawnReset = spawnTime;
        fowardSpawn = GameObject.Find("FowardSpawner").transform;
        backSpawn = GameObject.Find("BackSpawner").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPlayerNear = Physics2D.OverlapCircle(transform.position, playerRadius, whatIsPlayer);

        anim.SetBool("isOpen", shouldSpawn);

        if (isPlayerNear)
        {
            shouldSpawn = true;
        }

        if (shouldSpawn)
        {
            spawnTime -= Time.deltaTime;
            // if (spawnCount == 0)
            // {

            //     Invoke("spawnZombie", firstSpawnTime);
            //     spawnCount += 1;
            // }
            if (spawnTime <= 0 && spawnCount != spawnLimit)
            {
                spawnZombie();
                // Invoke("spawnZombie", .0f);
                spawnTime = spawnReset;
                spawnCount += 1;
            }

            if (spawnCount == spawnLimit)
            {
                shouldSpawn = false;
            }
        }



        // Invoke("spawnZombie", spawnTime);

        if (transform.position.x > fowardSpawn.position.x || transform.position.x < backSpawn.position.x)
        {
            shouldSpawn = false;
            spawnCount = 0;
        }
        // else
        // {
        //     shouldSpawn = true;
        // }




    }

    void spawnZombie()
    {

        // GameObject zombieClone = (GameObject)Instantiate(zombie, transform.position, transform.rotation);
        Instantiate(zombie, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation);

    }


}
