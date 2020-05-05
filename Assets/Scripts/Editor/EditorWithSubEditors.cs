using MyGameApplication;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication {
    public abstract class EditorWithSubEditors<TObject> : Editor
        where TObject : Object {

        protected List<SubEditor> subEditors;

        protected void CreateSubEditors(TObject[] subEditorObjects) {
            if (subEditors != null && subEditors.Count == subEditorObjects.Length) return;
            CleanSubEditors();
            subEditors = new List<SubEditor>();

            //List<TObject> list = subEditorObjects.ToList();
            //int idx = 0;
            //while (idx < subEditorObjects.Length) {
            //    if (subEditorObjects[idx] == null) {
            //        list.RemoveAt(idx);
            //    }
            //    else idx++;
            //}
            //subEditorObjects = list.ToArray();

            for (int i = 0; i < subEditorObjects.Length; i++) {
                SubEditor subEditor = CreateEditor(subEditorObjects[i]) as SubEditor;
                subEditors.Add(subEditor);
                subEditor.index = i;
                SubEditorSetup(subEditor);
            }
        }

        protected void CleanSubEditors() {
            if (subEditors == null) return;
            for (int i = 0; i < subEditors.Count; i++) {
                DestroyImmediate(subEditors[i]);
            }
            subEditors = null;
        }

        protected abstract void SubEditorSetup(SubEditor editor);
    }
}
