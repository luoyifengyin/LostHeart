using MyGameApplication.Maze.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Maze {
    public class InteractionPlayerControl : MonoBehaviour {
        private float m_InteractableMaxDistance = 2f;
        
        public Interactable Interacting { get; private set; }

        private void Awake() {
            m_InteractableMaxDistance = GetComponent<SphereCollider>().radius;
        }

        private void OnTriggerStay(Collider other) {
            if (Interacting) return;

            if (other.GetComponentInChildren<Interactable>()) {

                Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = Camera.main.ScreenPointToRay(pos);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractableMaxDistance)) {
                    Interactable interactable = hitInfo.collider.GetComponentInChildren<Interactable>();
                    if (interactable) {
                        if (CrossPlatformInputManager.GetButtonDown("Submit"))
                            Interact(interactable);
                    }
                }

                if (Input.GetMouseButtonDown(0)) OnInteractionClick();
            }
        }

        public void OnInteractionClick() {
            if (Interacting) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractableMaxDistance)) {
                Interactable interactable = hitInfo.collider.GetComponentInChildren<Interactable>();
                if (interactable)
                    Interact(interactable);
            }
        }

        private async void Interact(Interactable interactable) {
            Interacting = interactable;
            await interactable.Interact();
            Interacting = null;
        }
    }
}
