using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videos : MonoBehaviour
{
    public VideoPlayer video_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (video_player.isPaused) 
        {
            gameObject.SetActive(false);
        }
    }
}
