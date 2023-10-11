using Attributes;
using Lights;
using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes.Lights
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Lights/Light State")]
    public class LightStateNode : BaseNode
    {
        [NodeEnum] [SerializeField] private LightState lightState;
        [Space]
        [SerializeField] private float changeDuration = 0.3f;
        [SerializeField] private float afterChangeWaitDuration = 1.1f;

        public override void Execute()
        {
            LightManager.Instance.ChangeLightColor(lightState, changeDuration, afterChangeWaitDuration);
            NextNode("exit");
        }
    }
}