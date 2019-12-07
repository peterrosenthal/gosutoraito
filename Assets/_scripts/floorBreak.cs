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
    Light mainLight;
    Collider wall;

    // Start is called before the first frame update
    void Start()
    {
        mainLight = GameObject.Find("mainLight").GetComponent<Light>();
        terrain = GameObject.Find("TerrainGroup_0");
        treee = GameObject.Find("treee");
        floor = GameObject.Find("fullMat");
        crystal = crystalSwitch.GetComponent<CrystalSwitch>();
        wall = GameObject.Find("oneDoorWall").GetComponent<Collider>();
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
            mainLight.intensity = 3f;
            AudioManager.S.ChangeMusic();
            //Debug.Log("repeat");
            StartCoroutine("fade");
        }
    }
    IEnumerator fade()
    {
        yield return new WaitForSeconds(1);
        wall.enabled=true;
        Destroy(GameObject.Find("brokenFloor(Clone)"));
    }
}
