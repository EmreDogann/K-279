using UnityEngine;
using XNodeEditor;

namespace xNode.Nodes.Editor
{
    [CustomNodeEditor(typeof(BaseNode))]
    public class BaseNodeEditor : NodeEditor
    {
        private BaseNode _baseNode;

        public override void OnCreate()
        {
            base.OnCreate();
            _baseNode = target as BaseNode;
        }

        public override Color GetTint()
        {
            if (_baseNode == null)
            {
                _baseNode = target as BaseNode;
            }

            return _baseNode.state == BaseNode.State.Running ? new Color(0.14f, 0.86f, 0.4f, 1.0f) : base.GetTint();
        }

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            NodeEditorWindow.current.Repaint();
        }
    }
}