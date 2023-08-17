using Audio;
using UnityEngine;

namespace ScriptableObjects.Surface
{
    [CreateAssetMenu(fileName = "New Surface Data", menuName = "Surface Data", order = 0)]
    public class SurfaceData : ScriptableObject
    {
        public AudioSO surfaceSound;
    }
}