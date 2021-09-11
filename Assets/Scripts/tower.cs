using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tower : MonoBehaviour
{
    private float last_x = 0;
    private bool mousePressed = false;
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

        if (mousePressed) 
        { 
            float x_pos = (Input.mousePosition.x - Screen.width / 2) / 500f;
            gameObject.transform.Rotate(0f, x_pos, 0f);
            gameObject.transform.Translate(-Vector3.up * Time.deltaTime, Space.World);
        }
        if (gameObject.transform.position.y <= -20f) 
        {
            SceneManager.LoadScene("End");
        }
    }
}
