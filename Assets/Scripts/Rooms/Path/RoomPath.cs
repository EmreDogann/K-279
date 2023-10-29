using UnityEngine;
using UnityEngine.Splines;

namespace Rooms.Path
{
    public class RoomPath : MonoBehaviour
    {
        [SerializeField] private SplineContainer path;

        public Vector3 EvaluatePosition(float lengthPercentage)
        {
            return path.EvaluatePosition(lengthPercentage);
        }
    }
}