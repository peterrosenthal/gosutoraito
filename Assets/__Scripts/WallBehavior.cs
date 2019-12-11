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

    void Start()
    {
        anim = GetComponent<Animator>();
        crystal = linkedSwitch.GetComponent<CrystalSwitch>();
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            if (crystal._active)
                Open();
        }
        else if (!crystal._active)
        {
            Close();
        }
    }

    private void Open()
    {
        isOpen = true;
        anim.SetBool("wallDown", true);
    }

    private void Close()
    {
        isOpen = false;
        anim.SetBool("wallDown", false);
    }

}
