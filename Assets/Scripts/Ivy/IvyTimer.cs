using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y <= 3) 
        {
            var scale = transform.localScale;
            scale.y += 0.0005f;
            transform.localScale = scale;
            var pos = transform.position;
            pos.y += 0.00025f;
            transform.position = pos;
        }
    }
}
