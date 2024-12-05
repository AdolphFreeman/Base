using DS.Elements;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DS.Windows
{
    using Elements;
    
    public class DSGraphView : GraphView
    {
        public DSGraphView()
        {
            AddManipulators();
            AddGridBackground();
            
            //CreateNode();
            
            AddStyles();
        }

        private DSNode CreateNode(Vector2 position)
        {
            DSNode node = new DSNode();
            
            node.Initialize(position);
            node.Draw();
            
            AddElement(node);

            return node;
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
            this.AddManipulator(CreateNodeContextualMenu());
            
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentDragger());
        }

        public IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Node", actionEvent => AddElement(CreateNode(actionEvent.eventInfo.localMousePosition)))
            );
            return contextualMenuManipulator;
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        private void AddStyles()
        {
            StyleSheet ss = (StyleSheet)Resources.Load("StyleSheet/DSGraphViewStyles");
            styleSheets.Add(ss);
        }
    }
}
