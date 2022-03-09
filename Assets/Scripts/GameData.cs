using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameData
{
    public static int current_block = 0;
    public static Vector3 start_position;
    public static Quaternion start_rotation;
    public static Dictionary<string, object> variables = new Dictionary<string, object>()
    {
        {"$dream_mood", "positive" },
        {"$dream_quality", "low" },
        {"$dream_time", "past" },
        {"$person", "friend" },
        {"$perspective", "first" },
        {"$person_mood_connection", true },
        {"$person_pov_connection", true },
        {"$time_mood_connection", true},
        {"$time_pov_connection", true }
    };
    public static bool story_mode = false;
    public static int story_index = 0;
    public static int journal_phase = 0;
    public static bool drawing_ivy = false;
    public static Vector3 last_ivy_pos;
    public static GameObject last_ivy;
    // ivy
    public static bool insideIvy = false;
    public static bool insideIvyTrigger = false;
    public static Vector3 ivyPos;
    public static int overlapIvy = 0;
    public static Transform ivyParent;

    public static int journal_index = 0;
    
    public static string dream_mood_text = "My dream had a $dream_mood tone to it.";
    public static string dream_quality_text = "The quality of sleep I got that night was $dream_quality.";
    public static string dream_quality_high_text = "Which suggests I wasn't thinking of anything that's bothering me before bed.";
    public static string dream_quality_low_text = "Which suggests I was thinking of something that's bothering me before bed.";
    public static string dream_quality_normal_text = "Which suggests there might be something ongoing that's bothering me and keeping me from my highest quality sleep.";
    public static string dream_time_text = "Something about the dream made me think it was happening in the $dream_time.";
    public static string dream_person_text = "I consider the people in my dream to be $person.";
    public static string dream_perspective_text = "I dreamnt in a $perspective-person perspective.";
    public static string person_mood_text_true = "The $dream_mood feelings I have towards the $person from my dream, may be related to the $dream_mood tone of my dream.";
    public static string person_mood_text_false = "I don't have $dream_mood feelings towards the $person from my dream, they may symbolize something else that I do have $dream_mood feelings towards.";
    public static string person_pov_text_true = "The $perspective-person perspective may represent the $closeness I feel towards the $person for my dream or what they represent.";
    public static string person_pov_text_false = "The $perspective-person perspective doesn't seem to represent the $closeness I feel towards the $person for my dream.";
    public static string time_mood_text_true = "The $dream_mood feelings I have towards the $dream_time from my dream, may be related to the $dream_mood tone of my dream.";
    public static string time_mood_text_false = "The $dream_mood feelings I have towards the $dream_time events from my dream, is different than the $dream_mood tone of my dream.";
    public static string time_pov_text_true = "The $perspective-person perspective suggests $fondness, sush as the $fondness I feel towards the $dream_time events for my dream.";
    public static string time_pov_text_false = "The $perspective-person perspective suggests a different level of $fondness than I feel towards the $dream_time events for my dream.";


    [Yarn.Unity.YarnCommand("load_game")]
    public static void load_game()
    {
        SceneManager.LoadScene("Lucid");
    }
}
