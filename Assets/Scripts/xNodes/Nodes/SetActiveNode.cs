using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes
{
    [NodeWidth(250)]
    [CreateNodeMenu("Actions/Set Active State")]
    public class SetActiveNode : BaseNode
    {
        [SerializeField] private GameObject gameObject;
        [SerializeField] private bool setActive;

        public override void Execute()
        {
            gameObject.SetActive(setActive);
            NextNode("exit");
        }
    }
}