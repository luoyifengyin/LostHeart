using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

namespace MyGameApplication.Maze.NPC {
    public class Interactable : MonoBehaviour {
        [SerializeField] private ReactionCollection m_Reaction = null;
        private FollowTarget m_Follow;

        private void Reset() {
            m_Reaction = GetComponentInChildren<ReactionCollection>();

            m_Follow = GetComponent<FollowTarget>();
            if (m_Follow && m_Follow.enabled &&
                    transform.parent && transform.parent.GetComponents<Component>().Length > 1) {
                m_Follow.target = transform.parent;
            }
            //Collider collider = GetComponent<Collider>();
            //if (!(collider && collider.enabled) ||
            //    !(m_Follow.target == transform.parent && transform.parent.GetComponent<Collider>())) {

            //}
        }

        private void Awake() {
            if (!m_Reaction) m_Reaction = GetComponentInChildren<ReactionCollection>();
            if (!m_Reaction)
                Debug.LogWarning(string.Format("The gameObject named \"{0}\" " +
                    "has no Reaction MonoBehaviour attached to.", gameObject.name));
            m_Follow = GetComponent<FollowTarget>();
        }

        private void Start() {
            if (!m_Follow || !m_Follow.enabled) return;
            if (!m_Follow.target || m_Follow.target == transform.parent) {
                m_Follow.enabled = false;
            }
            else {
                m_Follow.offset = transform.position - m_Follow.target.position;
            }
        }

        public async Task Interact() {
            await m_Reaction.React();
        }
    }
}
