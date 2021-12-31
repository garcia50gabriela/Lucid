using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class journal_page : MonoBehaviour
{
    public GameObject journal_background;
    public Text journal_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        journal_text.text = journal_text.text.Replace("<place time>", GameData.user_inputs["LOCATION_DROPDOWN"] + " during the " + GameData.user_inputs["TIME_DAY_DROPDOWN"]);
        journal_text.text = journal_text.text.Replace("<feeling>", GameData.user_inputs["FEELING_DROPDOWN"]);
        journal_text.text = journal_text.text.Replace("<atmosphere>", GameData.user_inputs["ATMOSPHERE_DROPDOWN"]);

    }

    public void submit_and_close()
    {
        journal_background.SetActive(false);
        gameObject.SetActive(false);
    }
}
