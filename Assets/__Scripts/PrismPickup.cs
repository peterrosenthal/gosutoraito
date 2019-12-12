using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismPickup : MonoBehaviour
{
    Animator anim;
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
/*        PlayerBehavior.S.prismCount++;
        InventoryUI.S.UpdateInterfaceText();
        Destroy(gameObject);*/
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 6f)
        {
            anim.SetBool("ByPickupable", true);
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("ByPickupable", false);
                PlayerBehavior.S.prismCount++;
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
