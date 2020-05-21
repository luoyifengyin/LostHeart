using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject Obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Obj.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Obj.gameObject.SetActive(false);
        }
    }

    public void LoadToMainMenu()
    {
        SceneController.LoadScene("MainMenu");
    }

    public void LoadToOrigin()
    {
        SceneController.LoadScene("origin");
    }

    public void LoadToCarRacing()
    {
        SceneController.LoadScene("CarRacing");
    }

    public void LoadToSecond()
    {
        SceneController.LoadScene("Second");
    }

    public void LoadToMaze()
    {
        SceneController.LoadScene("Maze");
    }

    public void LoadToNormalEnd()
    {
        SceneController.LoadScene("NormalEnd");
    }

    public void LoadToGoodEnd()
    {
        SceneController.LoadScene("GoodEnd");
    }

    public void LoadToBadEnd()
    {
        SceneController.LoadScene("BadEnd");
    }

    public void LoadToBetMan()
    {
        SceneController.LoadScene("BetMan");
    }

    public void LoadToFireworksWar()
    {
        SceneController.LoadScene("GameScene");
    }
}
