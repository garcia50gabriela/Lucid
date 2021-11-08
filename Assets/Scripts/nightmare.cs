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
        nightmare_start_rotation = transform.rotation;
        moon_start_rotation = moon.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, direction, 0) * Time.deltaTime * 10f);
        
        if (transform.rotation.y <= nightmare_start_rotation.y - rot_min)
        {
            direction = 1f;
        }
        if (transform.rotation.y >= nightmare_start_rotation.y + rot_max)
        {
            direction = -1f;
        }


        var offset =  Mathf.Sin(moon.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
        if (offset > 0) 
        {
            rot_max = 0.1f * offset;
            rot_min = 0.1f;
        }
        if (offset < 0)
        {
            rot_min = Mathf.Abs( 0.1f * offset);
            rot_max = 0.1f;
        }
        if (offset == 0)
        {
            rot_min = 0.1f;
            rot_max = 0.1f;
        }
    }
}
