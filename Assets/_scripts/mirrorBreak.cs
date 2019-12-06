using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorBreak : MonoBehaviour
{
    public GameObject brokenMirror;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Instantiate(brokenMirror, transform.position, Quaternion.Euler(-28.909f, -90, 90));
            Destroy(this.gameObject);
        }
    }
}
