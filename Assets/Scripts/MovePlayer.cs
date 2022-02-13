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
    private float jumpHeight = 0.3f;
    private float gravityValue = -3f;
    // z calculation
    private Vector3 mountain_bounds;

    //ivy index
    private int ivyIndex = 0;
    private bool currentlyMoving = false;
    private bool movingForward = false;
    

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
            if (GameData.insideIvy)
            {
                apply_vine_movement();
            }
            else
            {
                apply_movement();
            }
            if (GameData.insideIvyTrigger && !GameData.insideIvy)
            {
                check_for_ivy_activate();
            }
            else if (GameData.insideIvy) 
            {
                check_for_ivy_deactivate();
            }
        }
        else 
        {
            just_apply_gravity();
        }
        respawn_if_fallen();
        transform.position = new Vector3(0f, transform.position.y, player_z_position());
    }

    void just_apply_gravity() 
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        transform.position = new Vector3(0f, transform.position.y, player_z_position());
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    void apply_vine_movement() 
    {
        if (Input.GetAxis("Horizontal") != 0) 
        {
            if (ivyIndex <= (GameData.ivyParent.childCount -1) && ivyIndex >= 0)
            {
                if (Input.GetAxis("Horizontal") > 0 && !currentlyMoving && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 0.05f))
                {
                    if (ivyIndex < (GameData.ivyParent.childCount - 1))
                    {
                        ivyIndex += 1;
                        currentlyMoving = true;
                        movingForward = true;
                    }
                    else 
                    {
                        stop_climbing();
                    }
                    
                }
                else if (Input.GetAxis("Horizontal") < 0  && !currentlyMoving && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), 0.05f))
                {
                    if (ivyIndex > 0)
                    {
                        ivyIndex -= 1;
                        currentlyMoving = true;
                        movingForward = false;
                    }
                    else 
                    {
                        stop_climbing();
                    }
                    
                }
                if (currentlyMoving) 
                {
                    var nextIvy = GameData.ivyParent.GetChild(ivyIndex);
                    Debug.Log("ivyindex:");
                    Debug.Log(ivyIndex);
                    var next_pos = Vector3.MoveTowards(transform.position, nextIvy.transform.position, Time.deltaTime * 7f);
                    Debug.Log("next position");
                    Debug.Log(next_pos.x);
                    transform.position = new Vector3(transform.position.x, next_pos.y, transform.position.z);
                    float angle = Vector3.SignedAngle(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(next_pos.x, 0f, next_pos.z), Vector3.up);
                    Debug.Log("angle");
                    Debug.Log(angle);
                    //Mountains.transform.eulerAngles = new Vector3(0, Mountains.transform.eulerAngles.y - angle, 0);
                    Mountains.transform.rotation *= Quaternion.AngleAxis(-angle, Vector3.up);
                    if ((angle >= 0.0 && Input.GetAxis("Horizontal") > 0) || (angle <= 0.0 && Input.GetAxis("Horizontal") < 0))
                    {
                        currentlyMoving = false;
                        Debug.Log("reset");
                    }
                }

            }

        }
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
            Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed) * Time.deltaTime, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Mountains.transform.Rotate(0f, Input.GetAxis("Horizontal") * (playerSpeed) * Time.deltaTime, 0f);
        }
        Vector3 move = new Vector3(0f, 0f, 0f);
        controller.Move(move * Time.deltaTime * playerSpeed/6);

        /*if (groundedPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1 * gravityValue);
        }*/
         
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        transform.position = new Vector3(0f, transform.position.y, player_z_position());
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    float player_z_position() 
    {
        var m_z = mountain_bounds.z / 2;
        var m_y = mountain_bounds.y + 0.5f;
        var p_y = transform.position.y;
        
        var player_z = ((m_y - p_y) * m_z) / m_y;
        return -(player_z + 0.29f);
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
            stop_climbing();
            transform.position = GameData.start_position;
            Mountains.transform.rotation = GameData.start_rotation;
        }
    }

    void OnTriggerExit(Collider Col) 
    {
        if (Col.gameObject.tag == "floatingPlatform")
        {
            transform.parent = null;
        }
    }

    void check_for_ivy_activate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameData.insideIvy)
            {
                GameData.insideIvy = false;
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else 
            {
                ivyIndex = 0;
                GameData.insideIvy = true;
                gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
            }
            
        }
    }

    void check_for_ivy_deactivate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameData.insideIvy)
            {
                GameData.insideIvy = false;
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            }
        }
    }

    public void stop_climbing() 
    {
        GameData.insideIvy = false;
    }
}
