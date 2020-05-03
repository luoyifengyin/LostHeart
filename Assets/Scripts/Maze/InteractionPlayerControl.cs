using MyGameApplication.Maze.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Maze {
    public class InteractionPlayerControl : MonoBehaviour {
        [SerializeField] private float m_InteractableMaxDistance = 2f;
        
        public bool IsInteracting { get; private set; }

        public Interactable interactionTest;
        private async void Start() {
            if (interactionTest) await interactionTest.Interact();
        }

        private void OnTriggerStay(Collider other) {
            if (IsInteracting) return;
            if (other.GetComponentInParent<Interactable>()) {

                Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = Camera.main.ScreenPointToRay(pos);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractableMaxDistance)) {
                    Interactable interactable = hitInfo.collider.GetComponentInParent<Interactable>();
                    if (interactable) {
                        if (CrossPlatformInputManager.GetButtonDown("Submit"))
                            Interact(interactable);
                    }
                }

                if (Input.GetMouseButtonDown(0)) OnInteractionClick();
            }
        }

        public void OnInteractionClick() {
            if (IsInteracting) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractableMaxDistance)) {
                Interactable interactable = hitInfo.collider.GetComponentInParent<Interactable>();
                if (interactable)
                    Interact(interactable);
            }
        }

        private async void Interact(Interactable interactable) {
            IsInteracting = true;
            await interactable.Interact();
            IsInteracting = false;
        }
    }
}
