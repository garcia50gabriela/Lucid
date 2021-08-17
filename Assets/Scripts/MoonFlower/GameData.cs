using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int current_block = 0;
    public static Vector3 start_position;
    public static Quaternion start_rotation;
    public static Dictionary<string, string> user_inputs = new Dictionary<string, string>();
}
