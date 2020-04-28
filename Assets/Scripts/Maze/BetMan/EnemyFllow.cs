using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFllow : MonoBehaviour {

    public Transform target;
    NavMeshAgent a;
    // Use this for initialization
    void Start()
    {
        a = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        a.destination = target.position;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("gg");
        }
    }
}
