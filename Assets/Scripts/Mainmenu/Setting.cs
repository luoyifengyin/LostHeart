using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.MainMenu {
    public class Setting : MonoBehaviour {
        public static Setting Instance { get; private set; }

        private Animator m_Animator;

        private void Awake() {
            Instance = this;
            m_Animator = GetComponent<Animator>();
        }

        public void OpenWindow() {
            m_Animator.SetBool("Open", true);
        }
        public void CloseWindow() {
            m_Animator.SetBool("Open", false);
        }

        [SerializeField] private Slider m_AudioSlider;
        [SerializeField] private Text m_VolumeText;

        public void OnSoundChange() {
            AudioListener.volume = (m_AudioSlider.value - m_AudioSlider.minValue)
                / (m_AudioSlider.maxValue - m_AudioSlider.minValue);
            m_VolumeText.text = "" + (int)(AudioListener.volume * 100);
        }
    }
}
