using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    [CustomEditor(typeof(NumberInputReaction))]
    public class NumberInputReactionEditor : ReactionEditor {
        private SerializedProperty correctAnswerProperty;
        private const string correctAnswerPropName = "correctAnswer";

        private SerializedProperty correctGotoProperty;
        private const string correctGotoPropName = "correctGoto";

        private SerializedProperty wrongGotoProperty;
        private const string wrongGotoPropName = "wrongGoto";

        //private SerializedProperty cancelGotoProperty;
        //private const string cancelGotoPropName = "cancelGoto";

        private int labelWidth = 82;

        private void OnEnable() {
            correctAnswerProperty = serializedObject.FindProperty(correctAnswerPropName);
            correctGotoProperty = serializedObject.FindProperty(correctGotoPropName);
            wrongGotoProperty = serializedObject.FindProperty(wrongGotoPropName);
            //cancelGotoProperty = serializedObject.FindProperty(cancelGotoPropName);
        }

        protected override void DrawInspector() {
            EditorGUILayout.PropertyField(correctAnswerProperty);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(correctGotoPropName, GUILayout.Width(labelWidth));
            correctGotoProperty.intValue = EditorGUILayout.IntField(correctGotoProperty.intValue);
            EditorGUILayout.LabelField(wrongGotoPropName, GUILayout.Width(labelWidth));
            wrongGotoProperty.intValue = EditorGUILayout.IntField(wrongGotoProperty.intValue);
            EditorGUILayout.EndHorizontal();

            //EditorGUILayout.PropertyField(cancelGotoProperty);
        }
    }
}
