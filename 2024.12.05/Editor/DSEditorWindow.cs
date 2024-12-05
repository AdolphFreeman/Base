using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Windows
{
    public class DSEditorWindow : EditorWindow
    {
        [MenuItem("Dialgoue System/DSEditorWindow")]
        public static void ShowExample()
        {
            DSEditorWindow wnd = GetWindow<DSEditorWindow>("Dialogue System");
            Debug.Log(Resources.Load("StyleSheet/DSGraphViewStyles"));
        }

        private void OnEnable()
        {
            AddGraphView();   
        }

        void AddGraphView()
        {
            DSGraphView graphView = new DSGraphView();
            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }
    }   
}
