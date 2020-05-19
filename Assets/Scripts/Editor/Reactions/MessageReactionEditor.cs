using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    [CustomEditor(typeof(MessageReaction))]
    public class MessageReactionEditor : ReactionEditor {
        private SerializedProperty messageProperty;
        private const string messagePropName = "m_Message";

        private void OnEnable() {
            messageProperty = serializedObject.FindProperty(messagePropName);
        }

        protected override void DrawInspector() {
            EditorGUILayout.BeginHorizontal();
            messageProperty.stringValue = EditorGUILayout.TextArea(messageProperty.stringValue,
                GUI.skin.GetStyle("TextArea"));
            EditorGUILayout.EndHorizontal();
        }
    }
}
