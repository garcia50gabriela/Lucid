using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "ivy")
        {
            GameData.overlapIvy++;
            GameData.insideIvyTrigger = true;
            GameData.ivyPos = Col.gameObject.transform.position;
            GameData.ivyParent = Col.gameObject.transform.parent; 
        }
    }

    void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.tag == "ivy")
        {
            GameData.overlapIvy--;
            if (GameData.overlapIvy <= 0)
            {
                //GameData.insideIvy = false;
                GameData.insideIvyTrigger = false;
                GameData.overlapIvy = 0;
            }
        }
    }
}
