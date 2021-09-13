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
            "[L] Noo, I think you killed all of them all!",
            "[P] I'm sorry I didn't...",
            "[L] No you don't understand! These aren't just any flowers! They..",
            "[P] Oh! It looks like this one might be okay!",
            "[L] I really hope so, for all our sakes... Okay, grab it and follow me...", }
        },
        {1, new string[] {
            "[P] Where are we going?",
            "[L] To the top of the mountain, that's where my castle is.",
            "[P] ..castle?",
            "[L] Yup, I'm Lucy, the queen of sleepy kingdom. Sorry, this isn't how we usually welcome guests",
            "[P] But aren't you a moth?",
            "[L] And what about it?!?",
            "[P] Oh nothing... uh so what do you do as the queen of sleepy kingdom?",
            "[L] I keep this place running, fixing things, keeping our guests happy, tending to the Moon Flowers - like the one you have",
            "[P] Right.."}
        },
        {2, new string[] {
            "[P] Why are we stopping?",
            "[L] I can fly ahead, but I'm not sure how you'll get over this cliff...",
            "[P] Is there another path?",
            "[L] No, there's only one path up the mountain... It's better that way..",
            "[L] If the tide was higher, you might be able to jump onto that lilly pad to get up there",
            "[L] Anyway, I'll fly ahead and let you figure it out.",
            "[P] Do you want to take the flower incase I don't make it?",
            "[L] Are you kidding me!? That flower is five times my size, I can't carry it. You're going to have to do it."}
        },
        {3, new string[] {
            "[P] Woah, did.. did the moon change?",
            "[P] ...Did I do that somehow?",
            "[P] No time to think about it now.. I'm just glad it worked." }
        },
        {4, new string[] {
            "[P] What's that?",
            "[P] That's weird it looks like one of my dream journal pages",
            "[P] I think there's some unfinished writing on it, I don't remember doing that.",
            "[P] I guess I'll go ahead and finish it?"}
        },
        {5, new string[] {
            "[L] Finally, I feel like I've been waiting FOREVER.",
            "[L] I just wanted to stick around to tell you that you can climb this ivy. A lot of the plants here are very old and sturdy.",
            "[P] Thanks, so why do we need to save this flower anyway?",
            "[L] When they bloom, they keep the nightmares away... I'm not sure how exactly..",
            "[L] But I've been trying to grow more ever since I figured it out..",
            "[L] I was in the middle of pollinating them when you showed up and crushed them.",
            "[P] Sorry about that, I'm not sure how that happened.. One moment I was in my room and the next...",
            "[L] Okay, this isn't story time, let's keep moving!"
            }
        },
        {6, new string[] {
            "[P] Oh, there's more of that Ivy",
            "[P] If only it was a little longer, then I could use it to climb up"}
        },
        {7, new string[] {
            "[P] Weird, I don't know why I can do these things...",}
        },
        {8, new string[] {
            "[P] Another journal page! how did they get here?",}
        },
        {9, new string[] {
            "[L] Oh good, you're here, I didn't think you'd make it!",
            "[L] Hey, how have you been getting past all these obstacles anyway? I tried to make this path impossible to get through",
            "[P] I'm not sure, I guess I can just control things when I need them.",
            "[L] That is strange... In that case, I'm glad you're here. I've been protecting the castle by myself for so long, it's nice to have some help.",
            "[P] Of course, It's the least I can do. I'm still trying to figure out why I'm here.",}
        },
        {10, new string[] {
            "[P] So I can control my dreams, they don't have to mean anything if I dont want them too. hmm..",}
        },
        {11, new string[] {
            "[P] This block kind of looks like the one on the other side of the mountain, If only this one was pushed out like that one was",}
        },
        {12, new string[] {
            "[L] This doesn't look good, the nightmares have already made it up the mountain.",
            "[P] What happens when the nightmares reach the castle?",
            "[L] Everything get's dark, and things go bad... It takes a long time and a lot of work to make them go away.",
            "[L] Not to mention all the repairs the castle will need afterwards.",
            "[P] You should fly ahead to the castle and make sure it's alright, me and the flower will catch up.",
            "[L] Good idea I'll see you there!"}
        },
        {13, new string[] {
            "[P] whew, We made it now what?",
            "[L] This is bad.. really bad.. the castle is already crumbling",
            "[P] NO, it's going to be okay",
            "[L] What do you mean?",
            "[P] This is my dream! I can control it! And I'm not going to let it turn into a nightmare!",
            "[L] You're The Dreamer? It's so great to finally meet you!",
            "[P] What can I do to make this flower bloom?",
            "[L] Right, it needs plenty of moon light. More than usual, considering the state it's in. Maybe if you take it to the roof..",
            "[P] I'm on my way!!"}
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
            // save position for any checkpoint
            saveCheckpoint(Col);
            // story dialog box if story
            if (gameObject.tag == "storyCheckpoint" && !GameData.story_mode && !instance_story_mode) 
            {
                storyCheckpoint();
            }
            // last story checkpoint goes to next scene
            if (instance_story_part_index == 14) 
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

    void journalCheckpoint() 
    {
        journal_background.SetActive(true);
        journal_portion.SetActive(true);
    }
}
