using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorShard : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("UICanvas").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f)
        {
            anim.SetBool("ByPickupable", true);
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("ByPickupable", false);
                PlayerBehavior.S.mirrorCount++;
                InventoryUI.S.UpdateInterfaceText();
                AudioManager.S.mirrorShard.Play();
                Destroy(gameObject);

            }
        }
            
    }

    private void OnMouseExit()
    {
        anim.SetBool("ByPickupable", false);
    }
}
