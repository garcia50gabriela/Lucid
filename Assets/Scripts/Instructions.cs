using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject journal_background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void instructions_submit_and_close()
    {
        journal_background.SetActive(false);
        gameObject.SetActive(false);
    }
}
