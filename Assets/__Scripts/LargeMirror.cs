using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMirror : MonoBehaviour
{
    public GameObject shardPrefab;
    public int shards = 2;
    private void OnMouseOver()
    {
        if(Vector3.Distance(transform.position, PlayerBehavior.S.transform.position) < 5f)
        {
            PlayerBehavior.S.mouseOverObject = gameObject;
        }
    }

    public void BreakMirror()
    {
        //PlayerBehavior.S.mirrorCount += shards;
        //Instatiate shards at this position
        for(int i = 0; i< shards; i++)
        {
            Instantiate(shardPrefab, transform.position - (i * .5f * transform.forward), Quaternion.identity);
        }
        

    }
}
