using System;
using Audio;
using Cinemachine;
using DG.Tweening;
using Inspect;
using Items;
using MyBox;
using Rooms;
using UnityEngine;

namespace Interactables
{
    public class Door : MonoBehaviour, IInteractableObjects, IInspectable
    {
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

        [Separator("Inspection")]
        [SerializeField] private bool isInspectable;
        [SerializeField] private CinemachineVirtualCamera inspectVirtualCamera;
        [SerializeField] private string inspectMessage;

        private bool _handleRemoved;
        private bool _isLocked;
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

            inspectVirtualCamera.gameObject.SetActive(false);

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
            closingDoorAudio.Play(transform.position);
        }

        public bool InteractionContinues(bool isInteractionKeyDown)
        {
            if (isInteractionKeyDown && !_doorOpenSequence.IsPlaying())
            {
                if (_isLocked)
                {
                    // TODO: Play locked audio
                    return false;
                }

                // TODO: Pause Game
                // TODO: Stop audio in room.

                turningValueAudio.Play(transform.position);
                _doorOpenSequence.PlayForward();

                return true;
            }

            return false;
        }

        public void InteractionEnd() {}

        public void InteractionStart() {}

        public bool IsInteractable()
        {
            return _isLocked;
        }

        public void SetLocked(bool isLocked, bool playLockedSound)
        {
            _isLocked = isLocked;
            if (playLockedSound)
            {
                // TODO: Play locking audio
            }
        }

        [ButtonMethod]
        public void RemoveHandle()
        {
            turningValve.gameObject.SetActive(false);
            _handleRemoved = true;
        }

        public void PlayLockedAudio()
        {
            // TODO: Play locking audio
        }

        public CinemachineVirtualCamera GetCameraAngle()
        {
            return inspectVirtualCamera;
        }

        public string GetMessage()
        {
            return inspectMessage;
        }

        public bool IsInspectable()
        {
            return isInspectable;
        }

        public bool IsExpectingItem()
        {
            return _handleRemoved;
        }

        public ItemType GetExpectedItem()
        {
            return ItemType.Valve;
        }

        public bool TryItem(IItem item)
        {
            if (IsExpectingItem() && item.GetItemType() == GetExpectedItem())
            {
                turningValve.gameObject.SetActive(true);
                _handleRemoved = false;
                _isLocked = false;
                // TODO: Play placing handle audio

                return true;
            }

            return false;
        }
    }
}