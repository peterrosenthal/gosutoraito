using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    Animator anim;
    public Transform rootNode;
    public Transform originalParent;
    public GameObject thisMirror;
    public GameObject thisPrism;
    [SerializeField]
    public bool hasMirror
    {
        get { return thisMirror.activeInHierarchy; }
        set
        {
            transform.parent.GetChild(0).gameObject.SetActive(value);
        }
    }
    [SerializeField]
    public bool hasPrism
    {
        get { return thisPrism.activeInHierarchy; }
        set
        {
            transform.parent.GetChild(1).gameObject.SetActive(value);
        }
    }

    public bool locked = true;
    public bool startPedestal = false;
    public Vector3 oldPos;
    private bool moving = false;

    void Start()
    {
        anim = GameObject.Find("UICanvas").GetComponent<Animator>();
        rootNode = transform.parent;
        originalParent = rootNode.parent;
        thisMirror = transform.parent.GetChild(0).gameObject;
        thisPrism = transform.parent.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && !Input.GetMouseButton(1))
        {
            rootNode.parent = originalParent;
            anim.SetBool("nearGhostPedestal", false);
        }
    }


    private void OnMouseUp()
    {
        //Exit edit
        //rootNode.parent = originalParent;
        if (!startPedestal && !PlayerBehavior.S.controllerScript.editingMirror && Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f)
        {
            if (hasMirror) //Get Mirror
            {
                hasMirror = false;
                PlayerBehavior.S.mirrorCount++;
                InventoryUI.S.UpdateInterfaceText();
                AudioManager.S.mirrorShard.Play();

            }
            else if (hasPrism) //Get Prism
            {
                hasPrism = false;
                PlayerBehavior.S.prismCount++;
                InventoryUI.S.UpdateInterfaceText();
                AudioManager.S.mirrorShard.Play();

            }
            else //Place  object
            {
                if (PlayerBehavior.S.selectedItem == 0 && PlayerBehavior.S.mirrorCount > 0)
                {
                    hasMirror = true;
                    PlayerBehavior.S.mirrorCount--;
                    InventoryUI.S.UpdateInterfaceText();
                    AudioManager.S.mirrorShard.Play();

                }
                else if (PlayerBehavior.S.selectedItem == 1 && PlayerBehavior.S.prismCount > 0)
                {
                    hasPrism = true;
                    PlayerBehavior.S.prismCount--;
                    InventoryUI.S.UpdateInterfaceText();
                    AudioManager.S.mirrorShard.Play();

                }
            }
        }
    }


    private void OnMouseOver()
    {
        if (locked && Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f) //Normal Pedestal
        {
            anim.SetBool("nearPedestal", true);
        }
        else if (!locked && Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f) //Ghost Pedestal
        {
            anim.SetBool("nearGhostPedestal", true);
            if (Input.GetMouseButton(1))
            {
                //GetComponent<Renderer>().material.color = Color.white;
                rootNode.parent = PlayerBehavior.S.grabTransform;
                moving = true;
            }
            else
            {
                rootNode.parent = originalParent;
            }
        }
    }

    private void OnMouseExit()
    {
        if (!moving)
        {
            anim.SetBool("nearGhostPedestal", false);
            anim.SetBool("nearPedestal", false);
            
        }

    }
    
}
