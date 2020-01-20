using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public void sceneOne()
    {
        SceneManager.LoadScene(1);
    }

    public void sceneTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void sceneThree()
    {
        SceneManager.LoadScene(3);
    }

    public void sceneFour()
    {
        SceneManager.LoadScene(4);
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
