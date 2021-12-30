 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightmare : MonoBehaviour
{
    public GameObject moon;
    private Quaternion nightmare_start_rotation;
    private Quaternion moon_start_rotation;
    private float direction = -1f;
    private float rot_min = 0.1f;
    private float rot_max = 0.1f;
    private bool direction_switch = false;
    // Start is called before the first frame update
    void Start()
    {
        nightmare_start_rotation = transform.localRotation;
        moon_start_rotation = moon.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //var offset = Mathf.Sin(moon.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        rot_min = -1; //- offset;
        rot_max = 1; //+ offset;

        transform.Rotate(new Vector3(0, direction, 0) * Time.deltaTime * 5f, Space.Self);
        
        if (transform.localRotation.y <= nightmare_start_rotation.y + (rot_min/50f))
        {
            direction = 1f;
        }
        if (transform.localRotation.y >= nightmare_start_rotation.y + (rot_max/50f))
        {
            direction = -1f;
        }

        
    }
}
