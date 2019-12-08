using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    public AudioMixer masterMixer;

    public AudioSource shatterSound;
    public AudioSource floorBreak;
    public AudioSource mirrorShard;
    public AudioSource hitSwitch;
    public AudioSource hitSword;
    public AudioSource doorOpen;

    public AudioMixerSnapshot templeSongTransition;
    

    void Start()
    {
        S = this;
        shatterSound = GameObject.Find("ShatterGlass").gameObject.GetComponent<AudioSource>();
        floorBreak = GameObject.Find("ShatterFloor").gameObject.GetComponent<AudioSource>();
        mirrorShard = GameObject.Find("MirrorShard").gameObject.GetComponent<AudioSource>();
        doorOpen = GameObject.Find("DoorOpen").gameObject.GetComponent<AudioSource>();
        hitSword = GameObject.Find("HitSword").gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic()
    {
        templeSongTransition.TransitionTo(8f);
        floorBreak.Play();
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        float pitch,vol;
        masterMixer.GetFloat("background1_pitch", out pitch);
        masterMixer.SetFloat("background1_pitch", pitch - 0.01f);
        masterMixer.GetFloat("background1_vol", out vol);
        yield return new WaitForSeconds(.01f);
        if (vol > -70) StartCoroutine("Fade");
    }
}
