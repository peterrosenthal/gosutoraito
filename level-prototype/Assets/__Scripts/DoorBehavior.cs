using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public GameObject[] linkedSwitches;
    private bool _isOpen = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckSwitches())
        {
            Open();
            

        }
        else
        {
            Close();
            
        }
    }

    private bool CheckSwitches()
    {
        if(linkedSwitches.Length > 0)
        {
            foreach (GameObject crystal in linkedSwitches)
            {
                CrystalSwitch c = crystal.GetComponent<CrystalSwitch>();
                if (!c._active) return false;
            }
            return true;
        }
        return false;
    }

    private void Open()
    {
        _isOpen = true;
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Close()
    {
        _isOpen = false;
        GetComponent<Renderer>().enabled = true;
        GetComponent<BoxCollider>().isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }
}
