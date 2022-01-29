 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightmare : MonoBehaviour
{
    public bool isVertical = false;
    private Quaternion nightmare_start_rotation;
    private Vector3 nightmare_start_position;
    private float direction = -1f;
    private float rot_min = 0.1f;
    private float rot_max = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        nightmare_start_rotation = transform.localRotation;
        nightmare_start_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isVertical)
        {
            moveHorizontally();
        }
        else 
        {
            moveVertically();
        }
        
    }
    void moveHorizontally() 
    {
        rot_min = -1;
        rot_max = 1;

        transform.Rotate(new Vector3(0, direction, 0) * Time.deltaTime * 5f, Space.Self);

        if (transform.localRotation.y <= nightmare_start_rotation.y + (rot_min / 50f))
        {
            direction = 1f;
        }
        if (transform.localRotation.y >= nightmare_start_rotation.y + (rot_max / 50f))
        {
            direction = -1f;
        }
    }

    void moveVertically()
    {
        rot_min = -0.1f;
        rot_max = 0.1f;

        if (transform.position.y >= nightmare_start_position.y + (rot_max))
        {
            direction = -1f;
        }
        if (transform.position.y <= nightmare_start_position.y + (rot_min))
        {
            direction = 1f;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + (direction * Time.deltaTime * 0.1f), transform.position.z);
    }
}
