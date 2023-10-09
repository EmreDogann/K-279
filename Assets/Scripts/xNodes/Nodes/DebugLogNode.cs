using UnityEngine;

namespace xNodes.Nodes
{
    [CreateNodeMenu("Actions/Debug Log")]
    public class DebugLogNode : BaseNode
    {
        [TextArea]
        [SerializeField] private string debugText;

        public override void Execute()
        {
            Debug.Log(debugText);
        }
    }
}