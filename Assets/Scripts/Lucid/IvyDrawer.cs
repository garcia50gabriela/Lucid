using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyDrawer : MonoBehaviour
{
    public GameObject core_block;
    public GameObject ivy;
    public GameObject wallParent;
    public GameObject mountainParent;
    private bool mousePressed = false;
    private Vector3 check = new Vector3(0.1F, 0.1F, 0.1F);
    private GameObject lastIvy;
    private float timePassed = 0f;
    private bool firstClick = false;
    private float IvyInstantiationTimer = 0.05f;
    private float IvyDestroyTimer = 0.02f;

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

        /*if (timePassed > 15f)
        {
            expire_ivy();
        }*/
    }

    void drawIvy()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "mountain")
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
                    }
                    IvyInstantiationTimer = 0.02f;
                }
            }
            if (hit.transform.tag == "tower")
            {
                float dist_from_last_ivy = Vector3.Distance(hit.point, lastIvy.transform.position);
                if (dist_from_last_ivy < Mathf.Abs(0.1f))
                {

                    lastIvy = Instantiate(ivy, hit.point, ivy.transform.rotation, wallParent.transform);
                    var rot = mountainParent.transform.rotation;
                    rot.y = -rot.y;
                    lastIvy.transform.localRotation = rot;
                    firstClick = true;
                }
            }
        }

    }

    void expire_ivy()
    {
        timePassed = 0;
        firstClick = false;
        lastIvy = ivy;
        foreach (Transform child in wallParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
