using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSwitch : MonoBehaviour
{
    public bool _active = false;
    public GameObject _lightEmitter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lightEmitter != null && !_lightEmitter.GetComponent<LightEmitter>()._activeCrystals.Contains(this.gameObject))
        {
            Deactivate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
    }

    public void Activate()
    {
        _active = true;
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void Deactivate()
    {
        _active = false;
        _lightEmitter = null;
        GetComponent<Renderer>().material.color = Color.white;
    }
}
