using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GameSetAndStat))]
public class GUIGameSetAndStat : Editor
{
    
    public override void OnInspectorGUI()
    {
         base.OnInspectorGUI();

        if (GUILayout.Button("Save Prefference"))
            GameSetAndStat.Instance.SaveData();
        if (GUILayout.Button("Load Prefference"))
            GameSetAndStat.Instance.GetData();
        if (GUILayout.Button("Delete Saves Prefference"))
            GameSetAndStat.Instance.Del();
    }

}
