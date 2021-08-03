using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainBlock : MonoBehaviour
{
    private float first_side = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move_block() 
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + (first_side));
        first_side = first_side * -1f;
    }
}
