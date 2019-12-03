using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrineDoor1 : MonoBehaviour
{
    Animator door;
    GameObject treee;
    GameObject terrain;
    void Start()
    {
        terrain = GameObject.Find("TerrainGroup_0");
        treee = GameObject.Find("treee");
        door = GameObject.Find("door").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            door.SetBool("close", true);
            StartCoroutine("timer");
        }
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(2);
        terrain.SetActive(false);
        treee.SetActive(false);
    }
}
