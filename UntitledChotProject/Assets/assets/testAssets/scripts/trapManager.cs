using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapManager : MonoBehaviour
{
    private playerMovement thePlayer;
    void Start()
    {
        thePlayer = (playerMovement)FindObjectOfType(typeof(playerMovement));

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            bool hitLeft = thePlayer.transform.position.x > transform.position.x; 
            if(hitLeft){
                thePlayer.isHit(true);            
            } else{
                thePlayer.isHit(false);            
            }
            
        }
    }
}
