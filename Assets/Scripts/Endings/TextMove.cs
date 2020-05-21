using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector2.up * Time.deltaTime * 20);
    }
}
