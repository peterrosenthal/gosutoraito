using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    private Transform rootNode;
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
    public Vector3 oldPos;
    void Start()
    {
        rootNode = transform.parent;
        originalParent = rootNode.parent;
        thisMirror = transform.parent.GetChild(0).gameObject;
        thisPrism = transform.parent.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Edit
        if (!locked && !PlayerBehavior.S.controllerScript.editingMirror && Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f)
        {
            rootNode.parent = PlayerBehavior.S.grabTransform;
            oldPos = transform.position;
        }
        
    }

    private void OnMouseUp()
    {
        //Exit edit
        rootNode.parent = originalParent;
        if (!PlayerBehavior.S.controllerScript.editingMirror && Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f)
        {
            if (hasMirror) //Get Mirror
            {
                hasMirror = false;
                PlayerBehavior.S.mirrorCount++;
                InventoryUI.S.UpdateInterfaceText();
            }
            else if (hasPrism) //Get Prism
            {
                hasPrism = false;
                PlayerBehavior.S.prismCount++;
                InventoryUI.S.UpdateInterfaceText();
            }
            else //Place  object
            {
                if (PlayerBehavior.S.selectedItem == 0 && PlayerBehavior.S.mirrorCount > 0)
                {
                    hasMirror = true;
                    PlayerBehavior.S.mirrorCount--;
                    InventoryUI.S.UpdateInterfaceText();
                }
                else if (PlayerBehavior.S.selectedItem == 1 && PlayerBehavior.S.prismCount > 0)
                {
                    hasPrism = true;
                    PlayerBehavior.S.prismCount--;
                    InventoryUI.S.UpdateInterfaceText();
                }
            }
        }
    }


    private void OnMouseOver()
    {
        if (!locked && Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
    
}
