using System.Collections;
using System.Collections.Generic;
using TWOPRO.Utils;
using UnityEngine;

public class SerializableDictionaryExample : MonoBehaviour
{
	// The dictionaries can be accessed throught a property
	[SerializeField]
	StringStringDictionary m_stringStringDictionary = null;
	public IDictionary<string, string> StringStringDictionary
	{
		get { return m_stringStringDictionary; }
		set { m_stringStringDictionary.CopyFrom(value); }
	}

	public ObjectColorDictionary m_objectColorDictionary;
	public StringColorArrayDictionary m_stringColorArrayDictionary;
#if NET_4_6 || NET_STANDARD_2_0
	public StringHashSet m_stringHashSet;
#endif

	public HelpDictionary m_testtype;

	void Reset()
	{
		// access by property
		StringStringDictionary = new Dictionary<string, string>() { { "first key", "value A" }, { "second key", "value B" }, { "third key", "value C" } };
		m_objectColorDictionary = new ObjectColorDictionary() { { gameObject, Color.blue }, { this, Color.red } };
	}
}
