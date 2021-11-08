using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int current_block = 0;
    public static Vector3 start_position;
    public static Quaternion start_rotation;
    public static Dictionary<string, string> user_inputs = new Dictionary<string, string>() {
        { "NAME_INPUT", "Name" },
        {"TIME_DAY_DROPDOWN", "Afternoon" },
        {"LOCATION_DROPDOWN", "Home" },
        {"PERSON_DROPDOWN", "friend" },
        {"HAPPENING_INPUT", "a thing" },
        {"FEELING_DROPDOWN", "happy" },
        {"ATMOSPHERE_DROPDOWN", "erie" }
    };
    public static bool story_mode = false;
    public static int story_index = 0;
    public static int journal_phase = 0;
    public static bool drawing_ivy = false;
    public static Vector3 last_ivy_pos;
}
