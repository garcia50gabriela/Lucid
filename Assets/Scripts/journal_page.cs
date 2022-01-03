using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class journal_page : MonoBehaviour
{
    public GameObject journal_background;
    public Text journal_text;
    public string value_name;
    public Text drop_down_input;
    public Text text_input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        journal_text.text = journal_text.text.Replace("<place time>", GameData.user_inputs["LOCATION_DROPDOWN"] + " during the " + GameData.user_inputs["TIME_DAY_DROPDOWN"]);
        journal_text.text = journal_text.text.Replace("<feeling>", GameData.user_inputs["FEELING_DROPDOWN"]);
        journal_text.text = journal_text.text.Replace("<atmosphere>", GameData.user_inputs["ATMOSPHERE_DROPDOWN"]);

    }

    public void submit_and_close()
    {
        save_inputs();
        journal_background.SetActive(false);
        gameObject.SetActive(false);
    }

    void save_inputs() 
    {
        if (drop_down_input != null) 
        {
            GameData.user_inputs[value_name + "_DROPDOWN"] = drop_down_input.text;
            print(GameData.user_inputs[value_name + "_DROPDOWN"]);
        }
        if (text_input != null)
        {
            GameData.user_inputs[value_name + "_INPUT"] = text_input.text;
            print(GameData.user_inputs[value_name + "_INPUT"]);
        }
    }
}
