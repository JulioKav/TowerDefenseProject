using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
[CustomEditor(typeof(LineConnectedImage))]
[CanEditMultipleObjects]
public class LineConnectedImageEditor : ImageEditor
{
    SerializedProperty thickness;
    SerializedProperty radius;
    SerializedProperty circleResolution;

    new void OnEnable()
    {
        base.OnEnable();
        thickness = serializedObject.FindProperty("thickness");
        radius = serializedObject.FindProperty("radius");
        circleResolution = serializedObject.FindProperty("circleResolution");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(thickness);
        EditorGUILayout.PropertyField(radius);
        EditorGUILayout.PropertyField(circleResolution);
        serializedObject.ApplyModifiedProperties();
    }
}