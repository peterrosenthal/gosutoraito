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
        if (Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f && Input.GetMouseButton(1))
        {
            BreakMirror();
            //PlayerBehavior.S.mouseOverObject = gameObject;
        }
        anim.SetBool("ByMirror", true);
    }

    private void OnMouseExit()
    {
        anim.SetBool("ByMirror", false);
    }

    public void BreakMirror()
    {
        Instantiate(brokenMirror, transform.position + transform.forward, Quaternion.identity);
        Destroy(this.gameObject);
        anim.SetBool("ByMirror", false);
    }
}
