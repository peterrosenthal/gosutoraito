using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour
{   public GameObject defaultt;
    public GameObject onShrineEnter;
    public GameObject onShrineEnterBackup;
    public GameObject onByMirror;
    public GameObject onByPickupable;
    public GameObject onEditMirror;
    public GameObject onPedestalGhost;
    public GameObject onPedestalNonGhost;
    GameObject treee;
    bool entered = false;
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
        if (!treee.activeInHierarchy && !entered)
        {
            anim.SetBool("shrineEntered", true);
            entered = true;
        }
        if (!defaultt.activeSelf && !onShrineEnter.activeSelf && !onByMirror.activeSelf && !onByPickupable.activeSelf && !onEditMirror.activeSelf && !onPedestalGhost.activeSelf && !onPedestalNonGhost.activeSelf)
        {
            onShrineEnterBackup.SetActive(true);

        }

    }
}
