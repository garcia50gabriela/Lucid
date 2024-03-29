﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogOption
{
    public int id;
    public String prompt;
    public String option1;
    public String option2;
    public DialogOption(int i, String p, String op1, String op2)
    {
        id = i;
        prompt = p;
        option1 = op1;
        option2 = op2;
    }
}

public class Checkpoints : MonoBehaviour
{
    public GameObject storyPanel;
    public Text storyText;
    public GameObject mountain;
    public int instance_story_part_index;
    public GameObject journal_background;
    public GameObject journal_portion;
    public GameObject lucy_avatar;

    private int story_list_index = 0;
    private bool instance_story_mode = false;

    // [L] = lucy; [p] = player;
    // [A] = angry; [S] = sad; [H] = happy; [W] = worried;
    private Dictionary<int, string[]> storyDict = new Dictionary<int, string[]>() {
        {0, new string[] {
            "[L][A] Hey! HEY!",
            "[L][A] Get OFF the flower bed!",
            "[L][S] Noo, I think you killed them all!",
            "[P] I'm sorry I didn't...",
            "[L][S] No you don't understand!! These aren't just any flowers! They..",
            "[P] Oh! It looks like this one might be okay!",
            "[L][W] I really hope so.. Okay, grab it and follow me...", }
        },
        {1, new string[] {
            "[P] So am I just going to keep following you all night?",
            "[L][H] We're going to the top of the mountain, that's where my castle is. And there's only one path up.",
            "[P] ..castle?",
            "[L][H] Yup, I'm Lucy, the queen of sleepy kingdom.",
            "[P] But aren't you a moth?",
            "[L][A] And what about it?!?",
            "[P] Oh nothing... uh so what do you do as the queen of sleepy kingdom? That must be fun",
            "[L][H] It's hard work! I keep this place running, fixing things, keeping our guests happy, tending to the Moon Flowers - like the one you have",
            "[P] Right.."}
        },
        {2, new string[] {
            "[P] You must really like these flowers, I'm sorry I crushed them.",
            "[L][S] It's not that, they keep the nightmares away...",
            "[L][S] Ever since I started tending to them the nightmares have stayed away.",
            "[P] Nightmares? I have those sometimes, they make me feel stuck or frozen.",
            "[L][S] I mean The Dreamer's nightmares, and it's my job to keep them away."}
        },
        {3, new string[] {
            "[P] So is this what you were meant to do?",
            "[L][H] I guess so... to be honest, I've been so busy I haven't had time to think about why I'm here.",
            "[P] Maybe I'll figure out why I'm here too.",
            "[L][H] You're probably just a memory of someone The Dreamer knew. Memories float through here a lot... until they're forgotten.",
            "[P] Well that's just great! So I'll be stuck here until I'm forgotten?? What then?",
            "[L][S] I don't know! I'm stuck here too! So we should keep things nice for ourselves while we're here.",
            "[L][S] Things go bad around here when the nightmares come."}
        },
        {4, new string[] {
            "[L][A] I think the nightmares are making their way up the mountain. I think I saw one.",
            "[P] Ugh, I'm already stuck in someone else's dream, the last thing I want is to be stuck in their nightmares.",
            "[L][A] You and me both!",
            "[L][A] Maybe if The Dreamer came around here once and awhile, I could actually get something done around here."}
        },
        {5, new string[] {
            "[P] Hey, have you seen any loose paper along the path?",
            "[L][A] What? We already have nightmares to worry about, now we have to worry about litterers too?!",
            "[P] No, they actually look like my journal pages. I just don't know how they got here.",
            "[L][W] Maybe you dropped them on your way in. You seem pretty clumsy.",
            "[P] I've been thinking, maybe I'm the dreamer? Maybe I left them here as clues for myself.",
            "[L][H] HA I think you would KNOW if you were The Dreamer. C'mon we don't have much time."}
        },
        {6, new string[] {
            "[L][W] This looks bad, the nightmares have made it into the castle.",
            "[P] I know my wake-self is out there, and I know they're listening..",
            "[L][W] What are you talking about?",
            "[P] Hey you! Yeah, YOU!!",
            "[P] Listen, we can do this! I know things seem grim, but this is OUR dream, and I believe in us!",
            "[L][A] Who are you talking to?!?",
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
                //gameObject.SetActive(false);
            }
            if (gameObject.tag == "checkpoint") 
            {
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
                lucy_avatar.GetComponent<lucy_image>().clear_image();
                storyText.alignment = TextAnchor.UpperLeft;
                storyText.color = Color.black;
                lucy_avatar.GetComponent<lucy_image>().show_player();
            }
            else if (t.StartsWith("[L]"))
            {
                lucy_avatar.GetComponent<lucy_image>().clear_image();
                storyText.alignment = TextAnchor.UpperRight;
                storyText.color = Color.magenta;
                show_lucy_image(t);
            }
            t = t.Replace("[P] ", "");
            t = t.Replace("[L]", "");
            t = t.Replace("[W] ", "");
            t = t.Replace("[H] ", "");
            t = t.Replace("[S] ", "");
            t = t.Replace("[A] ", "");
            storyText.text = t;
        }
    }

    void journalCheckpoint()
    {
        journal_background.SetActive(true);
        journal_portion.SetActive(true);
    }

    [Yarn.Unity.YarnCommand("close_journal")]
    public void closeJournal() 
    {
        GameData.journal_index += 1;
        journal_background.SetActive(false);
        journal_portion.SetActive(false);
        gameObject.SetActive(false);
    }

    void show_lucy_image(string tex)
    {
        if (tex.Contains("[H]")) 
        {
            lucy_avatar.GetComponent<lucy_image>().show_happy();
        }
        if (tex.Contains("[A]"))
        {
            lucy_avatar.GetComponent<lucy_image>().show_angry();
        }
        if (tex.Contains("[S]"))
        {
            lucy_avatar.GetComponent<lucy_image>().show_sad();
        }
        if (tex.Contains("[W]"))
        {
            lucy_avatar.GetComponent<lucy_image>().show_worry();
        }

    }

}
