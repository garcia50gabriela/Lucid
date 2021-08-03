using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ivy : MonoBehaviour
{
    public GameObject ivy;
    public GameObject wallParent;
    bool mousePressed = false;
    Vector3 check = new Vector3(0.1F, 0.1F, 0.1F);
    GameObject lastIvy;
    // Start is called before the first frame update
    void Start()
    {
        lastIvy = ivy;

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
    }

    void drawIvy() 
    {

        var v3 = Input.mousePosition;
        v3.z = -(Camera.main.transform.position.z + 0.51f);
        v3 = Camera.main.ScreenToWorldPoint(v3);

        //if (!Physics.CheckBox(v3, check, ivy.transform.rotation) && (v3.x <= lastIvy.x + 0.1F && v3.y <= lastIvy.y + 0.1F) && (v3.x >= lastIvy.x - 0.1F && v3.y >= lastIvy.y - 0.1F)) {
        if (!Physics.CheckBox(v3, check, ivy.transform.rotation) && (Vector3.Distance(lastIvy.transform.position, v3) < 0.1f))
        {
            if (v3.x >= -0.55 && v3.x <= 0.55) 
            {
                lastIvy = Instantiate(ivy, v3, ivy.transform.rotation, wallParent.transform);
            }
        }
    }
}
