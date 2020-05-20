using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public GameObject text;
    public int TotalTime = 60;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CountDown()
    {
        while (TotalTime >= 0)
        {
            text.GetComponent<Text>().text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;

        }
    }
}
