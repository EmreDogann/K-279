using Audio;
using MyBox;
using UnityEngine;

namespace xNode.Nodes.Sound
{
    [NodeWidth(400)]
    [CreateNodeMenu("Actions/Sound/Fade Sound")]
    public class FadeSoundNode : BaseNode
    {
        private enum FadeDirection
        {
            FadeOut,
            FadeIn
        }

        [SerializeField] private FadeDirection fadeDirection = FadeDirection.FadeOut;

        [SerializeField] private AudioSO audio;
        [SerializeField] private bool allInstances;

        [Tooltip("The volume to fade to as a percentage.")]
        [OverrideLabel("Volume %")] [ConditionalField(nameof(fadeDirection), false, FadeDirection.FadeOut)]
        [Range(0.0f, 1.0f)] [SerializeField] private float volume;
        [SerializeField] private float fadeDuration;

        public override void Execute()
        {
            switch (fadeDirection)
            {
                case FadeDirection.FadeOut:
                    if (allInstances)
                    {
                        audio.FadeAudioAll((audio.volume.Max + audio.volume.Min) / 2.0f * volume, fadeDuration);
                    }
                    else
                    {
                        audio.FadeAudio(AudioHandle.Invalid, (audio.volume.Max + audio.volume.Min) / 2.0f * volume,
                            fadeDuration);
                    }

                    break;
                case FadeDirection.FadeIn:
                    if (allInstances)
                    {
                        audio.UnFadeAudioAll(fadeDuration);
                    }
                    else
                    {
                        audio.UnFadeAudio(AudioHandle.Invalid, fadeDuration);
                    }

                    break;
            }

            NextNode("exit");
        }
    }
}