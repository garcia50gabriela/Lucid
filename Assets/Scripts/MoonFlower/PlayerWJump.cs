using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWJump : MonoBehaviour
{
    public GameObject main_camera;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 0.3f;
    private float gravityValue = -9.81f;
    private float distToGround;
    private bool inside = false;
    private Vector3 ivyPos;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        distToGround = gameObject.GetComponent<BoxCollider>().bounds.extents.y;
        GameData.start_position = transform.position;
    }

    void Update()
    {
        if (inside == true )
        {
            //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), ivyPos.y, 0);
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), ivyPos.y, 0);

            if (main_camera.transform.eulerAngles.y == 0f)
            {
                move = new Vector3(Input.GetAxis("Horizontal"), ivyPos.y, 0);
            }
            else if (main_camera.transform.eulerAngles.y == -90f || main_camera.transform.eulerAngles.y == 270f)
            {
                move = new Vector3(0, ivyPos.y, Input.GetAxis("Horizontal"));
            }
            else if (main_camera.transform.eulerAngles.y == 180f)
            {
                move = new Vector3(-Input.GetAxis("Horizontal"), ivyPos.y, 0);
            }
            else if (main_camera.transform.eulerAngles.y == 90f)
            {
                move = new Vector3(0, ivyPos.y, -Input.GetAxis("Horizontal"));
            }

            controller.Move(move * Time.deltaTime * playerSpeed);
        }
        if (inside == false)
        {
            apply_movement();
        }

        respawn_if_fallen();
    }

    void apply_movement() 
    {
        groundedPlayer = IsGrounded();
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = change_controls_based_on_cam();

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (groundedPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        check_if_fallen_in_water();
    }
    
    Vector3 change_controls_based_on_cam() 
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (main_camera.transform.eulerAngles.y == 0f)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        else if (main_camera.transform.eulerAngles.y == -90f || main_camera.transform.eulerAngles.y == 270f)
        {
            move = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        }
        else if (main_camera.transform.eulerAngles.y == 180f)
        {
            move = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        }
        else if (main_camera.transform.eulerAngles.y == 90f)
        {
            move = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        }
        return move;
        /*Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (main_camera.transform.eulerAngles.y >= 135 && main_camera.transform.eulerAngles.y <= 225)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        else if (main_camera.transform.eulerAngles.y >= 225 && main_camera.transform.eulerAngles.y <= 315)
        {
            move = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        }
        else if (main_camera.transform.eulerAngles.y >= -45 && main_camera.transform.eulerAngles.y <= 45)
        {
            move = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        }
        else if (main_camera.transform.eulerAngles.y >= 45 && main_camera.transform.eulerAngles.y <= 135)
        {
            move = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        }
        return move;*/
    }
    
    void respawn_if_fallen()
    {
        if (transform.position.y < 0)
        {
            transform.position = GameData.start_position;
        }
    }

    bool IsGrounded(){
        RaycastHit hit;
        //return 
        bool did_hit = Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f);
        if (did_hit) 
        {
            if (hit.collider.gameObject.name.Contains("floating_platform"))
            {
                transform.parent = hit.collider.gameObject.transform;
            }
            else
            {
                transform.parent = null;
            }
        }

        return did_hit;
    }
    void check_if_fallen_in_water() 
    {
        RaycastHit hit;
        //return 
        bool did_hit = Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f);
        if (did_hit)
        {
            if (hit.collider.gameObject.tag.Contains("water"))
            {
                transform.position = GameData.start_position;
            }
        }
    }
    void onCollisionEnter(Collider Col) 
    {
        if (Col.gameObject.tag == "water")
        {
            //respawn
            transform.position = GameData.start_position;
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "ivy")
        {
            inside = true;
            ivyPos = Col.gameObject.transform.position;
        }
        if (Col.gameObject.tag == "block") 
        {
            check_for_block_activate(Col);
        }
    }

    void OnTriggerStay(Collider Col) 
    {
        if (Col.gameObject.tag == "ivy")
        {
            inside = true;
        }
        if (Col.gameObject.tag == "block")
        {
            check_for_block_activate(Col);
        }
    }

    void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.tag == "ivy")
        {
            inside = false;
        }
    }

    void check_for_block_activate(Collider Col) 
    {
        if (Input.GetKey(KeyCode.E))
        {
            Col.gameObject.GetComponent<MountainBlock>().move_block();
        }
    }
}
