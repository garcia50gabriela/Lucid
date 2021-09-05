using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro_Text : MonoBehaviour
{
    public Text text_1;
    public Text text_2;
    private string text_1_current = "";
    private string text_2_current = "";
    public GameObject text_input;
    public InputField text_input2;
    private string current_input;
    public Dropdown dropdown_input;
    public GameObject dropdown_parent;
    List<Dropdown.OptionData> m_Messages = new List<Dropdown.OptionData>();
    Dropdown.OptionData m_NewData;
    public Text text_input_value;
    public Text dropdown_input_value;
    private int text_index = 0;
    private bool waiting_for_input = false;
    private string[] text_list = new string[] {
        "Hello,",
        "This is your dream journal.",
        "What is your name?",
        "NAME_INPUT",
        $"Well, NAME_VALUE You haven't written in me in a while...",
        "...can you write about a recent dream you had?",
        "My dream took place in the...",
        "TIME_DAY_DROPDOWN",
        "I was at...",
        "LOCATION_DROPDOWN",
        "I was with...",
        "PERSON_DROPDOWN",
        "The event taking place was...",
        "HAPPENING_INPUT",
        "I felt...",
        "FEELING_DROPDOWN",
        "The atmosphere felt...",
        "ATMOSPHERE_DROPDOWN",
    };
    private Dictionary<string, string[]> dropdown_options = new Dictionary<string, string[]>()
    {
        {"TIME_DAY_DROPDOWN", new string[] {"Morning", "Afternoon", "Evening", "Midnight" } },
        {"LOCATION_DROPDOWN", new string[] {"Home", "School", "Work", "Outdoors" } },
        {"PERSON_DROPDOWN", new string[] {"Friend", "Family Member", "Collegue", "Significant Other", "Stranger" } },
        {"FEELING_DROPDOWN", new string[] {"Happy", "Sad", "Worried", "Curious", "Excited", "Cautious" } },
        {"ATMOSPHERE_DROPDOWN", new string[] {"Erie", "Sterile", "Social", "Cozy", "Familiar", "Intimate" } },
    };
    // Start is called before the first frame update
    void Start()
    {
        text_1.text = "";
        text_2.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !waiting_for_input) 
        {
            play_next_text();
        }
        if (!waiting_for_input && (text_2_current.Contains("_INPUT") || text_2_current.Contains("_DROPDOWN"))) 
        {
            apply_input(text_2_current);
        }
        
    }

    public void submit_input() 
    {
        if (text_2_current.Contains("_INPUT"))
        {
            GameData.user_inputs.Add(current_input, text_input_value.text);
            text_input_value.text = string.Empty;
            text_input2.text = string.Empty;
            text_input.SetActive(false);
        }
        if (text_2_current.Contains("_DROPDOWN"))
        {
            GameData.user_inputs.Add(current_input, dropdown_input_value.text);
            dropdown_parent.SetActive(false);
        }
        waiting_for_input = false;
        play_next_text();
    }

    void play_next_text() 
    {
        if (text_index >= text_list.Length)
        {
            SceneManager.LoadScene("Lucid");
        }
        else 
        {
            StopCoroutine("PlayText");
            text_1.text = "";
            text_2.text = "";
            text_1_current = "";
            text_2_current = "";
            text_1_current = text_list[text_index];
            text_2_current = text_list[text_index + 1];
            if (text_1_current.Contains("_VALUE"))
            {
                text_1_current = text_1_current.Replace("NAME_VALUE", GameData.user_inputs["NAME_INPUT"]);
            }

            StartCoroutine("PlayText");
            text_index += 2;
        }
    }

    IEnumerator PlayText()
    {
        foreach (char c in text_1_current)
        {
            text_1.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        if (!text_2_current.Contains("_INPUT") && !text_2_current.Contains("_DROPDOWN")) 
        {
            foreach (char c in text_2_current)
            {
                text_2.text += c;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void apply_input(string text) 
    {
        current_input = text;
        text_2.text = "";
        waiting_for_input = true;
        if (text.Contains("_INPUT"))
        {
            text_input_value.text = string.Empty;
            text_input.SetActive(true);
        }
        if (text.Contains("_DROPDOWN"))
        {
            dropdown_input.ClearOptions();
            foreach (string option in dropdown_options[text])
            {
                m_NewData = new Dropdown.OptionData();
                m_NewData.text = option;
                m_Messages.Add(m_NewData);
            }
            dropdown_input.options = m_Messages;
            dropdown_parent.SetActive(true);
        }
    }
}
