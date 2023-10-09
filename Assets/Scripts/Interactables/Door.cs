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
        [SerializeField] private AudioSO lockingAudio;
        [SerializeField] private AudioSO doorLockedAudio;

        [field: SerializeReference] public bool PlayConnectingRoomAmbience { get; private set; }

        [Separator("Open Animation")]
        [SerializeField] private Transform turningValve;
        [SerializeField] private float valveTurnEulerAngle = 25;
        [SerializeField] private float valveTurnDuration = 1.0f;
        [SerializeField] private Ease valveTurnEasing = Ease.Linear;

        [SerializeField] private float openEulerAngle = 25;
        [SerializeField] private float openDuration = 1.0f;
        [SerializeField] private Ease openEasing = Ease.Linear;

        [Separator("Inspection")]
        [SerializeField] private ItemType expectedItem;
        [SerializeField] private bool isInspectable;
        [SerializeField] private CinemachineVirtualCamera inspectVirtualCamera;
        [SerializeField] private string inspectMessage;

        [Separator("Door State")]
        [ReadOnly] [SerializeField] private bool isLocked;

        private bool _handleRemoved;
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


        public void InteractionStart()
        {
            if (!_doorOpenSequence.IsPlaying())
            {
                if (isLocked)
                {
                    doorLockedAudio.Play(transform.position);
                }

                if (isLocked || _handleRemoved)
                {
                    return;
                }

                turningValueAudio.Play(transform.position);
                _doorOpenSequence.PlayForward();
            }
        }

        public void InteractionContinues() {}
        public void InteractionEnd() {}
        public void InteractionAreaEnter() {}
        public void InteractionAreaExit() {}

        public bool IsInteractable()
        {
            return true;
        }

        public void SetLocked(bool locked, bool playLockedSound = true)
        {
            isLocked = locked;
            if (playLockedSound)
            {
                lockingAudio.Play(transform.position);
            }
        }

        [ButtonMethod]
        private void LockDoor()
        {
            SetLocked(true);
        }

        [ButtonMethod]
        private void UnlockDoor()
        {
            SetLocked(false);
        }

        [ButtonMethod]
        private void RemoveHandle()
        {
            turningValve.gameObject.SetActive(false);
            _handleRemoved = true;
            isLocked = false;
            isInspectable = true;
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

        public bool IsExpectingItem(out ItemType itemType)
        {
            itemType = expectedItem;
            return _handleRemoved;
        }

        public bool HasAvailableItem()
        {
            return false;
        }

        public bool TryItem(IItem item)
        {
            bool isExpectingItem = IsExpectingItem(out ItemType itemType);
            if (isExpectingItem && item.GetItemType() == itemType)
            {
                item.Consume();

                turningValve.gameObject.SetActive(true);
                _handleRemoved = false;
                isLocked = false;
                isInspectable = false;
                // TODO: Play placing handle audio

                return true;
            }

            return false;
        }

        public IItem TryTakeItem()
        {
            return null;
        }
    }
}