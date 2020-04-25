using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.MainMenu {
    public class Setting : MonoBehaviour {
        public static Setting Instance { get; private set; }

        private Animator m_Animator = null;
        private readonly int OPEN_HASH = Animator.StringToHash("Open");

        public bool IsOpening => m_Animator.GetBool(OPEN_HASH);

        private void Awake() {
            Instance = this;
            m_Animator = GetComponent<Animator>();
        }

        private void Start() {
            OnSoundChange();
            OnBgmChange();
            OnSEChange();

            OnTextSpeedChange();
        }

        public void OpenWindow() {
            m_Animator.SetBool(OPEN_HASH, true);
            m_MasterAudioSlider.Select();
        }
        public void CloseWindow() {
            m_Animator.SetBool(OPEN_HASH, false);
        }

        private void ChangeVolume(string volumeName, Slider slider, Text text) {
            float value = Mathf.InverseLerp(slider.minValue, slider.maxValue, slider.value);
            if (slider.value == slider.minValue) m_AudioMixer.SetFloat(volumeName, -80);
            else m_AudioMixer.SetFloat(volumeName, slider.value);
            text.text = "" + Mathf.RoundToInt(value * 100);
        }

        [Header("Audio Volume Control")]
        [SerializeField] private AudioMixer m_AudioMixer = null;

        [SerializeField] private Slider m_MasterAudioSlider = null;
        [SerializeField] private Text m_MasterVolumeText = null;
        public void OnSoundChange() {
            ChangeVolume("Master Volume", m_MasterAudioSlider, m_MasterVolumeText);
        }

        [SerializeField] private Slider m_BgmSlider = null;
        [SerializeField] private Text m_BgmVolumeText = null;
        public void OnBgmChange() {
            ChangeVolume("BGM Volume", m_BgmSlider, m_BgmVolumeText);
        }

        [SerializeField] private Slider m_SESlider = null;
        [SerializeField] private Text m_SEVolumeText = null;
        public void OnSEChange() {
            ChangeVolume("SE Volume", m_SESlider, m_SEVolumeText);
        }

        [Header("Text Type Speed Control")]
        [SerializeField] private Slider m_TextSpeedSlider = null;
        [SerializeField] private Text m_SpeedText = null;
        public float TextTypeIntervalTime { get; private set; }
        public void OnTextSpeedChange() {
            float val = Mathf.InverseLerp(m_TextSpeedSlider.minValue, m_TextSpeedSlider.maxValue, m_TextSpeedSlider.value);
            TextTypeIntervalTime = Mathf.Lerp(0.2f, 0, val);
            m_SpeedText.text = "" + Mathf.RoundToInt(val * 100);
        }
    }
}
