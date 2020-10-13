using Ink;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TWOPRO.Utils;
using TWOPROLIB.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class HelpControl : MonoBehaviour
{
    private Story story;

    /// <summary>
    /// Help때 문구가 보여일 GameObject List
    /// </summary>
    public StringGameObjectDictionary lsHelpObject;

    /// <summary>
    /// Ink로 등록 된 Help(언어별 리스트)
    /// </summary>
    public MultiLangContentDictionary3 multiLangHelpContent;

    public int currentHelpIndex = 0;

    /// <summary>
    /// Ink에서 받은 정보 리스트
    /// </summary>
    public List<HelpItem> lsHelp;


    void Start()
    {
        // Help Object Hide 처리
        foreach (var obj in lsHelpObject)
        {
            obj.Value.SetActive(false);
        }

    }

    /// <summary>
    /// 도움말 관련 리스트 생성
    /// </summary>
    public void RunGenerateList()
    {
        Debug.Log("start");

        InitHelp();

        int cnt = 0;
        string tmp_head = "";
        while (true)
        {
            if (story.currentChoices.Count > 0)
            {
                if (lsHelp.Count < currentHelpIndex + 1)
                {
                    tmp_head = story.state.previousPointer.path.head.name;
                    lsHelp.Add(new HelpItem(tmp_head + "-" + cnt.ToString()) );
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

            }
            else
                break;
        }
        Debug.Log("end");
    }

    /// <summary>
    /// Help 시작
    /// </summary>
    /// <param name="b"></param>
    public void StartHelp(bool b = false)
    {
        InitHelp();

        // Help Object Hide 처리
        foreach (var obj in lsHelpObject)
        {
            obj.Value.SetActive(false);
        }

        if (b)
            BeforeView();
    }

    string CurrentHelpObjName = "";
    float CurrentBeforePlay = 0f;
    float CurrentAfterPlay = 0f;

    TextMeshProUGUI title;
    TextMeshProUGUI content;

    public void BeforeView()
    {
        CurrentHelpObjName = story.variablesState["HelpObjName"].ToString();
        CurrentBeforePlay = float.Parse(story.variablesState["BeforePlay"].ToString());
        CurrentAfterPlay = float.Parse(story.variablesState["AfterPlay"].ToString());

        lsHelpObject[CurrentHelpObjName].SetActive(true);

        TextMeshProUGUI[] tmp_ls = lsHelpObject[CurrentHelpObjName].GetComponentsInChildren<TextMeshProUGUI>();
        if (tmp_ls.Count() == 2)
        {
            title = null;
            content = tmp_ls[0];
        }
        else if (tmp_ls.Count() == 3)
        {
            title = tmp_ls[0];
            content = tmp_ls[1];
        }

        content.text = story.currentText;
        if (title && story.currentTags.Count > 0)
            title.text = story.currentTags[0];

        Time.timeScale = CurrentBeforePlay;
        // View Object
        ProcViewHide(lsHelp[currentHelpIndex].BeforeView, true);
        // Hide Object
        ProcViewHide(lsHelp[currentHelpIndex].BeforeHide, false);

        if (story.currentChoices[0].text.Equals("AUTO"))
        {
            AfterView();
        }
    }

    public void AfterView()
    {
        Time.timeScale = CurrentAfterPlay;
        // View Object
        ProcViewHide(lsHelp[currentHelpIndex].AfterView, true);
        // Hide Object
        ProcViewHide(lsHelp[currentHelpIndex].AfterHide, false);
    }

    public void InitHelp()
    {
        currentHelpIndex = -1;

        if (multiLangHelpContent.ContainsKey(Application.systemLanguage))
        {
            story = new Story(multiLangHelpContent[Application.systemLanguage].text);
        }
        else
        {
            // 기본은 영문으로 표현
            story = new Story(multiLangHelpContent[SystemLanguage.English].text);
        }

        getNextStoryBlock();

    }

    public void NextHelp(int choice = 0)
    {
        if (story.currentChoices[0].text.Equals("NEXT"))
        {
            AfterView();
        }

        lsHelpObject[CurrentHelpObjName].SetActive(false);

        story.ChooseChoiceIndex(choice);

        string t = getNextStoryBlock();

        // 다음이 없으면 종료 처리
        if (t.Equals(""))
        {
            Time.timeScale = 1;
            return;
        }

        BeforeView();
    }

    public void ProcViewHide(List<GameObject> obj, bool v)
    {
        for(int i =  0; i < obj.Count; i++)
        {
            obj[i].SetActive(v);
        }
    }

    // Load and potentially return the next story block
    string getNextStoryBlock()
    {
        currentHelpIndex++;

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

        /// <summary>
        /// Help 시 보여질 TextBox Name
        /// </summary>
        //public String HelpObj;

        //public float BeforeTimeScale;
        public List<GameObject> BeforeView;
        public List<GameObject> BeforeHide;

        //public float AfterTimeScale;
        public List<GameObject> AfterView;
        public List<GameObject> AfterHide;


        public HelpItem(string title)
        {
            this.title = title;
        }
    }


}
