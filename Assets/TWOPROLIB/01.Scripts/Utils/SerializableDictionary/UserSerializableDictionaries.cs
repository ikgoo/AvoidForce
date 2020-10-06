using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TWOPROLIB.ScriptableObjects;

namespace TWOPRO.Utils
{
    [Serializable]
    public class StringStringDictionary : SerializableDictionary<string, string> { }

    [Serializable]
    public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> { }

    [Serializable]
    public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> { }

    [Serializable]
    public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> { }


    [Serializable]
    public class HelpTopGroupArrayStorage : SerializableDictionary.Storage<HelpTopGroup[]> { }

    [Serializable]
    public class HelpDictionary : SerializableDictionary<Helptype, HelpTopGroup[], HelpTopGroupArrayStorage> { }

    [Serializable]
    public class MultiLangContentDictionary : SerializableDictionary<SystemLanguage, HelpContent> { }

    //public class MultiLangHelpContent : ScriptableObject
    //{
    //    public MultiLangContentDictionary multiLangContentDictionary;
    //}

    [Serializable]
    public class MyClass
    {
        public int i;
        public string str;
    }


    [Serializable]
    public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass> { }

#if NET_4_6 || NET_STANDARD_2_0
    [Serializable]
    public class StringHashSet : SerializableHashSet<string> { }
#endif

    public class HelpTopGroup
    {
        public string title;
        public List<HelpGroup> helpGroup;
    }

    public class HelpGroup
    {
        public string title;
        public string MultiLangContentDictionary;
    }

    [Serializable]
    public class HelpContent
    {
        public string title;
        [Multiline]
        public string content;
    }

    public enum Helptype
    {
        Tutoreal01,
        Tutoreal02

    }
}