using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dream_journal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open_journal() 
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
