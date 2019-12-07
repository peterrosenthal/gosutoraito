using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHole : MonoBehaviour
{
    public GameObject linkedDoor;
    public bool doorOpened;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        doorOpened = true;
        StartCoroutine("DelayedOpening");

    }

    IEnumerator DelayedOpening()
    {
        yield return new WaitForSeconds(3);
        
        linkedDoor.GetComponent<Animator>().SetBool("levelComplete", true);
        yield return new WaitForSeconds(.5f);
        AudioManager.S.doorOpen.Play();
    }
}
