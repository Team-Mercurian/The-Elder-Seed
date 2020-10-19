using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 #if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(UneditableTransform))]

public class UneditableTransformEditor : Editor {

    public override void OnInspectorGUI() {

        UneditableTransform t = (UneditableTransform)target;

        // Replicate the standard transform inspector gui
        
        GUI.enabled = false;
        bool restoreWideMode = EditorGUIUtility.wideMode;
        EditorGUIUtility.wideMode = true;
        EditorGUIUtility.labelWidth = 65;
        EditorGUIUtility.fieldWidth = 50;

        EditorGUI.indentLevel = 0;

        Vector3 position = EditorGUILayout.Vector3Field("Position", t.position);
        Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", t.rotation);
        Vector3 scale = EditorGUILayout.Vector3Field("Scale", t.scale);

        EditorGUIUtility.labelWidth = 0;
        EditorGUIUtility.fieldWidth = 0;

        EditorGUIUtility.wideMode = restoreWideMode;
        GUI.enabled = true;

        }

    private Vector3 FixIfNaN(Vector3 v) {
        if (float.IsNaN(v.x)) {
            v.x = 0;
        }
        if (float.IsNaN(v.y)) {
            v.y = 0;
        }
        if (float.IsNaN(v.z)) {
            v.z = 0;
        }
        return v;
    }
        }

#endif  