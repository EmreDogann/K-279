using System;
using System.Collections.Generic;
using Attributes;
using Audio;
using UnityEngine;

namespace xNodes.Nodes
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

        public override void Execute()
        {
            foreach (PlaySoundNode_AudioData audioData in audioDataList)
            {
                switch (audioData.playMode == PlayMode.GlobalOverride ? playModeGlobal : audioData.playMode)
                {
                    case PlayMode.ThreeD:
                        audioData.audio.Play(audioData.transform.position);
                        break;
                    case PlayMode.Attached:
                        audioData.audio.PlayAttached(audioData.transform.gameObject);
                        break;
                    case PlayMode.TwoD:
                        audioData.audio.Play2D();
                        break;
                }
            }

            NextNode("exit");
        }
    }
}