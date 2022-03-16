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
            person_mood = GameData.person_mood_text_true;
        }
        else 
        {
            person_mood = GameData.person_mood_text_false;
        }
        if ((bool)GameData.variables["$person_pov_connection"])
        {
            person_pov = GameData.person_pov_text_true;
        }
        else
        {
            person_pov = GameData.person_pov_text_false;
        }
        if ((bool)GameData.variables["$time_mood_connection"])
        {
            time_mood = GameData.time_mood_text_true;
        }
        else
        {
            time_mood = GameData.time_mood_text_false;
        }
        if ((bool)GameData.variables["$time_pov_connection"])
        {
            time_pov = GameData.time_pov_text_true;
        }
        else
        {
            time_pov = GameData.time_pov_text_false;
        }

        string t = reflective_text.text;
        t = t.Replace("$person_mood_connection", person_mood);
        t = t.Replace("$person_pov_connection", person_pov);
        t = t.Replace("$time_mood_connection", time_mood);
        t = t.Replace("$time_pov_connection", time_pov);

        t = t.Replace((string)"$dream_mood", (string)GameData.variables["$dream_mood"]);
        t = t.Replace((string)"$dream_quality", (string)GameData.variables["$dream_quality"]);
        t = t.Replace((string)"$dream_time", (string)GameData.variables["$dream_time"]);
        t = t.Replace((string)"$person", (string)GameData.variables["$person"]);
        t = t.Replace((string)"$perspective", (string)GameData.variables["$perspective"]);

        if (GameData.variables["$perspective"] == "first")
        {
            t = t.Replace("$fondness", "fondness");
            t = t.Replace("$closeness", "closeness");
        }
        if (GameData.variables["$perspective"] == "third")
        {
            t = t.Replace("$fondness", "detachment");
            t = t.Replace("$closeness", "distance");
        }
        if (GameData.variables["$perspective"] == "mixed")
        {
            t = t.Replace("$fondness", "changing levels attachment");
            t = t.Replace("$closeness", "change in relationship");
        }
        reflective_text.text = t;

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
