using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    public GameObject storyPanel;
    public Text storyText;
    public GameObject mountain;
    public int instance_story_part_index;
    public GameObject journal_background;
    public GameObject journal_portion;
    private int story_list_index = 0;
    private bool instance_story_mode = false;
    private Dictionary<int, string[]> storyDict = new Dictionary<int, string[]>() {
        {0, new string[] {
            "[L] Hey! HEY!",
            "[L] Get OFF the flower bed!",
            "[L] Noo, I think you killed them all!",
            "[P] I'm sorry I didn't...",
            "[L] No you don't understand!! These aren't just any flowers! They..",
            "[P] Oh! It looks like this one might be okay!",
            "[L] I really hope so.. Okay, grab it and follow me...", }
        },
        {1, new string[] {
            "[P] So am I just going to keep following you all night?",
            "[L] We're going to the top of the mountain, that's where my castle is. And there's only one path up.",
            "[P] ..castle?",
            "[L] Yup, I'm Lucy, the queen of sleepy kingdom.",
            "[P] But aren't you a moth?",
            "[L] And what about it?!?",
            "[P] Oh nothing... uh so what do you do as the queen of sleepy kingdom? That must be fun",
            "[L] It's hard work! I keep this place running, fixing things, keeping our guests happy, tending to the Moon Flowers - like the one you have",
            "[P] Right.."}
        },
        {2, new string[] {
            "[P] You must really like these flowers, I'm sorry I crushed them.",
            "[L] It's not that, they keep the nightmares away...",
            "[L] Ever since I started tending to them the nightmares have stayed away.",
            "[P] Nightmares? I have those sometimes, it feels like having your dreams taken from you.",
            "[L] I don't have them. The Dreamer has them, and it's my job to keep them away."}
        },
        {3, new string[] {
            "[P] This Dreamer person must be pretty special to have someone taking care of their dreams like this.",
            "[L] I guess so, I've never met them, per say. I just showed up here one day and figured out what to do after awhile.",
            "[P] Sounds like you're meant to be here. Maybe I'll figure out why I'm here too.",
            "[L] You're probably just a memory of someone The Dreamer knew. Memories float through here a lot... until they're forgotten.",
            "[P] Well that's just great! So I'll be stuck here until I'm forgotten?? What then?",
            "[L] I don't know! I'm stuck here too! I figured I might as well make it enjoyable for myself by keeping the place nice.",
            "[L] Things go bad around here when the nightmares come."}
        },
        {4, new string[] {
            "[L] I think the nightmares are making their way up the mountain.",
            "[P] Ugh, I'm already stuck in someone else's dream, the last thing I want is to be stuck in their nightmares.",
            "[L] You and me both!",
            "[L] Maybe if The Dreamer came around here once and awhile, I could actually get something done around here."}
        },
        {5, new string[] {
            "[P] Hey, have you seen any loose paper along the path?",
            "[L] What? We already have nightmares to worry about, now we have to worry about litterers too?!",
            "[P] No, they actually look like my journal pages. I just don't know how they got here.",
            "[L] Maybe you dropped them on your way in. You seem pretty clumsy.",
            "[P] I've been thinking, maybe I'm the dreamer? Maybe I left them here as clues for myself.",
            "[L] HA I think you would KNOW if you were The Dreamer. C'mon we don't have much time."}
        },
        {6, new string[] {
            "[L] This looks bad, the nightmares have made it to the castle.",
            "[P] I know my wake-self is out there, and I know they're listening..",
            "[L] What are you talking about?",
            "[P] Hey you! Yeah, YOU!!",
            "[P] Listen, we can do this! I know things seem grim, but this is OUR dream, and I believe in us!",
            "[L] Who are you talking to?!?",
            "[P] You've helped us make it this far! We have a flower, and a dream to save. Let's go!"}
        },
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instance_story_mode && gameObject.tag == "storyCheckpoint")
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) 
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
            // save position for any checkpoint
            saveCheckpoint(Col);
            // story dialog box if story
            if (gameObject.tag == "storyCheckpoint" && !GameData.story_mode && !instance_story_mode) 
            {
                storyCheckpoint();
            }
            // last story checkpoint goes to next scene
            if (instance_story_part_index == 7) 
            {
                SceneManager.LoadScene("tower");
            }
            // if journal checkpoint start journal
            if (gameObject.tag == "journal") 
            {
                journalCheckpoint();
                gameObject.SetActive(false);
            }
            
        }
    }

    void saveCheckpoint(Collider Col) 
    {
        GameData.start_position = Col.transform.position;
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
            var t = storyDict[instance_story_part_index][story_list_index];
            if (t.StartsWith("[P]"))
            {
                storyText.alignment = TextAnchor.UpperLeft;
                storyText.color = Color.black;
            }
            else if (t.StartsWith("[L]"))
            {
                storyText.alignment = TextAnchor.UpperRight;
                storyText.color = Color.magenta;
            }
            t = t.Replace("[P] ", "");
            t = t.Replace("[L] ", "");
            storyText.text = t;
        }
    }

    void journalCheckpoint() 
    {
        journal_background.SetActive(true);
        journal_portion.SetActive(true);
    }
}
