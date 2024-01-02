using Attributes;
using Lights;
using UnityEngine;

namespace xNode.Nodes.Lights
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Lights/Light State")]
    public class LightStateNode : BaseNode
    {
        [NodeEnum] [SerializeField] private LightState lightState;
        [Space]
        [SerializeField] private LightChangeMode changeMode;

        [SerializeField] private float fadeDuration = 0.3f;
        [SerializeField] private float afterFadeWaitDuration = 1.1f;

        public override void Execute()
        {
            LightManager.Instance.ChangeState(lightState, changeMode, fadeDuration, afterFadeWaitDuration);
            NextNode("exit");
        }
    }
}