using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeight : MonoBehaviour
{
    private Vector3 water_start;
    private float target_y = 0f;
    private Vector3 target_pos;
    public GameObject Moon;
    // Start is called before the first frame update
    void Start()
    {
        water_start = gameObject.transform.position;
        target_pos = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target_y = Mathf.Sin(Moon.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) / 10f;
        target_pos.y = (water_start.y - target_y);
        transform.position = Vector3.MoveTowards(transform.position, target_pos, 0.01f);
    }
}
