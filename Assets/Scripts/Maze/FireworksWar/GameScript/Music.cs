    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    private AudioSource[] m_ArrayMusic;
    public static AudioSource BGM;
    public static AudioSource Lose1;
    public static AudioSource Lose2;
    public static AudioSource Win1;
    public static AudioSource Win2;
    public static AudioSource GetFireworks;
    public static AudioSource Attack;
    public static AudioSource Kill;
    public static AudioSource RocketPropGet;
    public static AudioSource CoinsPropGet;
    public static AudioSource EXPPropGet;
	// Use this for initialization
	void Start () {
        m_ArrayMusic = gameObject.GetComponents<AudioSource>();
        BGM = m_ArrayMusic[0];
        Lose1 = m_ArrayMusic[1];
        Lose2 = m_ArrayMusic[2];
        Win1 = m_ArrayMusic[3];
        Win2 = m_ArrayMusic[4];
        GetFireworks = m_ArrayMusic[5];
        Attack = m_ArrayMusic[6];
        Kill = m_ArrayMusic[7];
        RocketPropGet = m_ArrayMusic[8];
        CoinsPropGet = m_ArrayMusic[9];
        EXPPropGet = m_ArrayMusic[10];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
