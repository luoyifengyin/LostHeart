using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapArrow : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, target.eulerAngles.z));
    }
}
