using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> core_blocks = new List<GameObject>();
    private GameObject core_block;
    private float player_last_x;

    private float block_n, block_s, block_e, block_w;
    //private float start_x, start_z;
    // Start is called before the first frame update
    void Start() 
    {
        core_block = core_blocks[GameData.current_block];
        calculate_limits();
        player_last_x = player.transform.position.x;
        //start_x = transform.position.x;
        //start_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        move_camera();
        /*var rot_y = (-(player.transform.position.x - player_last_x)*90f)/core_block.transform.localScale.x;
        gameObject.transform.Rotate(0f, rot_y, 0f);
        var pos_y = player.transform.position.y;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, pos_y, gameObject.transform.position.z);*/
        //gameObject.transform.LookAt(player.transform);

        check_level();
        player_last_x = player.transform.position.x;
    }
    void calculate_limits() 
    {
        block_n = core_block.transform.position.z + core_block.transform.localScale.z / 2;
        block_s = core_block.transform.position.z - core_block.transform.localScale.z / 2;
        block_e = core_block.transform.position.x + core_block.transform.localScale.x / 2;
        block_w = core_block.transform.position.x - core_block.transform.localScale.x / 2;
    }

    void check_level()
    {
        if (player.transform.position.y > (core_block.transform.position.y + core_block.transform.localScale.y / 2)) 
        { 
            GameData.current_block += 1;
            core_block = core_blocks[GameData.current_block];
            calculate_limits();
            GameData.start_position = player.transform.position;
        }

        if (player.transform.position.y < (core_block.transform.position.y - core_block.transform.localScale.y / 2))
        {
            GameData.current_block -= 1;
            core_block = core_blocks[GameData.current_block];
            calculate_limits();
        }
    }

    void move_camera() 
    {
        var pos_y = player.transform.position.y;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, pos_y, gameObject.transform.position.z);
        if (player.transform.position.x < block_w)
        {

            gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }
        else if (player.transform.position.x > block_e)
        {
            gameObject.transform.eulerAngles = new Vector3(0f, -90f, 0f);
        }
        else if (player.transform.position.z < block_n)
        {
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (player.transform.position.z > block_s)
        {
            gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}
