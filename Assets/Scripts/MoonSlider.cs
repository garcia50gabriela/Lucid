using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSlider : MonoBehaviour
{
    public Camera camera;
    private bool waxing_pressed = false;
    private bool waning_pressed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var v3Pos = new Vector3(0.9f, 0.9f, 1.5f);
        transform.position = camera.ViewportToWorldPoint(v3Pos);

        if (waxing_pressed) 
        {
            gameObject.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
        }
        if (waning_pressed)
        {
            gameObject.transform.Rotate(0f, -50f * Time.deltaTime, 0f);
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
