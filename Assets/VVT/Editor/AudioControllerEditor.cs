using UnityEngine;
using UnityEditor;
using VVT.Runtime;

[CustomEditor(typeof(AudioController))]
public class AudioControllerEditor : Editor {

    public override void OnInspectorGUI() {

        var audioController  = (AudioController) target;

        base.OnInspectorGUI(); // Draw default inspector
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Details");

    }
}