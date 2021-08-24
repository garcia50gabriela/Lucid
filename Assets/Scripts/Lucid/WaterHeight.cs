using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeight : MonoBehaviour
{
    private Vector3 water_start;
    private Vector3 target_position;
    // Start is called before the first frame update
    void Start()
    {
        water_start = gameObject.transform.position;
        target_position = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_position, 0.01f);
    }

    public void cresent()
    {
        target_position = new Vector3(water_start.x, water_start.y - 0.25f, water_start.z);
    }
    public void half()
    {
        target_position = new Vector3(water_start.x, water_start.y, water_start.z);
    }
    public void full()
    {
        target_position = new Vector3(water_start.x, water_start.y + 0.25f, water_start.z);
    }
}
