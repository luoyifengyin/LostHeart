using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    [CustomEditor(typeof(ItemReaction))]
    public class ItemReactionEditor : ReactionEditor {
        private SerializedProperty itemIdProperty;
        private const string itemIdPropName = "m_ItemId";

        private SerializedProperty cntProperty;
        private const string cntPropName = "m_GainCnt";

        private int labelWidth = 70;

        private void OnEnable() {
            itemIdProperty = serializedObject.FindProperty(itemIdPropName);
            cntProperty = serializedObject.FindProperty(cntPropName);
        }

        protected override void DrawInspector() {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Item Id", GUILayout.Width(labelWidth));
            itemIdProperty.intValue = EditorGUILayout.IntField(itemIdProperty.intValue);
            EditorGUILayout.LabelField("Gain Cnt", GUILayout.Width(labelWidth));
            cntProperty.intValue = EditorGUILayout.IntField(cntProperty.intValue);
            EditorGUILayout.EndHorizontal();
        }
    }
}
