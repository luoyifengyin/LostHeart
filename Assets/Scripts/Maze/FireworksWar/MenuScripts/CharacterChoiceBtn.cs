using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoiceBtn : MonoBehaviour {
    public List<Sprite> Character;
    public Sprite UI1;
    public Sprite UI2;
    public Sprite UI3;
    public Sprite UI4;
    public Image CharacterImage;
    public Image xuanze;
    int flag = 1;

    static public int xuanzee=1;
	// Use this for initialization
	void Start () {
        Character.Add(UI1);
        Character.Add(UI2);
        Character.Add(UI3);
        Character.Add(UI4);
	}
	
	// Update is called once per frame
	void Update () {
        if (flag == 1)
        {
           
            CharacterImage.sprite = Character[0];
        }
        if (flag == 2)
        {
            
            CharacterImage.sprite = Character[1];
        }
        if (flag == 3)
        {
            
            CharacterImage.sprite = Character[2];
        }
        if (flag == 4)
        {
            
            CharacterImage.sprite = Character[3];
        }
        if (xuanzee == 1)
        {
            xuanze.GetComponent<RectTransform>().anchoredPosition = new Vector2(-253, 121);
        }
        if (xuanzee == 2)
        {
            xuanze.GetComponent<RectTransform>().anchoredPosition = new Vector2(-81, 121);
        }
        if (xuanzee == 3)
        {
            xuanze.GetComponent<RectTransform>().anchoredPosition = new Vector2(75, 121);
        }
        if (xuanzee == 4)
        {
            xuanze.GetComponent<RectTransform>().anchoredPosition = new Vector2(244, 121);
        }

	}

    public void next()
    {
        if (flag < 4)
        {
            flag++;
        }
    }
    public void last()
    {
        if (flag > 1)
        {
            flag--;
        }
    }

    public void zhuangbei()
    {
        if (SkipScene.CoinNum >= 1)
        {
            if (flag == 1 && xuanzee!=1)
            {
                SkipScene.CoinNum -= 1;
                xuanzee=1;
            }
            if (flag == 2&& xuanzee!=2)
            {
                SkipScene.CoinNum -= 1;
                xuanzee = 2;
            }
            if (flag == 3&& xuanzee!=3)
            {
                SkipScene.CoinNum -= 1;
                xuanzee=3;
            }
            if (flag == 4&& xuanzee!=4)
            {
                SkipScene.CoinNum -= 1;
                xuanzee=4;
            }
        }
    }
}
