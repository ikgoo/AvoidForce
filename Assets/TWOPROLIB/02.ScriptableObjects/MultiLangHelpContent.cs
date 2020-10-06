using TWOPRO.Utils;
using TWOPROLIB.Scripts.Managers;
using UnityEngine;

namespace TWOPROLIB.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Content/Help")]
    public class MultiLangHelpContent : ScriptableObject
    {
        public MultiLangContentDictionary multiLangContentDictionary;

        public string GetTitle()
        {
            if (multiLangContentDictionary.ContainsKey((SystemLanguage)GameManager.Instance.systemLanguage.RuntimeValue) )
            {
                return ((HelpContent)multiLangContentDictionary[(SystemLanguage)GameManager.Instance.systemLanguage.RuntimeValue]).title;
            }
            else
            {
                return ((HelpContent)multiLangContentDictionary[SystemLanguage.English]).title;
            }
        }

        public string GetContent()
        {
            if (multiLangContentDictionary.ContainsKey((SystemLanguage)GameManager.Instance.systemLanguage.RuntimeValue))
            {
                return ((HelpContent)multiLangContentDictionary[(SystemLanguage)GameManager.Instance.systemLanguage.RuntimeValue]).content;
            }
            else
            {
                return ((HelpContent)multiLangContentDictionary[SystemLanguage.English]).content;
            }
        }
    }
}
