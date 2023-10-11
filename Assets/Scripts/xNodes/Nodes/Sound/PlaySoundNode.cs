using System;
using System.Collections.Generic;
using Attributes;
using Audio;
using UnityEngine;
using xNodes.Nodes.Delay;

namespace xNodes.Nodes.Sound
{
    [NodeWidth(400)]
    [CreateNodeMenu("Actions/Sound/Play Sound")]
    public class PlaySoundNode : BaseNode
    {
        [Serializable]
        public class PlaySoundNode_AudioData
        {
            [NodeEnum] public PlayMode playMode = PlayMode.ThreeD;
            public AudioSO audio;
            public Transform transform;
        }

        public enum PlayMode
        {
            GlobalOverride,
            ThreeD,
            Attached,
            TwoD
        }

        [NodeEnum] [SerializeField] private PlayMode playModeGlobal = PlayMode.ThreeD;

        [PlaySoundNode_AudioData] [SerializeField] private List<PlaySoundNode_AudioData> audioDataList;
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        [SerializeField] private BaseDelayNode delayFunction;

        public override void Execute()
        {
            bool shouldDelay = GetInputPort(nameof(delayFunction)).IsConnected;
            foreach (PlaySoundNode_AudioData audioData in audioDataList)
            {
                switch (audioData.playMode == PlayMode.GlobalOverride ? playModeGlobal : audioData.playMode)
                {
                    case PlayMode.ThreeD:
                        if (shouldDelay)
                        {
                            (GetInputPort(nameof(delayFunction)).Connection.node as BaseDelayNode)?.RunDelayFunction(
                                () => { audioData.audio.Play(audioData.transform.position); });
                        }
                        else
                        {
                            audioData.audio.Play(audioData.transform.position);
                        }

                        break;
                    case PlayMode.Attached:
                        if (shouldDelay)
                        {
                            (GetInputPort(nameof(delayFunction)).Connection.node as BaseDelayNode)?.RunDelayFunction(
                                () => { audioData.audio.PlayAttached(audioData.transform.gameObject); });
                        }
                        else
                        {
                            audioData.audio.PlayAttached(audioData.transform.gameObject);
                        }

                        break;
                    case PlayMode.TwoD:
                        if (shouldDelay)
                        {
                            (GetInputPort(nameof(delayFunction)).Connection.node as BaseDelayNode)?.RunDelayFunction(
                                () => { audioData.audio.Play2D(); });
                        }
                        else
                        {
                            audioData.audio.Play2D();
                        }

                        break;
                }
            }

            NextNode("exit");
        }
    }
}