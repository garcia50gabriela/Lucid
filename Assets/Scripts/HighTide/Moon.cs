using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public GameObject Water;
    public int direction = -1;
    public bool isMoving = false;
    private float tide_start_z;
    private float moon_start_y;
    private Vector3 rotate_around;
    private float rotation_sum = 0;
    // Start is called before the first frame update
    void Start()
    {
        tide_start_z = Water.transform.position.z;
        moon_start_y = transform.position.y;
        rotate_around = new Vector3(0f, 0f, tide_start_z);
    }

    // Update is called once per frame
    void Update()
    {
        //moveMoonWithArrows();
        moveMoon();

        moonPhases();

        calculateTidePosition();
    }

    void moveMoon()
    {
        if (isMoving)
        {
            var rotation = 20 * direction * Time.deltaTime;
            rotation_sum = rotation_sum + rotation;
            transform.RotateAround(rotate_around, Vector3.forward, rotation);
            if (Mathf.Abs(rotation_sum) >= 180)
            {
                isMoving = false;
                rotation_sum = 0;
            }
        }
    }

    void moveMoonWithArrows()
    {
        // arrow moon position
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(new Vector3(0f, 0f, -5f), Vector3.forward, -20 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(new Vector3(0f, 0f, -5f), Vector3.forward, 20 * Time.deltaTime);
        }
    }

    void moonPhases()
    {
        // phases
        transform.Rotate(0f, 0.05f, 0f, Space.Self);
    }

    void calculateTidePosition()
    {
        // phase effect
        var phase_strength = Mathf.Abs(Mathf.Sin(transform.localRotation.eulerAngles.y * Mathf.Deg2Rad));
        // tide position
        var pos = Water.transform.position;
        // effect of moon position
        var z_pos = ((tide_start_z / moon_start_y) * Mathf.Abs(transform.position.y));
        // scale down effect
        z_pos = z_pos + ((tide_start_z - z_pos) / 2f);
        // apply phases effect scaled by 1/10
        z_pos = z_pos * (1 - (phase_strength * 0.1f));
        Water.transform.position = new Vector3(pos.x, pos.y, z_pos);
    }
}
