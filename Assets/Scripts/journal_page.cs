using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class journal_page : MonoBehaviour
{
    public GameObject journal_background;
    public Text journal_text;
    private int page_index = -1;
    public List<GameObject> ReviewPages;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void open()
    {
        journal_background.SetActive(true);
        gameObject.SetActive(true);
    }
    public void close()
    {
        if (page_index >= 0) 
        {
            ReviewPages[page_index].SetActive(false);
        }
        
        page_index = -1;
        journal_background.SetActive(false);
        gameObject.SetActive(false);
    }
    public void next()
    {
        if (GameData.journal_index > 0 && page_index < GameData.journal_index - 1) 
        {
            if (page_index >= 0) 
            {
                ReviewPages[page_index].SetActive(false);
            }
            page_index++;
            replace_variables_in_text(ReviewPages[page_index]);
            ReviewPages[page_index].SetActive(true);
        }
    }
    public void previous()
    {
        if (page_index > 0 && page_index < GameData.journal_index)
        {
            if (page_index >= 0)
            {
                ReviewPages[page_index].SetActive(false);
            }
            page_index--;
            replace_variables_in_text(ReviewPages[page_index]);
            ReviewPages[page_index].SetActive(true);
        }
    }

    void replace_variables_in_text(GameObject gameobject) 
    {
        
        if (gameobject.GetComponent(typeof(Text)) != null) 
        {
            string t = gameobject.GetComponent<Text>().text;
            t = t.Replace("dream_mood_text", GameData.dream_mood_text);
            t = t.Replace("dream_quality_text", GameData.dream_quality_text);
            t = t.Replace("dream_time_text", GameData.dream_time_text);
            t = t.Replace("dream_person_text", GameData.dream_person_text);
            t = t.Replace("dream_perspective_text", GameData.dream_perspective_text);
            t = t.Replace("dream_perspective_text", GameData.dream_perspective_text);

            if (GameData.variables["$dream_quality"] == "low") 
            {
                t = t.Replace("dream_quality_text", GameData.dream_quality_low_text);
            }
            if (GameData.variables["$dream_quality"] == "high")
            {
                t = t.Replace("dream_quality_text", GameData.dream_quality_high_text);
            }
            if (GameData.variables["$dream_quality"] == "normal")
            {
                t = t.Replace("dream_quality_text", GameData.dream_quality_normal_text);
            }

            if ((bool)GameData.variables["$person_mood_connection"])
            {
                t = t.Replace("person_mood_text", GameData.person_mood_text_true);
            }
            if (!(bool)GameData.variables["$person_mood_connection"])
            {
                t = t.Replace("person_mood_text", GameData.person_mood_text_false);
            }

            if ((bool)GameData.variables["$person_pov_connection"])
            {
                t = t.Replace("person_pov_text", GameData.person_pov_text_true);
            }
            if (!(bool)GameData.variables["$person_pov_connection"])
            {
                t = t.Replace("person_pov_text", GameData.person_pov_text_false);
            }

            if ((bool)GameData.variables["$time_mood_connection"])
            {
                t = t.Replace("time_mood_text", GameData.time_mood_text_true);
            }
            if (!(bool)GameData.variables["$time_mood_connection"])
            {
                t = t.Replace("time_mood_text", GameData.time_mood_text_false);
            }

            if ((bool)GameData.variables["$time_pov_connection"])
            {
                t = t.Replace("time_pov_text", GameData.time_pov_text_true);
            }
            if (!(bool)GameData.variables["$time_pov_connection"])
            {
                t = t.Replace("time_pov_text", GameData.time_pov_text_false);
            }
            
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


            gameobject.GetComponent<Text>().text = t;

        }
    }
}
