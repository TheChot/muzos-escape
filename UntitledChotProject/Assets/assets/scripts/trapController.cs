using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapController : MonoBehaviour
{
    private characterController thePlayer;
    void Start()
    {
        thePlayer = (characterController)FindObjectOfType(typeof(characterController));

    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.layer == 10)
        {

            thePlayer.playerHit(true);
        }



    }
}
