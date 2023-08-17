using System;
using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactables
{
    public class Door : MonoBehaviour, IInteractableObjects
    {
        [FormerlySerializedAs("roomToEnter")]
        [Separator("Room Settings")]
        [SerializeField] private RoomType connectingRoom;
        [SerializeField] private Transform spawnPoint;

        [Separator("Open Animation")]
        [SerializeField] private float openEulerAngle = 25;
        [SerializeField] private float openDuration = 1.0f;
        [SerializeField] private Ease openEasing = Ease.Linear;

        private bool _isButtonOn;
        private Vector3 _startingRotation;
        private Transform _child;

        public static event Action<RoomType> OnRoomSwitching;

        private void Awake()
        {
            _child = transform.GetChild(0);
            _startingRotation = _child.localEulerAngles;
        }

        public RoomType GetConnectingRoom()
        {
            return connectingRoom;
        }

        public Transform GetSpawnPoint()
        {
            return spawnPoint;
        }

        public void InteractionContinues(bool isInteractionKeyDown)
        {
            if (isInteractionKeyDown)
            {
                // TODO: Pause Game
                // TODO: Stop audio in room.
                // TODO: Play opening door audio.

                _child.DOKill();
                _child.DOLocalRotate(
                        new Vector3(_startingRotation.x, openEulerAngle, _startingRotation.z),
                        openDuration)
                    .From(_startingRotation)
                    .SetEase(openEasing)
                    .OnComplete(() => { OnRoomSwitching?.Invoke(connectingRoom); });
            }
        }

        public void InteractionEnd() {}

        public void InteractionStart() {}
    }
}