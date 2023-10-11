using System;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes
{
    [NodeWidth(100)]
    [NodeColorHeader(0.14f, 0.66f, 0.4f)]
    [NodeColorBody(0.07f, 0.31f, 0.19f, 0.5f)]
    [CreateNodeMenu("Start")]
    public class StartNode : BaseNode
    {
        [Serializable]
        public class StartNodeOutput {}

        [Output(connectionType: ConnectionType.Override)] public StartNodeOutput nodeExit;

        public override void Execute()
        {
            NextNode("nodeExit");
        }
    }
}