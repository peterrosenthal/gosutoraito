using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    public AudioSource shatterSound;
    public AudioSource backgroundMusic;

    void Start()
    {
        S = this;
        shatterSound = transform.Find("ShatterGlass").gameObject.GetComponent<AudioSource>();
        backgroundMusic = transform.Find("BackgroundTrack").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
