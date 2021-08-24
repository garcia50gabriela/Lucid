using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject main_camera;
    public GameObject Mountains;

    // movement
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.5f;
    // jump
    private float distToGround;
    private bool groundedPlayer;
    private float jumpHeight = 0.20f;
    private float gravityValue = -9.81f;
    // ivy
    private bool insideIvy = false;
    private bool insideIvyTrigger = false;
    private Vector3 ivyPos;
    private int overlapIvy = 0;
    // block
    private bool insideBlock = false;
    private GameObject currentBlock;
    

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        distToGround = gameObject.GetComponent<BoxCollider>().bounds.extents.y;
        GameData.start_position = transform.position;
        GameData.start_rotation = Mountains.transform.rotation;
    }

    void Update()
    {
        if (!GameData.story_mode) 
        {
            if (insideIvy)
            {
                apply_vine_movement();
            }
            else
            {
                apply_movement();
            }

            if (insideBlock)
            {
                check_for_block_activate();
            }
            if (insideIvyTrigger)
            {
                check_for_ivy_activate();
            }
        }
        respawn_if_fallen();
    }

    void apply_vine_movement() 
    {
        transform.position = new Vector3(transform.position.x, ivyPos.y, transform.position.z);
        Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed / 100f), 0f);
    }

    void apply_movement()
    {
        groundedPlayer = IsGrounded();
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 lower_pos = new Vector3(transform.position.x, transform.position.y-0.05f, transform.position.z);
        Vector3 higher_pos = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        if (!Physics.Raycast(lower_pos, transform.TransformDirection(Vector3.right), 0.025f) && !Physics.Raycast(higher_pos, transform.TransformDirection(Vector3.right), 0.025f))
        {
            Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed / 100f), 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed / 100f), 0f);
        }
        Vector3 move = new Vector3(0f, 0f, 0f);
        if (!Physics.Raycast(lower_pos, transform.TransformDirection(Vector3.forward), 0.0025f))
        {
            move = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            move = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        }
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (groundedPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }
         
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
        bool did_hit = Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.001f);

        return did_hit;
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "ivy")
        {
            overlapIvy++;
            insideIvyTrigger = true;
            ivyPos = Col.gameObject.transform.position;
            if (overlapIvy > 0)
            {
                insideIvyTrigger = true;
            }
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

    void check_for_block_activate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentBlock.GetComponent<MountainBlock>().move_block();
        }
    }

    void check_for_ivy_activate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            insideIvy = !insideIvy;
        }
    }
}
