using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class floorBreak : MonoBehaviour
{
    GameObject floor;
    public GameObject brokenFloor;
    public GameObject crystalSwitch;
    CrystalSwitch crystal;
    GameObject treee;
    GameObject terrain;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("TerrainGroup_0");
        treee = GameObject.Find("treee");
        floor = GameObject.Find("fullMat");
        crystal = crystalSwitch.GetComponent<CrystalSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (floor != null && crystal._active)
        {
            //Ensures terrain and trees are disabled
            terrain.SetActive(false);
            treee.SetActive(false);
            //Destroy Floor
            Instantiate(brokenFloor, floor.transform.position, Quaternion.identity);
            Destroy(floor);
            //Make player fall
            PlayerBehavior.S.controllerScript.editingMirror = false;
            //Change Music
            AudioManager.S.ChangeMusic();
            StartCoroutine("fade");
        }
    }
    IEnumerator fade()
    {
        yield return new WaitForSeconds(1);
        Destroy(GameObject.Find("brokenFloor(Clone)"));
    }
}
