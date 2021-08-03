using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStar : MonoBehaviour
{
    public GameObject ScoreCounter;
    public bool dontDestroy = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dontDestroy)
        {
            Destroy(gameObject, 4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            ScoreCounter.GetComponent<Score>().incrementScore();
            Destroy(gameObject);
        }
    }
}
