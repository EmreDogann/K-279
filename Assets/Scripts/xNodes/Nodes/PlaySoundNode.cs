using Audio;
using UnityEngine;

namespace xNodes.Nodes
{
    [NodeWidth(300)]
    [CreateNodeMenu("Actions/Sound/Play Sound")]
    public class PlaySoundNode : BaseNode
    {
        public enum PlayMode
        {
            ThreeD,
            Attached,
            TwoD
        }

        [SerializeField] private PlayMode playMode = PlayMode.ThreeD;

        [SerializeField] private AudioSO audio;

        [SerializeField] private Transform transform;

        public override void Execute()
        {
            switch (playMode)
            {
                case PlayMode.ThreeD:
                    audio.Play(transform.position);
                    break;
                case PlayMode.Attached:
                    audio.PlayAttached(transform.gameObject);
                    break;
                case PlayMode.TwoD:
                    audio.Play2D(audio);
                    break;
            }

            NextNode("exit");
        }
    }
}