using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSlider : MonoBehaviour
{
    public Camera camera;
    private bool mousePressed = false;
    private bool waxing_pressed = false;
    private bool waning_pressed = false;
    // Start is called before the first frame update
    void Start()
    {

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
        if (Input.mousePosition.x > 1000 & Input.mousePosition.x < 1200 & Input.mousePosition.y > 450 & Input.mousePosition.y < 600) 
        {
            if (mousePressed)
            {
                transform.Rotate(new Vector3(0, (1150 - Input.mousePosition.x) * 10, 0) * Time.deltaTime * 1f);
            }
        }
        
    }

    public void start_waxing() 
    {
        waxing_pressed = true;
    }
    public void stop_waxing()
    {
        waxing_pressed = false;
    }

    public void start_waning() 
    {
        waning_pressed = true;
    }
    public void stop_waning()
    {
        waning_pressed = false;
    }
}
