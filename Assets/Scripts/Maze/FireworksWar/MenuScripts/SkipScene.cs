using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SkipScene : MonoBehaviour {
    static public float adTime;
    static int buttonn = 0;
    public static int isAd = 0;
    public static int gameIsAd = 0;

    public GameObject MainMenuScene;
    public GameObject CheckInScene;
    public GameObject SettingScene;
    public GameObject StrongerScene;
    public GameObject CharacterScene;
    public GameObject FireworksChangeScene;

    public GameObject BackBtn;
    public GameObject StrongBtn;
    public GameObject EvolutionBtn;
    public GameObject StartBtn;

    public Text CoinText;
    public static float CoinNum;
    public Image EXPImage;
    public static float EXPNum;
    public Image LevelImage;
    public static float Level = 1;
    public Text LevelName;

    public Text PlayerName;

    public Sprite Level1;
    public Sprite Level2;
    public Sprite Level3;
    public Sprite Level4;
    public Sprite Level5;
    public Sprite Level6;
    public Sprite Level7;
    public Sprite Level8;

    public List<Sprite> LevelList;
	// Use this for initialization
	void Start () {
        PlayerName.text = "Player";
        Screen.SetResolution(108*4, 192*4, false);
        if (PlayerPrefs.HasKey("CoinNum"))
        {
            SkipScene.CoinNum = PlayerPrefs.GetFloat("CoinNum");
        }
        if (PlayerPrefs.HasKey("EXPNum"))
        {
            SkipScene.EXPNum = PlayerPrefs.GetFloat("EXPNum");
        }
        if (PlayerPrefs.HasKey("LevelNum"))
        {
            SkipScene.Level = PlayerPrefs.GetFloat("LevelNum");
        }
        LevelList.Add(Level1);
        LevelList.Add(Level2);
        LevelList.Add(Level3);
        LevelList.Add(Level4);
        LevelList.Add(Level5);
        LevelList.Add(Level6);
        LevelList.Add(Level7);
        LevelList.Add(Level8);
	}
	
	// Update is called once per frame
	void Update () {
        CoinText.text = "" + CoinNum;
        EXPImage.fillAmount = EXPNum / 50f;
        if (UIScript.AddEXPFlag == 1)
        {
            EXPImage.fillAmount += EXPNum / 50f;
            UIScript.AddEXPFlag = 0;
        }
        if (EXPImage.fillAmount >= 1 && Level < 8)
        {
            Level++;
            EXPImage.fillAmount = 0;
            EXPNum = 0;
        }
        LevelControl();
	}

    void LevelControl()
    {
        if (Level == 1)
        {
            LevelImage.sprite = LevelList[0];
            LevelName.text = "英勇黄铜";
        }
        if (Level == 2)
        {
            LevelImage.sprite = LevelList[1];
            LevelName.text = "不屈白银";
        }
        if (Level == 3)
        {
            LevelImage.sprite = LevelList[2];
            LevelName.text = "荣耀黄金";
        }
        if (Level == 4)
        {
            LevelImage.sprite = LevelList[3];
            LevelName.text = "华贵铂金";
        }
        if (Level == 5)
        {
            LevelImage.sprite = LevelList[4];
            LevelName.text = "暗淡钻石";
        }
        if (Level == 6)
        {
            LevelImage.sprite = LevelList[5];
            LevelName.text = "璀璨钻石";
        }
        if (Level == 7)
        {
            LevelImage.sprite = LevelList[6];
            LevelName.text = "超凡大师";
        }
        if (Level == 8)
        {
            LevelImage.sprite = LevelList[7];
            LevelName.text = "最强王者";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        if (gameIsAd == 1)
        {
            isAd = 1;
            gameIsAd = 0;
        }
    }

    public void CheckIn()
    {
        CheckInScene.SetActive(true);
        StrongBtn.SetActive(false);
        StartBtn.SetActive(false);
        EvolutionBtn.SetActive(false);
        BackBtn.SetActive(false);
        SettingScene.SetActive(false);
        StrongerScene.SetActive(false);
        CharacterScene.SetActive(false);
        FireworksChangeScene.SetActive(false);
    }
    public void Setting()
    {
        SettingScene.SetActive(true);
        StrongBtn.SetActive(false);
        StartBtn.SetActive(false);
        EvolutionBtn.SetActive(false);
        BackBtn.SetActive(false);
        CheckInScene.SetActive(false);
        StrongerScene.SetActive(false);
        CharacterScene.SetActive(false);
        FireworksChangeScene.SetActive(false);
    }
    public void Stronger()
    {
        StrongerScene.SetActive(true);
        StrongBtn.SetActive(false);
        StartBtn.SetActive(false);
        EvolutionBtn.SetActive(false);
        BackBtn.SetActive(false);
        SettingScene.SetActive(false);
        CheckInScene.SetActive(false);
        CharacterScene.SetActive(false);
        FireworksChangeScene.SetActive(false);
    }
    public void Character()
    {
        CharacterScene.SetActive(true);
        BackBtn.SetActive(true);
        StrongBtn.SetActive(false);
        StartBtn.SetActive(false);
        EvolutionBtn.SetActive(false);
        StrongerScene.SetActive(false);
        SettingScene.SetActive(false);
        CheckInScene.SetActive(false);
        FireworksChangeScene.SetActive(false);
    }
    public void FireworksChange()
    {
        FireworksChangeScene.SetActive(true);
        BackBtn.SetActive(true);
        StrongBtn.SetActive(false);
        StartBtn.SetActive(false);
        EvolutionBtn.SetActive(false);
        CharacterScene.SetActive(false);
        StrongerScene.SetActive(false);
        SettingScene.SetActive(false);
        CheckInScene.SetActive(false);
    }
    public void BackToMainMenu()
    {
        StrongBtn.SetActive(true);
        StartBtn.SetActive(true);
        EvolutionBtn.SetActive(true);
        BackBtn.SetActive(false);
        FireworksChangeScene.SetActive(false);
        CharacterScene.SetActive(false);
        StrongerScene.SetActive(false);
        SettingScene.SetActive(false);
        CheckInScene.SetActive(false);
        PlayerPrefs.SetString("PlayerName", PlayerName.text);
    }
}
