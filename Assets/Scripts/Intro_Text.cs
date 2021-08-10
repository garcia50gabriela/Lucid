using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro_Text : MonoBehaviour
{
    public Text text_1;
    public Text text_2;
    private string text_1_current;
    private string text_2_current;
    public GameObject text_input;
    public Text text_input_value;
    private int text_index = 0;
    private bool waiting_for_input = false;
    private string[] text_list = new string[] {
        "Hello,",
        "This is your dream journal.",
        "What is your name?",
        "NAME_INPUT",
        $"Well, NAME_VALUE You haven't written in me in a while...",
        "...can you tell me about a recent dream you had?",

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
        if (text_2_current == "NAME_INPUT")
        {
            text_2.text = "";
            waiting_for_input = true;
            text_input.SetActive(true);
        }
    }

    public void submit_input() 
    {
        GameData.username = text_input_value.text;
        waiting_for_input = false;
        text_input.SetActive(false);
        play_next_text();
    }

    void play_next_text() 
    {
        text_1.text = "";
        text_2.text = "";
        text_1_current = text_list[text_index];
        text_2_current = text_list[text_index + 1];
        text_1_current = text_1_current.Replace("NAME_VALUE", GameData.username);
        StartCoroutine("PlayText");
        text_index += 2;
    }

    IEnumerator PlayText()
    {
        foreach (char c in text_1_current)
        {
            text_1.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        foreach (char c in text_2_current)
        {
            text_2.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
