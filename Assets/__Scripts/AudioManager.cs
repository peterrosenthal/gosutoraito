using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    public AudioMixer masterMixer;

    public AudioSource shatterSound;
    public AudioSource backgroundMusic;

    public AudioMixerSnapshot templeSong;
    

    void Start()
    {
        S = this;
        shatterSound = transform.Find("ShatterGlass").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic()
    {
        templeSong.TransitionTo(5f);
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        float pitch,vol;
        masterMixer.GetFloat("background1_pitch", out pitch);
        masterMixer.SetFloat("background1_pitch", pitch+.1f);
        masterMixer.GetFloat("background1_vol", out vol);
        yield return new WaitForSeconds(.01f);
        if (vol > -70) StartCoroutine("Fade");

    }
}
