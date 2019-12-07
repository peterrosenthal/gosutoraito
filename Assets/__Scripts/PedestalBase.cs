using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalBase : MonoBehaviour
{
    Animator anim;
    bool ghostPedestal;

    void Start()
    {
/*        anim = GameObject.Find("UICanvas").GetComponent<Animator>();
        if (gameObject.tag != "StartPedestal")
            ghostPedestal = !(transform.GetChild(2).GetComponent<PedestalScript>().locked); //Checks to see if associated pedestal is a ghost or not
*/    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.transform != this.transform)
                child.gameObject.BroadcastMessage("OnMouseDown", options: SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnMouseUp()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.transform != this.transform)
                child.gameObject.BroadcastMessage("OnMouseUp", options: SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnMouseOver()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.transform != this.transform)
                child.gameObject.BroadcastMessage("OnMouseOver", options: SendMessageOptions.DontRequireReceiver);
        }
/*        if (gameObject.tag != "StartPedestal")
        {
            if (ghostPedestal) anim.SetBool("nearGhostPedestal", true);
            else anim.SetBool("nearPedestal", true);

        }
*/    }

    private void OnMouseExit()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.transform != this.transform)
                child.gameObject.BroadcastMessage("OnMouseExit", options: SendMessageOptions.DontRequireReceiver);
        }
/*        if (gameObject.tag != "StartPedestal")
        {
            if (ghostPedestal) anim.SetBool("nearGhostPedestal", false);
            else anim.SetBool("nearPedestal", false);

        }
*/    }

}
