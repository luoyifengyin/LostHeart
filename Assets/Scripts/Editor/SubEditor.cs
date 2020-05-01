using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication {
    public class SubEditor : Editor {
        public Action<Object> OnRemoveObject { private get; set; }

        public bool isExpanded = true;
        private float delBtnWidth = 60f;
        public int index;

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            isExpanded = EditorGUILayout.Foldout(isExpanded, target.GetType().Name);
            EditorGUILayout.LabelField(index.ToString(), GUILayout.Width(50));
            if (GUILayout.Button("Delete", GUILayout.Width(delBtnWidth))) {
                OnRemoveObject?.Invoke(target);
            }
            EditorGUILayout.EndHorizontal();

            if (isExpanded) DrawInspector();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawInspector() {
            DrawDefaultInspector();
        }
    }
}
