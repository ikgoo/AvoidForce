using Ink;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpControl : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;

    public List<string> ls;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSONAsset.text);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run()
    {
        ls.Clear();

        if (story == null) story = new Story(inkJSONAsset.text);
        
        while(true)
        {
            if (story.currentChoices?.Count > 0)
            {
                ls.Add("");

                story.ChooseChoiceIndex(0);

                getNextStoryBlock();
            }
            else
                break;
        }

    }

    // Load and potentially return the next story block
    string getNextStoryBlock()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
    }
}
