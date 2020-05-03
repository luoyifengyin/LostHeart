using MyGameApplication.Maze.NPC.Reactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Maze.NPC {
    [CustomEditor(typeof(ReactionCollection))]
    public class ReactionCollectionEditor : EditorWithSubEditors<Reaction> {
        private ReactionCollection reactionCollection;

#if UNITY_EDITOR
        private SerializedProperty nameProperty;
        private const string namePropName = "description";
#endif

        private SerializedProperty reactionsProperty;
        private const string reactionsPropName = "reactions";

        private Type[] reactionTypes;
        private string[] reactionNames;

        private int selectedIndex = 0;
        private const string defaultSelectedRectionTypeName = "TalkReaction";
        private int addAtIndex = -1;

        private void Awake() {
            SetReactionTypesAndNames();
            for (int i = 0;i < reactionNames.Length; i++) {
                if (reactionNames[i] == defaultSelectedRectionTypeName) {
                    selectedIndex = i;
                    break;
                }
            }
        }

        private void OnEnable() {
            reactionCollection = (ReactionCollection)target;
#if UNITY_EDITOR
            nameProperty = serializedObject.FindProperty(namePropName);
#endif
            reactionsProperty = serializedObject.FindProperty(reactionsPropName);
            if (reactionCollection.reactions == null)
                reactionCollection.reactions = new Reaction[0];
            CreateSubEditors(reactionCollection.reactions);
            SetReactionTypesAndNames();
        }

        private void OnDisable() {
            CleanSubEditors();
        }

        protected override void SubEditorSetup(SubEditor editor) {
            editor.OnRemoveObject = (obj) => {
                reactionsProperty.RemoveFromObjectArray(obj);
            };
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
#if UNITY_EDITOR
            EditorGUILayout.PropertyField(nameProperty);
#endif

            CreateSubEditors(reactionCollection.reactions);
            for (int i = 0; i < subEditors.Count; i++) {
                subEditors[i].OnInspectorGUI();
            }

            selectedIndex = EditorGUILayout.Popup("Reaction Type", selectedIndex, reactionNames);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Selected Reaction")) {
                Reaction newReaction = CreateInstance(reactionTypes[selectedIndex]) as Reaction;
                int idx = addAtIndex < 0 ? reactionsProperty.arraySize + addAtIndex + 1 : addAtIndex;
                idx = Mathf.Clamp(idx, 0, reactionsProperty.arraySize);
                reactionsProperty.AddToObjectArray(newReaction, idx);
                //AddSubEditor(newReaction, idx);
            }
            EditorGUILayout.LabelField("at", GUILayout.MaxWidth(20));
            addAtIndex = EditorGUILayout.IntField(addAtIndex);
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        public void SetReactionTypesAndNames() {
            Type reactionType = typeof(Reaction);
            Type[] allTypes = reactionType.Assembly.GetTypes();
            List<Type> reactionSubTypeList = new List<Type>();
            for (int i = 0; i < allTypes.Length; i++) {
                if (allTypes[i].IsSubclassOf(reactionType) && !allTypes[i].IsAbstract)
                    reactionSubTypeList.Add(allTypes[i]);
            }
            reactionTypes = reactionSubTypeList.ToArray();

            reactionNames = new string[reactionTypes.Length];
            for (int i = 0; i < reactionTypes.Length; i++) {
                reactionNames[i] = reactionTypes[i].Name;
            }
        }
    }
}
