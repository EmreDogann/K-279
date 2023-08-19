using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public struct AudioHandle
    {
        public static AudioHandle Invalid = new AudioHandle(-1, null);

        public delegate void OnHandleStale(AudioHandle audioHandle);

        public event OnHandleStale OnStale;
        private event OnHandleStale HandleStale
        {
            add => OnStale += value;
            remove => OnStale -= value;
        }

        internal int ID;
        internal AudioSO Audio;

        public AudioHandle(int id, AudioSO audio) : this()
        {
            ID = id;
            Audio = audio;
        }

        public override bool Equals(object obj)
        {
            return obj is AudioHandle x && ID == x.ID && Audio == x.Audio;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ Audio.GetHashCode();
        }

        public static bool operator ==(AudioHandle x, AudioHandle y)
        {
            return x.ID == y.ID && x.Audio == y.Audio;
        }

        public static bool operator !=(AudioHandle x, AudioHandle y)
        {
            return !(x == y);
        }

        internal void MarkStale()
        {
            Debug.Log("Marking Stale: " + Audio.name);
            OnStale?.Invoke(this);
        }
    }
}