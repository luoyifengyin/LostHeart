using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Second
{
    public class AdultSoundEffects : MonoBehaviour
    {
        AudioSource m_AudioSource;
        AudioClip m_Shot;
        AudioClip m_Box;
        AudioClip m_Health;
        // Start is called before the first frame update
        void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_Shot = Resources.Load<AudioClip>("Second/shot");
            m_Box = Resources.Load<AudioClip>("Second/box");
            m_Health = Resources.Load<AudioClip>("Second/health");
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ShotSoundEffects()
        {
            m_AudioSource.clip = m_Shot;
            m_AudioSource.Play();
        }
        public void Box()
        {
            m_AudioSource.clip = m_Box;
            m_AudioSource.Play();
        }
        public void Health()
        {
            m_AudioSource.clip = m_Health;
            m_AudioSource.Play();
        }
    }
}