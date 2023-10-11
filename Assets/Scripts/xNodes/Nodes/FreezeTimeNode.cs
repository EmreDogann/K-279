using Attributes;
using Events;
using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Freeze Time")]
    public class FreezeTimeNode : BaseNode
    {
        private enum FreezeMode
        {
            Freeze,
            Unfreeze
        }
        [NodeEnum] [SerializeField] private FreezeMode freezeMode;
        [SerializeField] private BoolEventChannelSO pauseEvent;

        public override void Execute()
        {
            switch (freezeMode)
            {
                case FreezeMode.Freeze:
                    pauseEvent.Raise(true);
                    break;
                case FreezeMode.Unfreeze:
                    pauseEvent.Raise(false);
                    break;
            }

            NextNode("exit");
        }
    }
}