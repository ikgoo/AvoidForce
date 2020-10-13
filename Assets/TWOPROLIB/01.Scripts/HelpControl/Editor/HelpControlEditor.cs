using UnityEditor;  //Editor 클래스 사용하기 위해 넣어줍니다.
using UnityEngine;

[CustomEditor(typeof(HelpControl))]
public class HelpControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        HelpControl itemtrigger = (HelpControl)target;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)
                                   //버튼을 만듭니다 . GUILayout.Button("버튼이름" , 가로크기, 세로크기)

        if (GUILayout.Button("생성", GUILayout.Width(120), GUILayout.Height(30)))
        {

            itemtrigger.RunGenerateList();
        }

        if (GUILayout.Button("실행", GUILayout.Width(120), GUILayout.Height(30)))
        {

            itemtrigger.StartHelp(true);
        }

        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝

    }
}
