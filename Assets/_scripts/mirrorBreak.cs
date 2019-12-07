using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorBreak : MonoBehaviour
{
    public GameObject brokenMirror;
    Animator anim;
    private void Start()
    {
        anim = GameObject.Find("UICanvas").GetComponent<Animator>();

    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f)
        {
            anim.SetBool("ByMirror", true);
            if (Input.GetMouseButton(1))
            {
                BreakMirror();
            }
        }
        
    }

    private void OnMouseExit()
    {
        anim.SetBool("ByMirror", false);
    }

    public void BreakMirror()
    {
        anim.SetBool("ByMirror", false);
        AudioManager.S.shatterSound.Play();
        Instantiate(brokenMirror, transform.position + transform.forward, Quaternion.Euler(-90,0,0));
        Destroy(this.gameObject);
    }
}
