using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSwitch : MonoBehaviour
{
    public bool _active = false;
    public bool _startSwitch = false;
    public GameObject _lightEmitter;
    public GameObject _startLight;

    void Start()
    {
        if (_startSwitch)
        {
            _startLight = GameObject.Find("StartLight");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_lightEmitter != null && (!_lightEmitter.GetComponent<LightEmitter>()._activeCrystals.Contains(this.gameObject) || 
            !_lightEmitter.activeInHierarchy))
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
        if (_startSwitch && _startLight) _startLight.GetComponent<LightEmitter>()._startSwitchOn = true;
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void Deactivate()
    {
        _active = false;
        _lightEmitter = null;
        GetComponent<Renderer>().material.color = Color.white;
    }
}
