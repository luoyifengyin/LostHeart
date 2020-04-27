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

        public void OpenWindow() {
            m_Animator.SetBool(OPEN_HASH, true);
            m_MasterAudioSlider.Select();
        }
        public void CloseWindow() {
            m_Animator.SetBool(OPEN_HASH, false);
        }

        private void ChangeVolume(string volumeName, Slider slider, Text text) {
            float value = slider.value;
            if (slider.value == slider.minValue) value = -80;
            else m_AudioMixer.SetFloat(volumeName, value);
            text.text = "" + Mathf.RoundToInt(Mathf.InverseLerp(slider.minValue, slider.maxValue, slider.value) * 100);

            PlayerPrefs.SetFloat(volumeName, value);
        }

        [Header("Audio Volume Control")]
        [SerializeField] private AudioMixer m_AudioMixer = null;

        [SerializeField] private Slider m_MasterAudioSlider = null;
        [SerializeField] private Text m_MasterVolumeText = null;
        public void OnSoundChange() {
            float value = m_MasterAudioSlider.value;
            AudioListener.volume = m_MasterAudioSlider.value;
            m_MasterVolumeText.text = "" + Mathf.RoundToInt(value * 100);

            PlayerPrefs.SetFloat("MasterAudioVolume", value);
        }

        [SerializeField] private Slider m_BgmSlider = null;
        [SerializeField] private Text m_BgmVolumeText = null;
        public void OnBgmChange() {
            ChangeVolume("BGMVolume", m_BgmSlider, m_BgmVolumeText);
        }

        [SerializeField] private Slider m_SESlider = null;
        [SerializeField] private Text m_SEVolumeText = null;
        public void OnSEChange() {
            ChangeVolume("SEVolume", m_SESlider, m_SEVolumeText);
        }

        [Header("Text Type Speed Control")]
        [SerializeField] private Slider m_TextSpeedSlider = null;
        [SerializeField] private Text m_SpeedText = null;
        public float TextTypeIntervalTime { get; private set; }
        public void OnTextSpeedChange() {
            float val = Mathf.InverseLerp(m_TextSpeedSlider.minValue, m_TextSpeedSlider.maxValue, m_TextSpeedSlider.value);
            TextTypeIntervalTime = Mathf.Lerp(0.2f, 0, val);
            m_SpeedText.text = "" + Mathf.RoundToInt(val * 100);

            PlayerPrefs.SetFloat("TextTypeSpeed", val);
        }

        private void Start() {
            m_MasterAudioSlider.value = PlayerPrefs.GetFloat("MasterAudioVolume", m_MasterAudioSlider.value);
            m_BgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", m_BgmSlider.value);
            m_SESlider.value = PlayerPrefs.GetFloat("SEVolume", m_SESlider.value);

            m_TextSpeedSlider.value = PlayerPrefs.GetFloat("TextTypeSpeed", m_TextSpeedSlider.value);
        }
    }
}
