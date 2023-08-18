using System;
using Audio;
using DG.Tweening;
using MyBox;
using Rooms;
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

        [Separator("Audio")]
        [SerializeField] private AudioSO turningValueAudio;
        [SerializeField] private AudioSO closingDoorAudio;

        [Separator("Open Animation")]
        [SerializeField] private Transform turningValve;
        [SerializeField] private float valveTurnEulerAngle = 25;
        [SerializeField] private float valveTurnDuration = 1.0f;
        [SerializeField] private Ease valveTurnEasing = Ease.Linear;

        [SerializeField] private float openEulerAngle = 25;
        [SerializeField] private float openDuration = 1.0f;
        [SerializeField] private Ease openEasing = Ease.Linear;

        private bool _isButtonOn;
        private Vector3 _valveStartingRotation;
        private Vector3 _startingRotation;
        private Transform _child;

        private Sequence _doorOpenSequence;

        public static event Action<RoomType, Action> OnRoomSwitching;

        private void Awake()
        {
            _child = transform.GetChild(0);
            _startingRotation = _child.localEulerAngles;
            _valveStartingRotation = turningValve.localEulerAngles;

            _doorOpenSequence = DOTween.Sequence();
            _doorOpenSequence
                .Append(turningValve
                    .DOLocalRotate(new Vector3(_valveStartingRotation.x, _valveStartingRotation.y, valveTurnEulerAngle),
                        valveTurnDuration)
                    .SetEase(valveTurnEasing))
                .Insert(valveTurnDuration * 0.9f, _child
                    .DOLocalRotate(new Vector3(_startingRotation.x, openEulerAngle, _startingRotation.z), openDuration)
                    .SetEase(openEasing)
                    .OnComplete(() =>
                    {
                        OnRoomSwitching?.Invoke(connectingRoom, () => { _doorOpenSequence.SmoothRewind(); });
                    }))
                .SetAutoKill(false)
                .Pause();
        }

        public RoomType GetConnectingRoom()
        {
            return connectingRoom;
        }

        public Transform GetSpawnPoint()
        {
            return spawnPoint;
        }

        public void PlayClosingAudio()
        {
            closingDoorAudio.Play2D();
        }

        public void InteractionContinues(bool isInteractionKeyDown)
        {
            if (isInteractionKeyDown && !_doorOpenSequence.IsPlaying())
            {
                // TODO: Pause Game
                // TODO: Stop audio in room.

                turningValueAudio.Play(transform.position);
                _doorOpenSequence.PlayForward();
            }
        }

        public void InteractionEnd() {}

        public void InteractionStart() {}
    }
}