using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBehavior : MonoBehaviour
{
    private Quaternion _currentRotation;
    void Start()
    {
        _currentRotation = transform.parent.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Rotate15()
    {
        Quaternion newRotation = _currentRotation;
        newRotation.y += 15;
        _currentRotation = newRotation;
        transform.parent.transform.rotation = _currentRotation;
    }
}
