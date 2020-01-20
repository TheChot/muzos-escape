using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    characterController thePlayer;
    Transform fowardPoint;
    Transform backPoint;
    Transform highPoint;
    Transform lowPoint;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = (characterController)FindObjectOfType(typeof(characterController));
        fowardPoint = GameObject.Find("FowardPoint").transform;
        backPoint = GameObject.Find("BackPoint").transform;
        highPoint = GameObject.Find("HighPoint").transform;
        lowPoint = GameObject.Find("LowPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer.transform.position.x > fowardPoint.position.x)
        {
            float distance = thePlayer.transform.position.x - fowardPoint.position.x;
            transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        }

        if (thePlayer.transform.position.x < backPoint.position.x)
        {
            float distance = backPoint.transform.position.x - thePlayer.transform.position.x;
            transform.position = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
        }

        if (thePlayer.transform.position.y > highPoint.position.y)
        {
            float distanceY = thePlayer.transform.position.y - highPoint.position.y;
            transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
        }

        if (thePlayer.transform.position.y < lowPoint.position.y)
        {
            float distanceY = lowPoint.position.y - thePlayer.transform.position.y;
            transform.position = new Vector3(transform.position.x, transform.position.y - distanceY, transform.position.z);
        }
    }
}
