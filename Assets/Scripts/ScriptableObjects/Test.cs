using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Test : ScriptableObject
{
    public string str;
    public GameObject go;
    private void Awake() {
        Debug.Log("scriptable object awake");
    }

    public virtual void Func() {

    }
}
