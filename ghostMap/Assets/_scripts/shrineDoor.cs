using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrineDoor : MonoBehaviour
{
    Animator door;

    void Start()
    {
        
        door = GameObject.Find("door").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            door.SetBool("open", true);
        }
    }
}
