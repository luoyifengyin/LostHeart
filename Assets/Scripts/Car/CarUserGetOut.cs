using MyGameApplication.Maze.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Car {
    public class CarUserGetOut : MonoBehaviour {
        [SerializeField] private ReactionCollection m_Reaction = null;

        async void Update() {
            if (CrossPlatformInputManager.GetButtonDown("Fire2")) {
                if (m_Reaction) await m_Reaction.React();
            }
        }
    }
}
