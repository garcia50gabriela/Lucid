using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum InputType {
    DROP_DOWN,
    OPEN_TEXT,
    NONE
}

public class JournalPrompt
{
    public String key;
    public String prompt;
    public InputType inputType;
    public string[] inputOptions;
    public JournalPrompt(String k, String p, InputType t, string[] ops)
    {
        key = k;
        prompt = p;
        inputType = t;
        inputOptions = ops;
    }
}

public class Intro_Text : MonoBehaviour
{
    public string journalKey = "INTRO";
    public GameObject journal_background;

    public Text text_1;
    public Text text_2;
    private JournalPrompt currentPrompt;

    public GameObject text_input;
    public InputField text_input2;

    public Dropdown dropdown_input;
    public GameObject dropdown_parent;
    List<Dropdown.OptionData> m_Messages = new List<Dropdown.OptionData>();
    Dropdown.OptionData m_NewData;

    public Text text_input_value;
    public Text dropdown_input_value;

    public Text date_text;
    public Text header_text;

    private int text_index = 0;
    private bool waiting_for_input = false;
    private JournalPrompt[] text_list;
    private JournalPrompt[] introJournalList = new JournalPrompt[] {
        new JournalPrompt("BLANK", "", InputType.NONE, null),
        new JournalPrompt("INTRO1", "Journaling is a great form of self-care. It can help us process and understand our most complex emotions through writing.", InputType.NONE, null),
        new JournalPrompt("INTRO2", "Dreams often seem bizzare, but could reveal feelings we've been ignoring. Let's try writing about a recent dream...", InputType.NONE, null),
        new JournalPrompt("NAME_INPUT", "This journal belongs to...", InputType.OPEN_TEXT, null),
        new JournalPrompt("TIME_DAY_DROPDOWN", "This dream took place in...", InputType.DROP_DOWN, new string[]{"the Morning", "the Afternoon", "the Evening", "the Night", "an insignificant time of day." }),
        new JournalPrompt("LOCATION_DROPDOWN", "It all happened...", InputType.DROP_DOWN, new string[]{"at Home", "at School", "at Work", "Outdoors", "in multiple locations" }),
        new JournalPrompt("PERSON_DROPDOWN", "I was...", InputType.DROP_DOWN, new string[]{ "with a Friend", "with a Family Member", "with a Collegue", "with a Significant Other", "with a Stranger", "alone" }),
        new JournalPrompt("HAPPENING_INPUT", "The event taking place was...", InputType.OPEN_TEXT, null),
        new JournalPrompt("FEELING_DROPDOWN", "I felt...", InputType.DROP_DOWN, new string[]{"Happy", "Sad", "Worried", "Curious", "Excited", "Cautious" }),
        new JournalPrompt("ATMOSPHERE_DROPDOWN", "The atmosphere felt...", InputType.DROP_DOWN, new string[]{"Erie", "Sterile", "Social", "Cozy", "Familiar", "Intimate" }),
        new JournalPrompt("OPEN_INPUT", "Some other things I would like to make note of are...", InputType.OPEN_TEXT, null),
        new JournalPrompt("OUTRO", "Ignoring dreams and avoiding emotions is never as benificial as learning to control them. We should embrace them, understand them, and learn from them.", InputType.NONE, null),
    };
    private JournalPrompt[] Journal1List = new JournalPrompt[] {
        new JournalPrompt("INTRO", "Places in dreams can represent the feelings we have towards those places. If it's a place you've been before, then consider your personal memories, or if you've never been consider what you know about the place.", InputType.NONE, null),
        new JournalPrompt("PLACE_REFLECTION_INPUT", "How is being <place> in <time> significant to me...", InputType.OPEN_TEXT, null),  
    };
    private JournalPrompt[] Journal2List = new JournalPrompt[] {
        new JournalPrompt("INTRO", "People and events in dreams do not necessarily mean you're dreaming of them specifically, but rather they represent aspects of ourselves. A performance could represent how you present yourself to others, or a humble friend could represent our humble side.", InputType.NONE, null),
        new JournalPrompt("PERSON_REFLECTION_INPUT", "What about being <person> or a <event> could symbolize about myself..", InputType.OPEN_TEXT, null),
    };
    private JournalPrompt[] Journal3List = new JournalPrompt[] {
        new JournalPrompt("INTRO", "Emotions in dreams are never disguised and often do not represent anything, but they are more self directed than we think.", InputType.NONE, null),
        new JournalPrompt("FEELING_REFLECTION_INPUT", "Is there anything you've done recently that could make you feel <feeling> or <atmosphere> towards yourself?", InputType.OPEN_TEXT, null),
    };
    // Start is called before the first frame update
    void Start()
    {
        if (date_text != null) 
        {
            date_text.text = System.DateTime.Now.ToShortDateString();
        }
        

        if (journalKey == "INTRO")
        {
            text_list = introJournalList;
        }
        else if (journalKey == "PAGE1") 
        {
            text_list = Journal1List;
        }
        else if (journalKey == "PAGE2")
        {
            text_list = Journal2List;
        }
        else if (journalKey == "PAGE3")
        {
            text_list = Journal3List;
        }

        currentPrompt = text_list[text_index];
        
    }

