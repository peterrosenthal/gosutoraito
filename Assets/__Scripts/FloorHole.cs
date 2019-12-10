using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHole : MonoBehaviour
{
    public GameObject _lightEmitter;
    public GameObject linkedDoor;
    public bool doorOpened;
    public int waitSeconds = 2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lightEmitter && !_lightEmitter.GetComponent<LightEmitter>()._activeCrystals.Contains(this.gameObject))
        {
            doorOpened = false;
            _lightEmitter = null;
            linkedDoor.GetComponent<Animator>().SetBool("levelComplete", false);

        }
    }

    public void OpenDoor(GameObject light)
    {
        doorOpened = true;
        _lightEmitter = light;
        linkedDoor.GetComponent<Animator>().SetBool("levelComplete", true);
        AudioManager.S.doorOpen.Play();
        //StartCoroutine("DelayedSound");

    }

    IEnumerator DelayedSound()
    {
        yield return new WaitForSeconds(waitSeconds);
        
        linkedDoor.GetComponent<Animator>().SetBool("levelComplete", true);
    }
}
