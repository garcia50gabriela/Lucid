using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load_high_tide() 
    {
        SceneManager.LoadScene("HighTide");
    }
    public void load_moon_flower()
    {
        SceneManager.LoadScene("MoonFlower");
    }
    public void load_ivy()
    {
        SceneManager.LoadScene("ivy");
    }
}
