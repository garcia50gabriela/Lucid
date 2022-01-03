using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lucy_image : MonoBehaviour
{
    public GameObject happy;
    public GameObject sad;
    public GameObject angry;
    public GameObject worry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clear_image() 
    {
        happy.SetActive(false);
        sad.SetActive(false);
        angry.SetActive(false);
        worry.SetActive(false);
    }

    public void show_happy() 
    {
        happy.SetActive(true);
    }
    public void show_sad()
    {
        sad.SetActive(true);
    }
    public void show_angry()
    {
        angry.SetActive(true);
    }
    public void show_worry()
    {
        worry.SetActive(true);
    }
}
