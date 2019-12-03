using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorBreak : MonoBehaviour
{
    GameObject floor;
    public GameObject brokenFloor;
    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.Find("fullMat");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Destroy(floor);
            Instantiate(brokenFloor, floor.transform.position, Quaternion.identity);
            StartCoroutine("fade");
        }
    }
    IEnumerator fade()
    {
        yield return new WaitForSeconds(1);
        Destroy(GameObject.Find("brokenFloor(Clone)"));
    }
}
