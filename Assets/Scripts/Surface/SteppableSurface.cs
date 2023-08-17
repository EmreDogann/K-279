using ScriptableObjects.Surface;
using UnityEngine;

namespace Surface
{
    public class SteppableSurface : MonoBehaviour, ISteppable
    {
        [SerializeField] private SurfaceData _surfaceData;

        public SurfaceData GetSurfaceData()
        {
            return _surfaceData;
        }
    }
}