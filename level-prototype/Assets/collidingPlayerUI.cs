using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidingPlayerUI : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("UICanvas").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pedestal")
        {
            anim.SetBool("nearPedestal", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pedestal")
        {
            anim.SetBool("nearPedestal", false);
        }
    }
}
