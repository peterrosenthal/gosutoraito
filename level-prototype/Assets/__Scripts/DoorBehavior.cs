using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public GameObject[] linkedSwitches;

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
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Close()
    {
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
