using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
    public GameObject EndUI;
    public GameObject WinBackGround;
    public GameObject LoseBackGround;
    public GameObject BackToMenuBtn;

    public float PlayerKillNum = 0;
    public static float AddEXPFlag = 0;

    public GameObject ai1;
    public GameObject ai2;
    public GameObject ai3;
    public GameObject ai4;
    public GameObject ai5;

    int MusicOnWin = 0;
    int MusicOnLose = 0;

    public static float isWin = 0;

    enemyAI AI;
    enemyAITwo AITwo;
    enemyAIThree AIThree;
    enemyAIFour AIFour;
    enemyAIFive AIFive;
	// Use this for initialization
	void Start () {
        AI = GameObject.Find("垃圾分类").GetComponent<enemyAI>();
        AITwo = GameObject.Find("有害垃圾").GetComponent<enemyAITwo>();
        AIThree = GameObject.Find("可回收垃圾").GetComponent<enemyAIThree>();
        AIFour = GameObject.Find("湿垃圾").GetComponent<enemyAIFour>();
        AIFive = GameObject.Find("干垃圾").GetComponent<enemyAIFive>();
        FireworkScripts.GG = 0;
        AddEXPFlag = 0;
        MusicOnWin = 0;
        MusicOnLose = 0;
        isWin = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (FireworkScripts.GG == 1)
        {
            SkipScene.isAd = 0;
            PlayerPrefs.SetFloat("CoinNum", SkipScene.CoinNum);
            PlayerPrefs.SetFloat("EXPNum", SkipScene.EXPNum);
            PlayerPrefs.SetFloat("LevelNum", SkipScene.Level);
            if (MusicOnLose == 0)
            {
                Music.Lose1.Play();
                Music.Lose2.Play();
                Music.BGM.Stop();
                MusicOnLose = 1;
            }
            GameOver();
            LoseBackGround.SetActive(true);
            AI.AISpeed = 0;
            AITwo.AISpeed = 0;
            AIThree.AISpeed = 0;
            AIFour.AISpeed = 0;
            AIFive.AISpeed = 0;
        }
        if (ai1==null&&ai2==null&&ai3==null&&ai4==null&&ai5==null)
        {
            PlayerPrefs.SetFloat("CoinNum", SkipScene.CoinNum);
            PlayerPrefs.SetFloat("EXPNum", SkipScene.EXPNum);
            PlayerPrefs.SetFloat("LevelNum", SkipScene.Level);
            Win();
            isWin = 1;
            SkipScene.isAd = 0;
            if (MusicOnWin == 0)
            {
                Music.Win1.Play();
                Music.Win2.Play();
                Music.BGM.Stop();
                MusicOnWin = 1;
            }
            WinBackGround.SetActive(true);
        }
	}

    public void GameOver()
    {
        EndUI.SetActive(true);
    }

    public void Win()
    {
        EndUI.SetActive(true);
    }

    public void BackToMenu()
    {
        
        SceneManager.LoadScene("FireworksMenu");
        AddEXPFlag = 1;
        SkipScene.CoinNum += gameui.AllCoinNum;
        SkipScene.EXPNum += gameui.AllEXPNum;
    }
}
