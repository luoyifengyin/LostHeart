using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGameApplication.Second
{
    public class AdultMonster : MonoBehaviour
    {
        public GameObject Leap;
        private NavMeshAgent agent;
        public Transform target;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            
         }

        // Update is called once per frame
        void Update()
        {
            agent.SetDestination(Leap.transform.position);
        }
    }
}