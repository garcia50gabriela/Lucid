using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class journal_page : MonoBehaviour
{
    public GameObject journal_background;
    public Text journal_text;
    private int page_index = -1;
    public List<GameObject> ReviewPages;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void open()
    {
        journal_background.SetActive(true);
        gameObject.SetActive(true);
    }
    public void close()
    {
        ReviewPages[page_index].SetActive(false);
        page_index = -1;
        journal_background.SetActive(false);
        gameObject.SetActive(false);
    }
    public void next()
    {
        if (GameData.journal_index > 0 && page_index < GameData.journal_index - 1) 
        {
            if (page_index >= 0) 
            {
                ReviewPages[page_index].SetActive(false);
            }
            page_index++;
            ReviewPages[page_index].SetActive(true);
        }
    }
    public void previous()
    {
        if (page_index > 0 && page_index < GameData.journal_index)
        {
            if (page_index >= 0)
            {
                ReviewPages[page_index].SetActive(false);
            }
            page_index--;
            ReviewPages[page_index].SetActive(true);
        }
    }
}
