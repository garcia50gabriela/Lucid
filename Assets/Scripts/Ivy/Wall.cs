using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    float goalRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 60F;

        if (Input.GetKeyDown(KeyCode.A)) {
            goalRotation = transform.rotation.eulerAngles.y - 90;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            goalRotation = transform.rotation.eulerAngles.y + 90;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, goalRotation, 0), speed * Time.deltaTime);
    }
}
