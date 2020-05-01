using MyGameApplication.Maze.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Maze {
    public class InteractionPlayerControl : MonoBehaviour {
        [SerializeField] private float m_InteractableMaxDistance = 2f;
        private int m_InteractiveLayer;
        
        public bool IsInteracting { get; private set; }

        public Interactable interactionTest;

        private void Awake() {
            m_InteractiveLayer = LayerMask.NameToLayer("Interactable");
        }

        private async void Start() {
            if (interactionTest) await interactionTest.Interact();
        }

        private void OnTriggerStay(Collider other) {
            print(other.gameObject.layer + "  " + m_InteractiveLayer);
            if (!IsInteracting && other.gameObject.layer == m_InteractiveLayer) {
            print("on trigger stay");

                Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                Ray ray = Camera.main.ScreenPointToRay(pos);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractableMaxDistance, 1 << m_InteractiveLayer)) {
                    print("hit");
                    Interactable interactable = hitInfo.collider.gameObject.GetComponentInParent<Interactable>();
                    if (interactable && hitInfo.distance < interactable.ScopeOfInteractive) {
                        print("hi");
                        if (CrossPlatformInputManager.GetButtonDown("Submit"))
                            Interact(interactable);
                    }
                }

                if (Input.GetMouseButtonDown(0)) {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hitInfo, m_InteractableMaxDistance, 1 << m_InteractiveLayer)) {
                        Interactable interactable = hitInfo.collider.gameObject.GetComponentInParent<Interactable>();
                        if (interactable && hitInfo.distance < interactable.ScopeOfInteractive)
                            Interact(interactable);
                    }
                }
            }
        }

        private async void Interact(Interactable interactable) {
            IsInteracting = true;
            await interactable.Interact();
            IsInteracting = false;
        }
    }
}
