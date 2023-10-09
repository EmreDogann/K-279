using UnityEngine;

namespace xNodes.Nodes
{
    [NodeWidth(400)]
    [CreateNodeMenu("Actions/Sound/Trigger Sound Scape")]
    public class TriggerSoundScapeNode : BaseNode
    {
        [SerializeField] private SubmarineSoundScape submarineSoundScape;

        [SerializeField] private SoundType soundType = SoundType.Explosion;
        [SerializeField] private ShakeOverride shakeOverride = ShakeOverride.DefaultShake;
        [SerializeField] private float minRadius = -1.0f;

        [SerializeField] private float maxRadius = -1.0f;

        public override void Execute()
        {
            submarineSoundScape.TriggerSound(soundType, shakeOverride, minRadius, maxRadius);
            NextNode("exit");
        }
    }
}