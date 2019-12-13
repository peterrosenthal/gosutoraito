using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    public GameObject linkedSwitch;
    private CrystalSwitch crystal;
    public bool isOpen = false;
    private Animator anim;
    new private AudioSource audio;

    void Start()
    {
        anim = GetComponent<Animator>();
        crystal = linkedSwitch.GetComponent<CrystalSwitch>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen && crystal._active)
        {
            Open();
        }
        else if (isOpen && !crystal._active)
        {
            Close();
        }
    }

    private void Open()
    {
        isOpen = true;
        anim.SetBool("wallDown", true);
        audio.Play();
    }

    private void Close()
    {
        isOpen = false;
        anim.SetBool("wallDown", false);
    }

}
