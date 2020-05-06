using UnityEditor;

namespace MyGameApplication.Maze.NPC.Reactions {
    [CustomEditor(typeof(ConditionReaction))]
    public class ConditionReactionEditor : ReactionEditor {
        private ConditionReaction conditionReaction;

        private SerializedProperty keyProperty;
        private const string keyPropName = "key";

        private SerializedProperty valueProperty;
        private const string valuePropName = "satisfied";

        private void OnEnable() {
            conditionReaction = (ConditionReaction)target;
            keyProperty = serializedObject.FindProperty(keyPropName);
            valueProperty = serializedObject.FindProperty(valuePropName);
        }

        protected override void DrawInspector() {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(keyPropName);
            EditorGUILayout.LabelField("true/false");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            keyProperty.stringValue = EditorGUILayout.TextField(keyProperty.stringValue);
            valueProperty.boolValue = EditorGUILayout.Toggle(valueProperty.boolValue);
            EditorGUILayout.EndHorizontal();
        }
    }
}
