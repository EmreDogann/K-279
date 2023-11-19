using System;
using System.Collections;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Inspect.Views
{
    public class PickupView : View
    {
        [SerializeField] private ItemTextAnimator itemTextAnimator;

        private Action<bool> _currentCallback;
        private IItem _currentItem;
        private InputSystemUIInputModule _uiInputModule;
        private bool _isShowing;

        public override void Initialize()
        {
            base.Initialize();
            _uiInputModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();
        }

        public void SetupPickup(IItem item, Action<bool> callback)
        {
            _currentCallback = callback;
            _currentItem = item;
        }

        private void Update()
        {
            if (!_isShowing)
            {
                return;
            }

            if (_uiInputModule.submit.action.WasPressedThisFrame() ||
                _uiInputModule.leftClick.action.WasPressedThisFrame())
            {
                if (itemTextAnimator.SkipAnimation())
                {
                    _currentCallback?.Invoke(true);
                    ViewManager.Instance.Back();
                }
            }
        }

        protected override IEnumerator Show()
        {
            if (vCam != null)
            {
                vCam.gameObject.SetActive(true);
            }

            itemTextAnimator.StartAnimation(_currentItem.GetItemInfo());
            // Wait one frame so input does not trigger right away.
            yield return null;
            _isShowing = true;
        }

        protected override IEnumerator Hide()
        {
            if (vCam != null)
            {
                vCam.gameObject.SetActive(false);
            }

            itemTextAnimator.StopAnimation();
            _isShowing = false;
            yield break;
        }
    }
}