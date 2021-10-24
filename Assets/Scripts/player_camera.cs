using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_camera : MonoBehaviour
{
    public GameObject player_obj;
    private float player_start_y;
    private float speed = 0.025f;
    private float target_z;
    // Start is called before the first frame update
    void Start()
    {
        player_start_y = player_obj.transform.position.y;
        target_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        target_z = player_obj.transform.position.z - 0.4f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player_obj.transform.position.x, player_obj.transform.position.y, target_z), step);
    }
}
