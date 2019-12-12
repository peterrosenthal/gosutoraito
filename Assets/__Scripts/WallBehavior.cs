using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    public GameObject linkedSwitch;
    private CrystalSwitch crystal;
    public bool isOpen = false;
    private Animator anim;
    private MeshRenderer mesh;
    private Collider collider;
    private AudioSource audio;

    void Start()
    {
        anim = GetComponent<Animator>();
        crystal = linkedSwitch.GetComponent<CrystalSwitch>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen && crystal._active)
        {
            Open();
        }
        if (isOpen && !crystal._active)
        {
            Close();
        }
    }

    private void Open()
    {
        isOpen = true;
        anim.SetBool("wallDown", true);
        StartCoroutine("DelayedSound");
        StartCoroutine("Disable");
        
    }

    private void Close()
    {
        isOpen = false;
        StartCoroutine("DelayedSound");
        anim.SetBool("wallDown", false);
        mesh.enabled = true;
        collider.enabled = true;
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(3);
        if (isOpen)
        {
            mesh.enabled = false;
            collider.enabled = false;

        }
        else
        {
            //StartCoroutine("Disable");
        }
    }

    IEnumerator DelayedSound()
    {
        yield return new WaitForSeconds(1f);
        audio.Play();
    }

}
