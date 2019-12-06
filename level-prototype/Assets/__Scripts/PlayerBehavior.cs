using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior S;
    private bool _holdingSword;
    private Transform _thisCamera;
    private Animator anim;


    public int mirrorCount = 0;
    public int prismCount = 0;
    public Transform grabTransform;
    public GameObject mouseOverObject;
    public FirstPersonController controllerScript;

    public GameObject _editMirror;
    public Quaternion _previousMirrorRotation;

    public int selectedItem = 0;

    public enum equipment
    {
        mirror,
        prism
    }

    public bool holdingSword
    {
        get { return _holdingSword; }
    }
    void Start()
    {
        anim = GameObject.Find("UICanvas").GetComponent<Animator>();

        S = this;
        controllerScript = GetComponent<FirstPersonController>();
        _thisCamera = transform.GetChild(0);
        grabTransform = transform.GetChild(1);

    }

    // Update is called once per frame
    void Update()
    {


        if (controllerScript.editingMirror) //things that happen while editing the mirror
        {
            anim.SetBool("nearPedestal", false);
            anim.SetBool("nearGhostPedestal", false);

            anim.SetBool("isEditingObjectOnPedestal", true);

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 currentRotation = _editMirror.transform.rotation.eulerAngles;
            Vector3 newRotation = currentRotation;
            if (_editMirror.tag == "Mirror")
            {
                newRotation = new Vector3(currentRotation.x + vertical, currentRotation.y + horizontal, currentRotation.z);
            }
            else if (_editMirror.tag == "LightRay")
            {
                newRotation = new Vector3(currentRotation.x - vertical, currentRotation.y + horizontal, currentRotation.z);
            }
            
            _editMirror.transform.rotation = Quaternion.Euler(newRotation);
            
            if (Input.GetKeyUp(KeyCode.E)) //save the edits
            {
                controllerScript.editingMirror = false;
                _editMirror = null;
                anim.SetBool("isEditingObjectOnPedestal", false);

            }
            else if (Input.GetKeyUp(KeyCode.Q)) //cancel the edits
            {
                controllerScript.editingMirror = false;
                _editMirror.transform.rotation = _previousMirrorRotation;
                _editMirror = null;
                anim.SetBool("isEditingObjectOnPedestal", false);

            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (mouseOverObject != null)
                {
                    switch (mouseOverObject.tag)
                    {
                        case "Mirror":
                        case "LightRay":    
                            controllerScript.editingMirror = true;
                            _editMirror = mouseOverObject;
                            _previousMirrorRotation = _editMirror.transform.rotation;
                            break;
                    }
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (mouseOverObject != null)
                {
                    switch (mouseOverObject.tag)
                    {
                        case "LargeMirror":
                            AudioManager.S.shatterSound.Play();
                            mirrorBreak m = mouseOverObject.GetComponent<mirrorBreak>();
                            m.BreakMirror();
                            Destroy(mouseOverObject);
                            break;
                    }
                }
            }
        }
        
        
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _holdingSword = true;
        }
        else
        {
            _holdingSword = false;
        }


        
    }

    public bool CanReflect(Vector3 lightHit)
    {
        Vector3 targetDir = (lightHit - transform.position).normalized;
        if (Vector3.Angle(transform.forward, targetDir) < 90)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(Camera.main.transform.position, Camera.main.transform.forward));
    }
}
