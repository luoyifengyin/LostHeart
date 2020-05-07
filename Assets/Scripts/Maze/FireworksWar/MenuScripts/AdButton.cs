using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdButton : MonoBehaviour
{
    
    public GameObject advertisement;
    public Text time;
    int time1;
    int time2;
    int buttonn = 0;
    // Use this for initialization
    void Start()
    {
        time.text = "4";
    }

    // Update is called once per frame
    void Update()
    {
        time2++;
        if (time2 > 60)
        {
            time2 = 0;
            time1--;
            time.text = time1.ToString();
        }
        if (time1 == 0)
        {
            advertisement.SetActive(false);
            time.gameObject.SetActive(false);
            buttonn = 0;
        }
    }
    public void advertisementbutton()
    {
        if (buttonn == 0)
        {
            buttonn = 1;
            SkipScene.adTime = 0;
            time1 = 4;
            time2 = 0;
            time.text = time1.ToString();
            advertisement.SetActive(true);
            time.gameObject.SetActive(true);
            SkipScene.isAd = 1;

            if (FireworkScripts.GG == 1)
            {
                SkipScene.gameIsAd = 1;
            }
        }
    }
}
