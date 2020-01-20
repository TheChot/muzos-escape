using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    private Scene currentScene;
    private Transform lowestPoint;
    private Transform endPoint;
    private characterController thePlayer;

    RectTransform playerHealth;
    int playerHealthCount;
    float healthScale;

    Transform endLevel;
    public LayerMask whatIsPlayer;
    public float winRadius;

    // Start is called before the first frame update
    void Start()
    {
        lowestPoint = GameObject.Find("LowestPoint").transform;
        endPoint = GameObject.Find("EndPoint").transform;
        thePlayer = (characterController)FindObjectOfType(typeof(characterController));
        playerHealth = GameObject.Find("healthbar").GetComponent<RectTransform>();
        healthScale = playerHealth.sizeDelta.x;
        endLevel = GameObject.Find("EndLevel").transform;
        playerHealthCount = thePlayer.health;
        // Debug.Log(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex).IsValid());
    }

    // Update is called once per frame
    void Update()
    {

        bool isFinished = Physics2D.OverlapCircle(endLevel.position, winRadius, whatIsPlayer);
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown("x"))
        {
            SceneManager.LoadScene(0);
        }

        if (thePlayer.transform.position.y < lowestPoint.position.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (thePlayer.transform.position.x > endPoint.position.x)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (isFinished)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }

        }
    }

    public void playerTakeDamage()
    {
        // playerHealth.localScale = new Vector3(playerHealth.localScale.x - 1/playerHealthCount, playerHealth.localScale.y, playerHealth.localScale.z);
        playerHealth.sizeDelta = new Vector2(playerHealth.sizeDelta.x - healthScale / playerHealthCount, playerHealth.sizeDelta.y);
        // Debug.Log("Health should go down");
        if (thePlayer.health == 0)
        {
            thePlayer.isDead = true;
        }
    }
}
