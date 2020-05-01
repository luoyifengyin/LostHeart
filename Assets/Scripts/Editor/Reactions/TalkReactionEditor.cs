using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    [CustomEditor(typeof(TalkReaction))]
    public class TalkReactionEditor : ReactionEditor {
        private SerializedProperty nameProperty;
        private const string namePropName = "name";

        private SerializedProperty sayProperty;
        private const string sayPropName = "say";

        private const float labelWidth = 47f;
        private readonly float singleLineHeight = EditorGUIUtility.singleLineHeight;
        private const float textLines = 3f;

        private void OnEnable() {
            nameProperty = serializedObject.FindProperty(namePropName);
            sayProperty = serializedObject.FindProperty(sayPropName);
        }

        protected override void DrawInspector() {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(namePropName, GUILayout.MaxWidth(labelWidth));
            nameProperty.stringValue = EditorGUILayout.TextField(nameProperty.stringValue);
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(sayPropName, GUILayout.MaxWidth(labelWidth));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(singleLineHeight);
            sayProperty.stringValue = GUILayout.TextArea(sayProperty.stringValue);
            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.EndHorizontal();
        }
    }
}