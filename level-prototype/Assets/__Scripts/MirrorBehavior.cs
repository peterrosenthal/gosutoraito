using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBehavior : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseOver()
    {
        if (PlayerBehavior.S.mouseOverObject != gameObject)
        {
            PlayerBehavior.S.mouseOverObject = gameObject;
        }
    }

    private void OnMouseExit()
    {
        if (PlayerBehavior.S.mouseOverObject == gameObject)
        {
            PlayerBehavior.S.mouseOverObject = null;
        }
    }
}