    private void OnEnable()
    {
        if (journalKey == "INTRO")
        {
            text_list = introJournalList;
        }
        else if (journalKey == "PAGE1")
        {
            text_list = Journal1List;
        }
        else if (journalKey == "PAGE2")
        {
            text_list = Journal2List;
        }
        else if (journalKey == "PAGE3")
        {
            text_list = Journal3List;
        }
        currentPrompt = text_list[text_index];
        text_2.text = currentPrompt.prompt;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !waiting_for_input) 
        {
            play_next_text();
        }
        make_name_header();
    }

    void make_name_header() 
    {
        if (GameData.user_inputs["NAME_INPUT"] != "Name")
        {
            header_text.text = GameData.user_inputs["NAME_INPUT"] + "'s Dream";
        }
    }

    public void submit_input() 
    {
        if (currentPrompt.inputType == InputType.OPEN_TEXT)
        {
            GameData.user_inputs[currentPrompt.key] = text_input_value.text;
            text_input_value.text = string.Empty;
            text_input2.text = string.Empty;
            text_input.SetActive(false);
        }
        if (currentPrompt.inputType == InputType.DROP_DOWN)
        {
            GameData.user_inputs[currentPrompt.key] = dropdown_input_value.text;
            dropdown_parent.SetActive(false);
        }
        if (currentPrompt.inputType != InputType.NONE && currentPrompt.key != "NAME_INPUT")
        {
            text_1.text = text_1.text.Replace("...", " " + GameData.user_inputs[currentPrompt.key]) + ".\n";
        }
        else if(currentPrompt.key == "NAME_INPUT")
        {
            text_1.text = "";
        }
        waiting_for_input = false;
        play_next_text();
    }

    void play_next_text() 
    {
        if (text_index >= text_list.Length - 1)
        {
            if (journalKey == "INTRO") 
            {
                SceneManager.LoadScene("Lucid");
            }
        }
        else 
        {
            text_index += 1;
            currentPrompt = text_list[text_index];
            if (currentPrompt.inputType == InputType.NONE)
            {
                text_2.text = currentPrompt.prompt;
            }
            else
            {
                text_2.text = "";
                StopCoroutine("PlayText");
                StartCoroutine("PlayText");
                show_input();
            }
        }
    }

    IEnumerator PlayText()
    {
        foreach (char c in currentPrompt.prompt)
        {
            text_1.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void show_input()
    {
        if (currentPrompt.inputType != InputType.NONE) 
        {
            waiting_for_input = true;
            if (currentPrompt.inputType == InputType.OPEN_TEXT)
            {
                text_input_value.text = string.Empty;
                text_input.SetActive(true);
            }
            if (currentPrompt.inputType == InputType.DROP_DOWN)
            {
                dropdown_input.ClearOptions();
                foreach (string option in currentPrompt.inputOptions)
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
    public void submit_and_close()
    {
        submit_input();
        journal_background.SetActive(false);
        gameObject.SetActive(false);
        text_2.text = "";
        text_1.text = "";
    }
}
