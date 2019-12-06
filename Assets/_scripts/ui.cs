using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour
{   public GameObject defaultt;
    public GameObject onShrineEnter;
    public GameObject onByMirror;
    public GameObject onByPickupable;
    public GameObject onEditMirror;
    public GameObject onPedestalGhost;
    public GameObject onPedestalNonGhost;
    GameObject treee;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        treee = GameObject.Find("treee");
        anim = GetComponent<Animator>();
    }

    // This script will likely need access to your other scripts to check conditionals, I just dont have them in the file I'm working in :(
    void Update()
    {
        if (!treee.activeInHierarchy)
        {
            anim.SetBool("shrineEntered", true);
        }

    }
}
