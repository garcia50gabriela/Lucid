using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameData
{
    public static int current_block = 0;
    public static Vector3 start_position;
    public static Quaternion start_rotation;
    public static Dictionary<string, string> user_inputs = new Dictionary<string, string>() {
        {"NAME_INPUT", "Name" },
        {"TIME_DAY_DROPDOWN", "Afternoon" },
        {"LOCATION_DROPDOWN", "Home" },
        {"PERSON_DROPDOWN", "friend" },
        {"HAPPENING_INPUT", "a thing" },
        {"FEELING_DROPDOWN", "happy" },
        {"ATMOSPHERE_DROPDOWN", "erie" },
        {"OPEN_INPUT", "I was watching tv but nothing was on." },
        {"PLACE_REFLECTION_INPUT", "is"},
        {"PERSON_REFLECTION_INPUT", "confused" },
        {"FEELING_REFLECTION_INPUT", "means something."}
    };
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
    
    [Yarn.Unity.YarnCommand("load_game")]
    public static void load_game()
    {
        SceneManager.LoadScene("Lucid");
    }
}
