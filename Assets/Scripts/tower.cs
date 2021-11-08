using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tower : MonoBehaviour
{
    public GameObject ivy_drawer;
    private float last_x = 0;
    private bool mousePressed = false;
    private bool started = false;
    private float tower_distance = 0;
    private Vector3 tower_start_pos;
    private Quaternion tower_start_rot;

    // Start is called before the first frame update
    void Start()
    {
        tower_start_pos = transform.position;
        tower_start_rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (started) 
        {
            gameObject.transform.Translate(-Vector3.up * Time.deltaTime, Space.World);
            tower_distance += Time.deltaTime;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
        }

        if (GameData.drawing_ivy && mousePressed) 
        {
            started = true;
            transform.Rotate(new Vector3(0, -((Screen.width / 2) - Input.mousePosition.x), 0) * Time.deltaTime * 1f);
        }
        if (GameData.last_ivy_pos.y <= -(tower_distance + 10f)) 
        {
            restart();
        }
        if (transform.position.y <= -16f)
        {

            SceneManager.LoadScene("End");
        }
    }
    void restart() 
    {
        print("restart");
        transform.position = tower_start_pos;
        transform.rotation = tower_start_rot;
        tower_distance = 0;
        ivy_drawer.GetComponent<IvyDrawer>().expire_ivy();
    }
}
