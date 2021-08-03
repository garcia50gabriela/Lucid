using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers : MonoBehaviour
{
    public int direction;
    public GameObject moon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !moon.GetComponent<Moon>().isMoving)
        {
            print("start moving");
            moon.GetComponent<Moon>().isMoving = true;
            moon.GetComponent<Moon>().direction = direction;
        }
    }
}
