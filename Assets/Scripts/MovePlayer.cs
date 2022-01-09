using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject main_camera;
    public GameObject Mountains;
    public GameObject MountainMesh;

    // movement
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 20f;
    // jump
    private float distToGround;
    private bool groundedPlayer;
    private float jumpHeight = 1f;
    private float gravityValue = -3f;
    // ivy
    private bool insideIvy = false;
    private bool insideIvyTrigger = false;
    private GameObject currentIvy;
    private Transform ivyParent;
    private Transform next_ivy;
    private bool currently_moving = false;
    private int ivyIndex;
    private int overlapIvy = 0;
    // block
    private bool insideBlock = false;
    private GameObject currentBlock;
    // z calculation
    private Vector3 mountain_bounds;
    

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        distToGround = gameObject.GetComponent<BoxCollider>().bounds.extents.y;
        GameData.start_position = transform.position;
        GameData.start_rotation = Mountains.transform.rotation;

        mountain_bounds = MountainMesh.GetComponent<Renderer>().bounds.size;
    }

    void Update()
    {
        if (!GameData.story_mode)
        {
            //if (insideIvy)
            //{
                //apply_vine_movement();
            //}
            //else
            //{
                apply_movement();
                
            //}
            //if (insideIvyTrigger)
            //{
                //check_for_ivy_activate();
            //}
        }
        else 
        {
            just_apply_gravity();
        }
        //respawn_if_fallen();
        //transform.position = new Vector3(transform.position.x, transform.position.y, player_z_position());
    }

    void just_apply_gravity() 
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, player_z_position());
        //transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    void apply_vine_movement() 
    {
        
        if (!currently_moving)
        {
            if (ivyIndex <= (ivyParent.childCount - 1) && ivyIndex > 0)
            {
                if (Input.GetAxis("Horizontal") < 0f)
                {
                    ivyIndex--;
                }
                else if (Input.GetAxis("Horizontal") > 0f)
                {
                    ivyIndex++;
                }
                // print(Input.GetAxis("Horizontal"));

                next_ivy = ivyParent.GetChild(ivyIndex);
                currently_moving = true;
            }
            //print(ivyIndex);
        }

        var new_position = Vector3.MoveTowards(transform.position, next_ivy.position, 0.00001f);
        print(new_position);
        transform.position = new Vector3(transform.position.x, new_position.y, transform.position.z);
        //Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed) * Time.deltaTime, 0f);
        //transform.RotateAround();
        if (new_position.y == transform.position.y && new_position.x == transform.position.x)
        {
            currently_moving = false;
        }
    }

    void apply_movement()
    {
        groundedPlayer = IsGrounded();
        print(groundedPlayer);
        //controller.Move(playerVelocity * Time.deltaTime);
        //Vector3 lower_pos = new Vector3(transform.position.x, transform.position.y-0.05f, transform.position.z);
        //Vector3 higher_pos = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        //if (!Physics.Raycast(lower_pos, transform.TransformDirection(Vector3.right), 0.025f) && !Physics.Raycast(higher_pos, transform.TransformDirection(Vector3.right), 0.025f))
        //{
        //Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed) * Time.deltaTime, 0f);
        //transform.RotateAround(Mountains.transform.position, Vector3.up, Input.GetAxis("Horizontal") * (playerSpeed) * Time.deltaTime);
        //}
        //else if (Input.GetAxis("Horizontal") < 0)
        //{
        //Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed) * Time.deltaTime, 0f);
        if (Input.GetAxis("Horizontal") != 0)
        {
            //print(Mountains.transform.position);
            var prev_transform = transform.position;
            transform.RotateAround(Mountains.transform.position, Vector3.up, -Input.GetAxis("Horizontal") * (playerSpeed));
            var next_transform = transform.position;
            transform.position = prev_transform;
            //var rot = RotateAbout(transform.position, Mountains.transform.position, Vector3.up, -Input.GetAxis("Horizontal") * (playerSpeed) * Time.deltaTime);
            playerVelocity = next_transform - prev_transform;
            playerVelocity.y = 0f;
        }
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (groundedPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1 * gravityValue);
        }
        //}
        //Vector3 move = new Vector3(0f, 0f, 0f);
        //controller.Move(move * Time.deltaTime * playerSpeed/6);

        /*if (groundedPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1 * gravityValue);
        }*/

        // transform.position = new Vector3(transform.position.x, transform.position.y + playerVelocity.y, transform.position.z);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, player_z_position());
        //transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    /*Vector3 RotateAbout(Vector3 position, Vector3 rotatePoint, Vector3 axis, float angle) {
           return (Quaternion.AngleAxis(angle, axis));
    }*/

    float player_z_position() 
    {
        var m_z = mountain_bounds.z / 2;
        var m_y = mountain_bounds.y + 0.5f;
        var p_y = transform.position.y;
        
        var player_z = ((m_y - p_y) * m_z) / m_y;
        return -(player_z + 0.15f);
    }

    void respawn_if_fallen()
    {
        if (transform.position.y < 0)
        {
            transform.position = GameData.start_position;
            Mountains.transform.rotation = GameData.start_rotation;
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        bool did_hit = Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.005f);

        return did_hit;
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "ivy")
        {
            /*if (Col.bounds.center.y > (transform.position.y - 0.03f) && Col.bounds.center.y < (transform.position.y + 0.03f))
            {*/
                overlapIvy++;
                insideIvyTrigger = true;
                currentIvy = Col.gameObject;
            /*}*/
        }
        if (Col.gameObject.tag == "block")
        {
            insideBlock = true;
            currentBlock = Col.gameObject;
        }
        if (Col.gameObject.tag == "water")
        {
            //respawn
            transform.position = GameData.start_position;
            Mountains.transform.rotation = GameData.start_rotation;
        }
        if (Col.gameObject.tag == "floatingPlatform")
        {
            transform.parent = Col.gameObject.transform;
        }
        if (Col.gameObject.tag == "nightmare") 
        {
            transform.position = GameData.start_position;
            Mountains.transform.rotation = GameData.start_rotation;
        }
    }

    void OnTriggerExit(Collider Col) 
    {
        if (Col.gameObject.tag == "ivy")
        {
            overlapIvy--;
            if (overlapIvy <= 0)
            {
                insideIvy = false;
                insideIvyTrigger = false;
                overlapIvy = 0;
            }
        }
        if (Col.gameObject.tag == "floatingPlatform")
        {
            transform.parent = null;
        }
        if (Col.gameObject.tag == "block")
        {
            insideBlock = false;
        }
    }

    void check_for_ivy_activate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (insideIvy)
            {
                insideIvy = false;
                overlapIvy = 0;
                ivyIndex = 0;
            }
            else 
            {
                insideIvy = true;
                ivyParent = currentIvy.transform.parent;
                ivyIndex = currentIvy.transform.GetSiblingIndex();
            }
            
        }
    }
    public void stop_climbing() 
    {
        insideIvy = false;
        insideIvyTrigger = false;
        overlapIvy = 0;
    }
}
