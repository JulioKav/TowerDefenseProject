using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(LineConnectedImage))]
[CanEditMultipleObjects]
public class LineConnectedImageEditor : ImageEditor
{
    SerializedProperty thickness;

    new void OnEnable()
    {
        base.OnEnable();
        thickness = serializedObject.FindProperty("thickness");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(thickness);
        serializedObject.ApplyModifiedProperties();
    }
}