using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    public class DSNode : Node
    {
        public string DialogueName { get; set; }
        public List<string> Chioces { get; set; }
        public string Text { get; set; }
        public DSDialogueType DialogueType { get; set; }

        public void Initialize(Vector2 position)
        {
            DialogueName = "DialogueName";
            Chioces = new List<string>();
            Text = "Dialogue Text.";
            
            SetPosition(new Rect(position, Vector2.zero));
        }

        public void Draw()
        {
            TextField dialogueTextField = new TextField()
            {
                value = DialogueName
            };
            
            titleContainer.Insert(0, dialogueTextField);
            Port inputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

            inputPort.portName = "Dialogue Connection";

            VisualElement customDataContainer = new VisualElement();
            
            inputContainer.Add(inputPort);

            Foldout textFoldout = new Foldout()
            {
                text = "Dialogue Text"
            };

            TextField textTextField = new TextField()
            {
                value = Text
            };
            
            textFoldout.Add(textTextField);
            
            customDataContainer.Add(textFoldout);
            
            extensionContainer.Add(customDataContainer);
            
            RefreshExpandedState();
        }
    }

    public enum DSDialogueType
    {
        SingleChioce, MultipleChioce
    }
}
