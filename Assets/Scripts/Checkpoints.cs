using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    public GameObject storyPanel;
    public Text storyText;
    public GameObject mountain;
    public int instance_story_part_index;
    private int story_list_index = 0;
    private bool instance_story_mode = false;
    private Dictionary<int, string[]> storyDict = new Dictionary<int, string[]>() {
        {0, new string[] {
            "Hey!",
            "Down here!",
            "My name is Lucy, and this is your dream...",
            "I am the queen of the Sleepy Kingdom, my castle is at the top of this mountain!",
            "Since this is your dream you have some special abilities, like controlling the environment...", }
        },
        {1, new string[] {
            "I am out doing my nightly pollination rounds... but it seems this flower hasn't bloomed yet :(",
            "It's important I visit every flower, if we can get this flower up to the castle I might be able to make it bloom!",
            "But I am too small to carry it on my own, will you help me?",
            "Great, you take the flower, and I'll fly ahead."}
        },
        {2, new string[] {
            "Oh good, you're here!",
            "This dream fog is blocking the path, it's too thick... I don't think we can get through",
            "Maybe there's something you can do to help!",
            "Sometimes trying to understand dreams will clear this stuff up...",
            "Maybe you should try writing in your dream journal and see if that helps..."}
        },
        {3, new string[] {
            "We're getting close, I hope we make it in time.",
            "But there's more fog, this stuff sure is annoying",
            "Welp, let's try the journal again",}
        },
        {4, new string[] {
            "We made it to the castle!",
            "...But something doesn't look right...",
            "I couldn't complete my nightly pollination rounds, and now nightmares have taken over the castle!",
            "I don't know if we have enough time, but if you get the flower to the roof where there's plenty of moonlight...",
            "It should bloom - I can finish pollinating - and maybe that will fix this!"}
        },
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.story_mode && instance_story_mode && gameObject.tag == "storyCheckpoint")
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                story_list_index++;
            }
            playNextText();
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            saveCheckpoint(Col);
            if (gameObject.tag == "storyCheckpoint" && !GameData.story_mode && !instance_story_mode) 
            {
                storyCheckpoint();
            }
            
        }
    }

    void saveCheckpoint(Collider Col) 
    {
        GameData.start_rotation = mountain.transform.rotation;
    }

    void storyCheckpoint() 
    {
        storyPanel.SetActive(true);
        GameData.story_mode = true;
        instance_story_mode = true;
        GameData.journal_phase = instance_story_part_index;
    }

    void playNextText() 
    {
        if (GameData.story_mode) 
        {
            if (story_list_index == storyDict[instance_story_part_index].Length)
            {
                GameData.story_mode = false;
                instance_story_mode = false;
                storyPanel.SetActive(false);
                storyText.text = "";
                story_list_index = 0;
                gameObject.SetActive(false);
            }
            else 
            {
                storyText.text = storyDict[instance_story_part_index][story_list_index];
            }
        }
    }
}
