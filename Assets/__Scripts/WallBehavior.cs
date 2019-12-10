using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    public GameObject[] linkedSwitches;
    private bool isOpen = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            if (CheckSwitches())
                Open();

        }
        else if (!CheckSwitches())
        {
            Close();
        }
    }

    private void Open()
    {
        isOpen = true;
    }

    private void Close()
    {
        isOpen = false;
    }

    private bool CheckSwitches()
    {
        if (linkedSwitches.Length > 0)
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
}
