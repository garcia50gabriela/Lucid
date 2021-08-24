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
            "Hello",
            "This is a story",
            "This is some more story",
            "This is the end" }
        },
        {1, new string[] {
            "Hello again",
            "This is another story",
            "This is some more story",
            "This is the end" }
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
                print(story_list_index);
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
