using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallIvy : MonoBehaviour
{
    public GameObject core_block;
    public GameObject ivy;
    public GameObject wallParent;
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

        block_n = core_block.transform.position.z + core_block.transform.localScale.z / 2;
        block_s = core_block.transform.position.z - core_block.transform.localScale.z / 2;
        block_e = core_block.transform.position.x + core_block.transform.localScale.x / 2;
        block_w = core_block.transform.position.x - core_block.transform.localScale.x / 2;
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
        IvyInstantiationTimer -= Time.deltaTime;
        if (IvyInstantiationTimer <= 0)
        {
            var v3 = Input.mousePosition;

            if (cam.transform.eulerAngles.y == 0f)
            {
                v3.z = -(cam.transform.position.z - block_s);
            }
            else if (cam.transform.eulerAngles.y == -90f || cam.transform.eulerAngles.y == 270f)
            {
                v3.z = (cam.transform.position.x - block_e);
            }
            else if (cam.transform.eulerAngles.y == 180f)
            {
                v3.z = (cam.transform.position.z - block_n);
            }
            else if (cam.transform.eulerAngles.y == 90f)
            {
                v3.z = -(cam.transform.position.x - block_w);
            }

            v3 = Camera.main.ScreenToWorldPoint(v3);
            float dist_from_last_ivy = Vector3.Distance(v3, lastIvy.transform.position);
            float dist_from_first_ivy = Vector3.Distance(v3, ivy.transform.position);
            if (dist_from_last_ivy < Mathf.Abs(0.1f) && dist_from_first_ivy < Mathf.Abs(1f))
            {
                lastIvy = Instantiate(ivy, v3, ivy.transform.rotation, wallParent.transform);
                firstClick = true;
            }
            IvyInstantiationTimer = 0.02f;
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
