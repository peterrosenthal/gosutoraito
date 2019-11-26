using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
    }

    private void OnMouseExit()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.transform != this.transform)
                child.gameObject.BroadcastMessage("OnMouseExit", options: SendMessageOptions.DontRequireReceiver);
        }
    }

}
