using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyDrawer : MonoBehaviour
{
    public GameObject core_block;
    public GameObject ivy;
    public GameObject wallParent;
    public GameObject mountainParent;
    public GameObject IvyMesh;
    private bool mousePressed = false;
    private Vector3 check = new Vector3(0.1F, 0.1F, 0.1F);
    private GameObject lastIvy;
    private float timePassed = 0f;
    private bool firstClick = false;
    private float IvyInstantiationTimer = 0.05f;
    private float IvyDestroyTimer = 0.02f;
    private int ivyColliderCounter = 0;
    private Vector3 first_mesh_point;
    private Vector3 last_mesh_point;

    private Camera cam;
    private float block_n, block_s, block_e, block_w;

    // Start is called before the first frame update
    void Start()
    {
        lastIvy = ivy;
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
        }

        if (mousePressed)
        {
            drawIvy();
        }


        if (firstClick)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed > 15f)
        {
            expire_ivy();
        }
    }

    void drawIvy()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "trellis")
            {
                IvyInstantiationTimer -= Time.deltaTime;
                if (IvyInstantiationTimer <= 0)
                {
                    float dist_from_last_ivy = Vector3.Distance(hit.point, lastIvy.transform.position);
                    float dist_from_first_ivy = Vector3.Distance(hit.point, ivy.transform.position);
                    if (dist_from_last_ivy < Mathf.Abs(0.1f) && dist_from_first_ivy < Mathf.Abs(1f))
                    {
                        lastIvy = Instantiate(ivy, hit.point, ivy.transform.rotation, wallParent.transform);
                        //var rot = mountainParent.transform.rotation;
                        //rot.y = -rot.y;
                        //lastIvy.transform.localRotation = rot;
                        firstClick = true;
                        ivyColliderCounter++;
                        if (ivyColliderCounter == 1)
                        {
                            first_mesh_point = lastIvy.transform.position;
                        }
                        if (ivyColliderCounter == 2)
                        {
                            last_mesh_point = lastIvy.transform.position;
                        }
                        if (ivyColliderCounter >= 2) 
                        {
                            ivyColliderCounter = 0;
                            var cal_rot = Mathf.Atan2(last_mesh_point.y - first_mesh_point.y, last_mesh_point.x - first_mesh_point.x);
                            Instantiate(IvyMesh, hit.point, Quaternion.Euler(10f, 0f, Mathf.Rad2Deg*cal_rot-90), wallParent.transform);
                        }
                    }
                    IvyInstantiationTimer = 0.1f;
                    // click off to delete
                    if (dist_from_last_ivy > 0.2) 
                    {
                        expire_ivy();
                    }
                }
            }
            if (hit.transform.tag == "tower")
            {
                float dist_from_last_ivy = Vector3.Distance(hit.point, lastIvy.transform.position);
                if (dist_from_last_ivy < Mathf.Abs(0.1f))
                {
                    GameData.drawing_ivy = true;
                    lastIvy = Instantiate(ivy, hit.point, ivy.transform.rotation, wallParent.transform);
                    var rot = mountainParent.transform.rotation;
                    rot.y = -rot.y;
                    lastIvy.transform.localRotation = rot;
                    firstClick = true;
                    ivyColliderCounter++;
                }
                else
                {
                    GameData.drawing_ivy = false;
                }
            }
            else 
            {
                GameData.drawing_ivy = false;
            }
        }
        else
        {
            GameData.drawing_ivy = false;
        }

    }

    void expire_ivy()
    {
        print("expire ivy");
        timePassed = 0;
        firstClick = false;
        lastIvy = ivy;
        ivyColliderCounter = 0;
        foreach (Transform child in wallParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
