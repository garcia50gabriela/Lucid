using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class final_journal : MonoBehaviour
{
    public Text header;
    public Text dream_text;
    public Text reflective_text;
    public Text date_text;

    // Start is called before the first frame update
    void Start()
    {
        //header.text = header.text.Replace("<name>", GameData.user_inputs["NAME_INPUT"]);

        dream_text.text = dream_text.text.Replace("$dream_mood", (string)GameData.variables["$dream_mood"]);
        dream_text.text = dream_text.text.Replace("$dream_quality", (string)GameData.variables["$dream_quality"]);
        dream_text.text = dream_text.text.Replace("$dream_time", (string)GameData.variables["$dream_time"]);
        dream_text.text = dream_text.text.Replace("$person", (string)GameData.variables["$person"]);
        dream_text.text = dream_text.text.Replace("$perspective", (string)GameData.variables["$perspective"]);

        var person_mood = "";
        var person_pov = "";
        var time_mood = "";
        var time_pov = "";
        if ((bool)GameData.variables["$person_mood_connection"])
        {
            person_mood = "My feelings towards the person in my dream may be related to the overall tone of my dream.";
        }
        else 
        {
            person_mood = "";
        }
        if ((bool)GameData.variables["$person_pov_connection"])
        {
            person_pov = "The closeness I have, or lack of, with the person from my dreams could explain the perspective of my dream.";
        }
        else
        {
            person_pov = "";
        }
        if ((bool)GameData.variables["$time_mood_connection"])
        {
            time_mood = "My feelings towards the point in time that my dream took place in seems to be connected to the overall tone of my dream.";
        }
        else
        {
            time_mood = "";
        }
        if ((bool)GameData.variables["$time_pov_connection"])
        {
            time_pov = "The importance that I place on the point in time in my dream seems like it could be represented through the perspective of my dream.";
        }
        else
        {
            time_pov = "";
        }
        reflective_text.text = reflective_text.text.Replace("$person_mood_connection", person_mood);
        reflective_text.text = reflective_text.text.Replace("$person_pov_connection", person_pov);
        reflective_text.text = reflective_text.text.Replace("$time_mood_connection", time_mood);
        reflective_text.text = reflective_text.text.Replace("$time_pov_connection", time_pov);

        date_text.text = System.DateTime.Now.ToShortDateString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void download_journal() 
    {
        ScreenCapture.CaptureScreenshot("MyDreamJournal.png");
    }

    public void show_journal() 
    {
        gameObject.SetActive(true);
    }
}
