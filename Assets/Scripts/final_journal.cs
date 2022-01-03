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
        header.text = header.text.Replace("<name>", GameData.user_inputs["NAME_INPUT"]);

        dream_text.text = dream_text.text.Replace("<time_day>", GameData.user_inputs["TIME_DAY_DROPDOWN"]);
        dream_text.text = dream_text.text.Replace("<place>", GameData.user_inputs["LOCATION_DROPDOWN"]);
        dream_text.text = dream_text.text.Replace("<person>", GameData.user_inputs["PERSON_DROPDOWN"]);
        dream_text.text = dream_text.text.Replace("<event>", GameData.user_inputs["HAPPENING_INPUT"]);
        dream_text.text = dream_text.text.Replace("<feeling>", GameData.user_inputs["FEELING_DROPDOWN"]);
        dream_text.text = dream_text.text.Replace("<atmosphere>", GameData.user_inputs["ATMOSPHERE_DROPDOWN"]);
        dream_text.text = dream_text.text.Replace("<open_input>", GameData.user_inputs["OPEN_INPUT"]);

        reflective_text.text = reflective_text.text.Replace("<place>", GameData.user_inputs["LOCATION_DROPDOWN"]);
        reflective_text.text = reflective_text.text.Replace("<time>", GameData.user_inputs["TIME_DAY_DROPDOWN"]);
        reflective_text.text = reflective_text.text.Replace("<feeling>", GameData.user_inputs["FEELING_DROPDOWN"]);
        reflective_text.text = reflective_text.text.Replace("<atmosphere>", GameData.user_inputs["ATMOSPHERE_DROPDOWN"]);
        reflective_text.text = reflective_text.text.Replace("<feel now>", GameData.user_inputs["FEEL_NOW_DROPDOWN"]);
        reflective_text.text = reflective_text.text.Replace("<meaning>", GameData.user_inputs["MEANING_DROPDOWN"]);
        reflective_text.text = reflective_text.text.Replace("<meaning_open>", GameData.user_inputs["MEANING_INPUT"]);

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
