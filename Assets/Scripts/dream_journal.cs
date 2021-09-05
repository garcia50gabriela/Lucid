using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dream_journal : MonoBehaviour
{
    public Text journalText;
    public GameObject Phase2Parent;
    public Text input_phase_2;
    public Dropdown input_field_2;
    public Text input_field_2_text;
    public GameObject Phase3Parent;
    public Text input_phase_3;
    public Dropdown input_field_3;
    public Text input_field_3_text;
    private string[] text_list = new string[] {
        "Now that I'm not dreaming, that place and time makes me feel...",
        "PLACE_FEELING_DROPDOWN",
        "In real life doing that thing with that person is something I...",
        "THING_FEELING_DROPDOWN", 
    };
    private Dictionary<string, string[]> dropdown_options = new Dictionary<string, string[]>()
    {
        {"PLACE_FEELING_DROPDOWN", new string[] {"Nostalgic", "Wary", "Frightened", "Excited" } },
        {"THING_FEELING_DROPDOWN", new string[] {"would never do.", "would like to do.", "would feel indifferent about doing.", "have already done." } },
    };
    // Start is called before the first frame update
    void Start()
    {
        journalText.text = (GameData.user_inputs["NAME_INPUT"] + "'s Dream\n" +
            "I had a dream I was at " + GameData.user_inputs["LOCATION_DROPDOWN"] +
            " during the " + GameData.user_inputs["TIME_DAY_DROPDOWN"] + ".\n" +
            "A " + GameData.user_inputs["HAPPENING_INPUT"] + " was going on, and I was with a " + GameData.user_inputs["PERSON_DROPDOWN"] + ".\n" +
            "The atmosphere was " + GameData.user_inputs["ATMOSPHERE_DROPDOWN"] + " and I felt " + GameData.user_inputs["FEELING_DROPDOWN"] + ".");
        input_phase_2.text = "Now that I'm not dreaming, that place and time makes me feel...";
        input_phase_2.text = "In real life doing that thing with that person is something I...";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.journal_phase == 2) 
        {
            Phase2Parent.SetActive(true);
        }
        if (GameData.journal_phase == 3)
        {
            Phase3Parent.SetActive(true);
        }

    }

    public void open_journal() 
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void save_phase_2() 
    {
        GameData.user_inputs.Add("PLACE_FEELING_DROPDOWN", input_field_2_text.text);
        Phase2Parent.SetActive(false);
        input_phase_2.text = "When I'm not dreaming that place and time makes me feel " + input_field_2_text.text;
    }

    public void save_phase_3()
    {
        GameData.user_inputs.Add("THING_FEELING_DROPDOWN", input_field_3_text.text);
        Phase3Parent.SetActive(false);
        input_phase_3.text = "Doing that thing with that person is something I " + input_field_3_text.text;
    }
}
