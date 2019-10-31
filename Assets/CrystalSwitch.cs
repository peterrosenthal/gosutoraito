using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        print(go.name);
    }

    public void Activate()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void Deactivate()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
