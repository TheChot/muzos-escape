using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformActivator : MonoBehaviour
{
    public platformController[] thePlatforms;
    public doorController[] theDoors;
    public Animator anim;
    public Collider2D col;
    // bool isActive;

    public void activatePlatform()
    {
        if (thePlatforms.Length > 0)
        {
            for (int i = 0; i < thePlatforms.Length; i++)
            {
                thePlatforms[i].activateThePlatform();
            }
        }

        if (theDoors.Length > 0)
        {
            // Debug.Log("Im an activating door");
            for (int i = 0; i < theDoors.Length; i++)
            {
                theDoors[i].activateTheDoor();
            }
        }

        col.enabled = false;
        anim.SetBool("isActive", true);
    }

    // void Update()
    // {

    // }
}
