using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // 
    Animator anim;

    // Player Movement
    playerMovement playerMover;

    // For Punching    
    public int punchClicks = 0;
    float punchClickedTime;
    public float maxComboTime = 0.9f;







    void Start()
    {
        anim = GetComponent<Animator>();
        playerMover = GetComponent<playerMovement>();
    }


    void Update()
    {
        playerPunch();
    }
    // Punching
    void playerPunch()
    {

        if (Time.time - punchClickedTime >= maxComboTime)
        {
            punchClicks = 0;
            anim.SetBool("punch_1", false);
            anim.SetBool("punch_2", false);
            playerMover.cancelMovement = false;
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            punchClicks++;
            punchClickedTime = Time.time;
            playerMover.cancelMovement = true;
            // Debug.Log(playerMover.cancelMovement);

            if (punchClicks == 1)
            {
                anim.SetBool("punch_1", true);
            }


            punchClicks = Mathf.Clamp(punchClicks, 0, 2);
        }
    }

    public void secondPunch()
    {
        if (punchClicks >= 2)
        {
            anim.SetBool("punch_2", true);
        }
        else
        {
            anim.SetBool("punch_1", false);
            punchClicks = 0;
            playerMover.cancelMovement = false;
        }
    }
    public void resetPunch()
    {
        // hasPunched = false;
        punchClicks = 0;
        anim.SetBool("punch_2", false);
        anim.SetBool("punch_1", false);
        playerMover.cancelMovement = false;
    }
    // Punching end


}
