namespace Audio
{
    public enum FadeType
    {
        FadeIn,
        FadeOut
    }

    public class SoundFade
    {
        public FadeType FadeType;
        public float Volume;
        public float Duration;
    }

    public class AudioEventData
    {
        public float Volume;
        public float Pitch;
        public bool ShouldLoop;
        public bool CanPause;
        public SoundFade SoundFade;
    }
}