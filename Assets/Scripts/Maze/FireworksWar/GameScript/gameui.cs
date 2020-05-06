using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameui : MonoBehaviour
{
    public List<int> killCount;
    public float totalTime = 150;
    float intervalTime = 1;
    public Text countDownText;
    public Text myKillCount;

    public Image killBackground;
    public Text killText;

    GameObject player;

    public List<float> ScoreList;
    int[] ScoreSort = { 0, 0, 0, 0, 0, 0 };
    string[] Number = { "0", "0", "0", "0", "0", "0" };
    public List<Sprite> uis;

    public Text NOText;
    public float No = 0;
    float nowNo = 0;
    public Text NoCoinText;
    public float NoCoinNum = 0;
    public Text NoEXPText;
    public float NoEXPNum = 0;
    public Text KillCoinText;
    public float KillCoinNum = 0;
    public Text KillEXPText;
    public float KillEXPNum = 0;
    public Text PropCoinsText;
    public float PropCoinsNum = 0;
    public Text PropEXPText;
    public float propEXPNum = 0;
    public Text EndKillNUMText;

    public Text KillNumText;
    public float KillNum = 0;

    public static float AllCoinNum = 0;
    public static float AllEXPNum = 0;

    public Image AI1;
    public Image AI2;
    public Image AI3;
    public Image AI4;
    public Image AI5;

    public Image NO1;
    public Image NO2;
    public Image NO3;
    public Image NO4;
    public Image NO5;

    public Sprite UI1;
    public Sprite UI2;
    public Sprite UI3;
    public Sprite UI4;
    public Sprite UI5;
    public Sprite UI6;

    public GameObject ai1;
    public GameObject ai2;
    public GameObject ai3;
    public GameObject ai4;
    public GameObject ai5;


    public Text One;
    public Text Two;
    public Text Three;
    public Text Four;
    public Text Five;

    public Text OneName;
    public Text TwoName;
    public Text ThreeName;
    public Text FourName;
    public Text FiveName;
    // Use this for initialization

    int newTime = 0;
    void Start()
    {
        
        countDownText.text = string.Format("时间:{0:D2}:{1:D2}",
            (int)totalTime / 60, (int)totalTime % 60);
        player = GameObject.Find("Player");

        for (int i = 0; i < 6; i++)
        {
            killCount.Add(0);
        }

        uis.Add(UI1);
        uis.Add(UI2);
        uis.Add(UI3);
        uis.Add(UI4);
        uis.Add(UI5);
        uis.Add(UI6);
    }

    // Update is called once per frame
    void Update()
    {
        time();
        AIposition();

        if (newTime >= 60)////////////
        {
            newTime = 0;
            scoreSort();
            ScoreText();
        }
        else if (newTime < 60)
        {
            newTime++;
        }

        UIControl();
    }

    void UIControl()
    {
        nowNo = 0;
        if (ai1 == null)
        {
            nowNo++;
        }
        if (ai2 == null)
        {
            nowNo++;
        }
        if (ai3 == null)
        {
            nowNo++;
        }
        if (ai4 == null)
        {
            nowNo++;
        }
        if (ai5 == null)
        {
            nowNo++;
        }
        No = 6 - nowNo;
        NoCoinNum = 30 - No * 5;
        NoEXPNum = 30 - No * 5;
        if (KillNum >= 5)
        {
            KillNum = 5;
        }
        KillCoinNum = KillNum * 3;
        KillEXPNum = KillNum * 3;
        KillNumText.text = "击杀:" + KillNum;
        EndKillNUMText.text = "击杀:" + KillNum;
        KillCoinText.text = "" + KillCoinNum;
        KillEXPText.text = "" + KillEXPNum;
        NOText.text = "No." + No;
        NoCoinText.text = "" + NoCoinNum;
        NoEXPText.text = "" + NoEXPNum;
        PropCoinsText.text = "" + PropCoinsNum;
        PropEXPText.text = "" + propEXPNum;
        AllCoinNum = NoCoinNum + KillCoinNum + PropCoinsNum;
        AllEXPNum = NoEXPNum + KillEXPNum + propEXPNum;
    }

    void time()
    {
        if (totalTime > 0)
        {
            intervalTime -= Time.deltaTime;
            if (intervalTime <= 0)
            {
                intervalTime += 1;
                totalTime--;
                countDownText.text = string.Format("时间:{0:D2}:{1:D2}",
            (int)totalTime / 60, (int)totalTime % 60);
            }
        }
    }

    void AIposition()
    {
        if (ai1 != null)//1
        {
            AI1.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, ai1.transform.position);
            Vector2 a = AI1.GetComponent<RectTransform>().anchoredPosition;

            if (a.x > 520.0f || a.x < -520.0f || a.y > 960.0f || a.y < -960.0f)
            {
                AI1.gameObject.SetActive(true);
                if (a.x > 520.0f)
                {
                    a = new Vector2(520.0f, a.y);
                }
                if (a.x < -520.0f)
                {
                    a = new Vector2(-520.0f, a.y);
                }
                if (a.y > 960.0f)
                {
                    a = new Vector2(a.x, 960.0f);
                }
                if (a.y < -960.0f)
                {
                    a = new Vector2(a.x, -960.0f);
                }
                AI1.GetComponent<RectTransform>().anchoredPosition = a;
            }
            else
            {
                AI1.gameObject.SetActive(false);
            }
        }
        else
        {
            AI1.gameObject.SetActive(false);
        }

        if (ai2 != null)//2
        {

            AI2.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, ai2.transform.position);
            Vector2 a = AI2.GetComponent<RectTransform>().anchoredPosition;

            if (a.x > 520.0f || a.x < -520.0f || a.y > 960.0f || a.y < -960.0f)
            {
                AI2.gameObject.SetActive(true);
                if (a.x > 520.0f)
                {
                    a = new Vector2(520.0f, a.y);
                }
                if (a.x < -520.0f)
                {
                    a = new Vector2(-520.0f, a.y);
                }
                if (a.y > 960.0f)
                {
                    a = new Vector2(a.x, 960.0f);
                }
                if (a.y < -960.0f)
                {
                    a = new Vector2(a.x, -960.0f);
                }
                AI2.GetComponent<RectTransform>().anchoredPosition = a;
            }
            else
            {
                AI2.gameObject.SetActive(false);
            }
        }
        else
        {
            AI2.gameObject.SetActive(false);
        }

        if (ai3 != null)//3
        {

            AI3.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, ai3.transform.position);
            Vector2 a = AI3.GetComponent<RectTransform>().anchoredPosition;

            if (a.x > 520.0f || a.x < -520.0f || a.y > 960.0f || a.y < -960.0f)
            {
                AI3.gameObject.SetActive(true);
                if (a.x > 520.0f)
                {
                    a = new Vector2(520.0f, a.y);
                }
                if (a.x < -520.0f)
                {
                    a = new Vector2(-520.0f, a.y);
                }
                if (a.y > 960.0f)
                {
                    a = new Vector2(a.x, 960.0f);
                }
                if (a.y < -960.0f)
                {
                    a = new Vector2(a.x, -960.0f);
                }
                AI3.GetComponent<RectTransform>().anchoredPosition = a;
            }
            else
            {
                AI3.gameObject.SetActive(false);
            }
        }
        else
        {
            AI3.gameObject.SetActive(false);
        }

        if (ai4 != null)//4
        {

            AI4.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, ai4.transform.position);
            Vector2 a = AI4.GetComponent<RectTransform>().anchoredPosition;

            if (a.x > 520.0f || a.x < -520.0f || a.y > 960.0f || a.y < -960.0f)
            {
                AI4.gameObject.SetActive(true);
                if (a.x > 520.0f)
                {
                    a = new Vector2(520.0f, a.y);
                }
                if (a.x < -520.0f)
                {
                    a = new Vector2(-520.0f, a.y);
                }
                if (a.y > 960.0f)
                {
                    a = new Vector2(a.x, 960.0f);
                }
                if (a.y < -960.0f)
                {
                    a = new Vector2(a.x, -960.0f);
                }
                AI4.GetComponent<RectTransform>().anchoredPosition = a;
            }
            else
            {
                AI4.gameObject.SetActive(false);
            }
        }
        else
        {
            AI4.gameObject.SetActive(false);
        }

        if (ai5 != null)//5
        {

            AI5.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, ai5.transform.position);
            Vector2 a = AI5.GetComponent<RectTransform>().anchoredPosition;

            if (a.x > 520.0f || a.x < -520.0f || a.y > 960.0f || a.y < -960.0f)
            {
                AI5.gameObject.SetActive(true);
                if (a.x > 520.0f)
                {
                    a = new Vector2(520.0f, a.y);
                }
                if (a.x < -520.0f)
                {
                    a = new Vector2(-520.0f, a.y);
                }
                if (a.y > 960.0f)
                {
                    a = new Vector2(a.x, 960.0f);
                }
                if (a.y < -960.0f)
                {
                    a = new Vector2(a.x, -960.0f);
                }
                AI5.GetComponent<RectTransform>().anchoredPosition = a;
            }
            else
            {
                AI5.gameObject.SetActive(false);
            }
        }
        else
        {
            AI5.gameObject.SetActive(false);
        }
    }

    void scoreSort()
    {
        int i = 0;
        int temp = 0;
        string temp2 = "0";

        Sprite temp3 = UI1;////////
        Number[0] = "垃圾分类";/////////////
        Number[1] = "有害垃圾";
        Number[2] = "可回收垃圾";
        Number[3] = "湿垃圾";
        Number[4] = "干垃圾";
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            Number[5] = PlayerPrefs.GetString("PlayerName");
        }

        uis[0] = UI1;
        uis[1] = UI2;
        uis[2] = UI3;
        uis[3] = UI4;
        uis[4] = UI5;
        uis[5] = UI6;



        
        for (i = 0; i <= 5; i++)
        {
            ScoreSort[i] = killCount[i];

        }
        for (int k = 0; k < ScoreSort.Length - 1; k++)
        {
            for (int kk = 0; kk < ScoreSort.Length - 1 - k; kk++)
            {
                if (ScoreSort[kk] > ScoreSort[kk + 1])
                {
                    temp = ScoreSort[kk + 1];
                    temp2 = Number[kk + 1];
                    temp3 = uis[kk + 1];
                    ScoreSort[kk + 1] = ScoreSort[kk];
                    Number[kk + 1] = Number[kk];
                    uis[kk + 1] = uis[kk];
                    ScoreSort[kk] = temp;
                    Number[kk] = temp2;
                    uis[kk] = temp3;
                }
            }
        }
    }

    void ScoreText()
    {
        One.text = ScoreSort[5].ToString();
        Two.text = ScoreSort[4].ToString();
        Three.text = ScoreSort[3].ToString();
        Four.text = ScoreSort[2].ToString();
        Five.text = ScoreSort[1].ToString();


        OneName.text = Number[5];
        TwoName.text = Number[4];
        ThreeName.text = Number[3];
        FourName.text = Number[2];
        FiveName.text = Number[1];



        NO1.sprite = uis[5];
        NO2.sprite = uis[4];
        NO3.sprite = uis[3];
        NO4.sprite = uis[2];
        NO5.sprite = uis[1]; 
    }

    public void killInformation(string kr, string kd)
    {
        
        Text t_killText;
        Image t_killImage;
        t_killImage = GameObject.Instantiate(killBackground);
        t_killImage.transform.parent = this.gameObject.transform;
        t_killText = GameObject.Instantiate(killText);
        t_killText.transform.parent = t_killImage.gameObject.transform;
        killText.transform.position = killBackground.transform.position;
        t_killText.text = kr + "击杀了" + kd;
    }

    public void Score(GameObject P, int S)
    {
        if (P == ai1)
        {
            killCount[0] = S;
        }
        else if (P == ai2)
        {
            killCount[1] = S;
        }
        else if (P == ai3)
        {
            killCount[2] = S;
        }
        else if (P == ai4)
        {
            killCount[3] = S;
        }
        else if (P == ai5)
        {
            killCount[4] = S;
        }

        else if (P == player)
        {
            killCount[5] = S;
        }
    }
}
