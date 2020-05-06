using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    [CustomEditor(typeof(IfGotoReaction))]
    public class IfGotoReactionEditor : ReactionEditor {
        private IfGotoReaction ifGotoReaction;

        private SerializedProperty keyProperty;
        private const string keyPropName = "key";

        private SerializedProperty valueProperty;
        private const string valuePropName = "value";

        private SerializedProperty gotoProperty;
        private const string gotoPropName = "gotoIdx";

        private SerializedProperty elseGotoProperty;
        private const string elseGotoPropName = "elseGoto";

        private void OnEnable() {
            ifGotoReaction = (IfGotoReaction)target;
            keyProperty = serializedObject.FindProperty(keyPropName);
            valueProperty = serializedObject.FindProperty(valuePropName);
            gotoProperty = serializedObject.FindProperty(gotoPropName);
            elseGotoProperty = serializedObject.FindProperty(elseGotoPropName);
        }

        protected override void DrawInspector() {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("if", GUILayout.Width(23));
            keyProperty.stringValue = EditorGUILayout.TextField(keyProperty.stringValue);
            ifGotoReaction.valueType = (ValueType)EditorGUILayout.EnumPopup(ifGotoReaction.valueType);
            if (ifGotoReaction.valueType == ValueType.Const) {
                ifGotoReaction.value = EditorGUILayout.Toggle(ifGotoReaction.value);
            }
            else ifGotoReaction.valueKey = EditorGUILayout.TextField(ifGotoReaction.valueKey);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUIUtility.singleLineHeight * 2);
            //EditorGUILayout.Space();
            EditorGUILayout.LabelField("goto", GUILayout.Width(40));
            gotoProperty.intValue = EditorGUILayout.IntField(gotoProperty.intValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("else goto", GUILayout.MaxWidth(EditorGUIUtility.labelWidth));
            elseGotoProperty.intValue = EditorGUILayout.IntField(elseGotoProperty.intValue);
            EditorGUILayout.EndHorizontal();
        }
    }
}
