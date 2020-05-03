using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Maze.NPC {
    public class Interactable : MonoBehaviour {

        [SerializeField] private float m_ScopeOfInteracitve = 2f;

        [SerializeField] private ReactionCollection m_Reaction = null;

        public float ScopeOfInteractive => m_ScopeOfInteracitve;

        private void Reset() {
            m_Reaction = GetComponentInChildren<ReactionCollection>();
        }

        private void Awake() {
            if (!m_Reaction) m_Reaction = GetComponentInChildren<ReactionCollection>();
            if (!m_Reaction)
                Debug.LogWarning(string.Format("The gameObject named \"{0}\" " +
                    "has no Reaction MonoBehaviour attached to.", gameObject.name));
        }

        public async Task Interact() {
            await m_Reaction.React();
        }
    }
}
