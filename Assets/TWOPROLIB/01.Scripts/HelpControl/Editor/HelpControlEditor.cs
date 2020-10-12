using UnityEditor;  //Editor 클래스 사용하기 위해 넣어줍니다.
using UnityEngine;

[CustomEditor(typeof(HelpControl))]
public class HelpControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();



        //ItemEffectTrigger.cs 의 객체를 받아옵니다 => 이래야 버튼시 명령을 내릴수 잇습니다
        HelpControl itemtrigger = (HelpControl)target;

        EditorGUILayout.BeginHorizontal();  //BeginHorizontal() 이후 부터는 GUI 들이 가로로 생성됩니다.
        GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)
                                   //버튼을 만듭니다 . GUILayout.Button("버튼이름" , 가로크기, 세로크기)

        if (GUILayout.Button("이펙트 실행", GUILayout.Width(120), GUILayout.Height(30)))
        {

            //ItemEffectTrigger 클래스에서 버튼 누를시 해당 명령을 구현해줍니다.
            itemtrigger.RunGenerateList();
        }
        GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
        EditorGUILayout.EndHorizontal();  // 가로 생성 끝

    }
}
