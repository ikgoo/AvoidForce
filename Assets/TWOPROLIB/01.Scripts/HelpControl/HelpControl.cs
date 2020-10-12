using Ink;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TWOPRO.Utils;
using TWOPROLIB.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class HelpControl : MonoBehaviour
{
    private Story story;

    public List<HelpItem> lsHelp;

    public StringGameObjectDictionary lsHelpObject;

    public MultiLangContentDictionary3 multiLangHelpContent;

    void Start()
    {

    }

    void Update()
    {
        
    }

    /// <summary>
    /// 도움말 관련 리스트 생성
    /// </summary>
    public void RunGenerateList()
    {
        Debug.Log("start");

        if(multiLangHelpContent.ContainsKey(Application.systemLanguage))
        {
            story = new Story(multiLangHelpContent[Application.systemLanguage].text);
        }
        else
        {
            // 기본은 영문으로 표현
            story = new Story(multiLangHelpContent[SystemLanguage.English].text);
        }

        int currentCNT = 0;
        int cnt = 0;
        string tmp_head = "";
        string helpObjName = "";
        float beforePlay = 0f;
        float afterPlay = 0f;
        while (true)
        {
            if (story.currentChoices.Count > 0)
            {
                helpObjName = story.variablesState["HelpObjName"].ToString();
                beforePlay = float.Parse(story.variablesState["BeforePlay"].ToString());
                afterPlay = float.Parse(story.variablesState["AfterPlay"].ToString());
                if (lsHelp.Count >= currentCNT + 1)
                {
                    lsHelp[currentCNT].UpdateHelp(helpObjName, beforePlay, afterPlay);
                }
                else
                {
                    tmp_head = story.state.previousPointer.path.head.name;
                    lsHelp.Add(new HelpItem(tmp_head + "-" + cnt.ToString(), helpObjName, beforePlay, afterPlay) );
                }

                story.ChooseChoiceIndex(0);

                getNextStoryBlock();

                try
                {
                    if (story.state.previousPointer.path != null && tmp_head.Equals(story.state.previousPointer.path.head.name))
                        cnt++;
                    else
                        cnt = 0;
                }
                catch(Exception ex)
                {
                    int iii = 0;
                }

                currentCNT++;
            }
            else
                break;
        }
        Debug.Log("end");
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

    [Serializable]
    public class HelpItem
    {
        [SerializeField]
        private string title;

        public String HelpObj;

        public float BeforeTimeScale;
        public List<GameObject> BeforeView;
        public List<GameObject> BeforeHide;

        public float AfterTimeScale;
        public List<GameObject> AfterView;
        public List<GameObject> AfterHide;


        public HelpItem(string title, string HelpObj, float beforeTimeScale, float afterTimeScale)
        {
            this.title = title;
            this.BeforeTimeScale = beforeTimeScale;
            this.AfterTimeScale = afterTimeScale;
        }

        public void UpdateHelp(string HelpObj, float beforeTimeScale, float afterTimeScale)
        {
            this.BeforeTimeScale = beforeTimeScale;
            this.AfterTimeScale = afterTimeScale;
        }
    }


}
