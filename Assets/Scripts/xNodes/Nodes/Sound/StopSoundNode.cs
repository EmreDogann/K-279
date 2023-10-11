using Audio;
using MyBox;
using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes.Sound
{
    [NodeWidth(400)]
    [CreateNodeMenu("Actions/Sound/Stop Sound")]
    public class StopSoundNode : BaseNode
    {
        [SerializeField] private AudioSO audio;
        [SerializeField] private bool stopAllInstances;

        [SerializeField] private bool fadeOut;
        [ConditionalField(nameof(fadeOut))] [SerializeField] private float fadeOutDuration;

        public override void Execute()
        {
            if (stopAllInstances)
            {
                audio.StopAll(fadeOut, fadeOutDuration);
            }
            else
            {
                audio.Stop(fadeOut, fadeOutDuration);
            }

            NextNode("exit");
        }
    }
}