//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//namespace MyGameApplication.Maze.NPC.Reactions {
//    [CustomEditor(typeof(Branch))]
//    public class ChoicesReactionBranchEditor : Editor {
//        private Branch branch;

//        private SerializedProperty nameProperty;
//        private SerializedProperty branchTypeProperty;
//        private SerializedProperty gotoProperty;
//        private SerializedProperty reactionProperty;

//        private const string namePropName = "name";
//        private const string branchTypePropName = "type";
//        private const string gotoPropName = "gotoIndex";
//        private const string reactionPropName = "reaction";

//        private void OnEnable() {
//            nameProperty = serializedObject.FindProperty(namePropName);
//            branchTypeProperty = serializedObject.FindProperty(branchTypePropName);
//            gotoProperty = serializedObject.FindProperty(gotoPropName);
//            reactionProperty = serializedObject.FindProperty(reactionPropName);
//        }

//        public override void OnInspectorGUI() {
//            serializedObject.Update();
//            EditorGUILayout.PropertyField(nameProperty);

//            EditorGUILayout.BeginHorizontal();
            
//            //serializedObject.Update();
//            //if (branch.branchType == ChoicesReaction.BranchType.ReactionCollection)
//            //    EditorGUILayout.ObjectField(reactionProperty);
//            //else
//                gotoProperty.intValue = EditorGUILayout.IntField(gotoProperty.intValue);
//            EditorGUILayout.EndHorizontal();

//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//}
