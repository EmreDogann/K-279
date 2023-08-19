using System;
using System.Collections;
using Controllers;
using Events;
using MyBox;
using TMPro;
using UnityEngine;

namespace Inspect
{
    public class Inspector : MonoBehaviour
    {
        [SerializeField] private Controller controller;
        [SerializeField] private TextMeshProUGUI text;

        [Separator("Buttons")]
        [SerializeField] private TextMeshProUGUI confirmButton;
        [SerializeField] private TextMeshProUGUI cancelButton;

        [Separator("Animation")]
        [Tooltip("Characters to show per second")]
        [SerializeField] private float textAnimationSpeed;

        [Separator("Events")]
        [SerializeField] private BoolEventChannelSO pauseEvent;

        private bool _waitingConfirmation;

        private bool _isTextAnimating;
        private string _messageTarget;
        private Action<bool> _currentCallback;
        private IInspectable _currentInspectable;

        private void Update()
        {
            if (_isTextAnimating)
            {
                if (controller.input.RetrieveInteractInput())
                {
                    text.maxVisibleCharacters = _messageTarget.Length;
                }
            }
            else if (!_waitingConfirmation)
            {
                if (_currentInspectable != null && controller.input.RetrieveInteractInput())
                {
                    _currentCallback?.Invoke(false);
                    ClosePopup();
                }
            }
        }

        public void Inspect(IInspectable inspectable, Action<bool> callback = null)
        {
            _currentInspectable = inspectable;
            _currentCallback = callback;

            pauseEvent.Raise(true);

            inspectable.GetCameraAngle().gameObject.SetActive(true);
            StartCoroutine(DisplayMessage(_currentInspectable.GetMessage()));
        }

        public void InspectWithConfirmation(IInspectable inspectable, Action<bool> callback = null)
        {
            _waitingConfirmation = true;
            Inspect(inspectable, callback);
        }

        public void ConfirmInspect()
        {
            _currentCallback?.Invoke(true);
            ClosePopup();
        }

        public void CancelInspect()
        {
            _currentCallback?.Invoke(false);
            ClosePopup();
        }

        private void ClosePopup()
        {
            _currentInspectable.GetCameraAngle().gameObject.SetActive(false);

            text.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);

            _currentInspectable = null;
            _currentCallback = null;
            _waitingConfirmation = false;

            pauseEvent.Raise(false);
        }

        private IEnumerator DisplayMessage(string itemName)
        {
            _isTextAnimating = true;
            bool isAddingRichTextTag = false;

            _messageTarget = itemName;

            text.gameObject.SetActive(true);
            text.text = _messageTarget;
            text.maxVisibleCharacters = 0;

            foreach (char letter in _messageTarget)
            {
                if (text.maxVisibleCharacters == _messageTarget.Length)
                {
                    break;
                }

                if (letter == '<' || isAddingRichTextTag)
                {
                    isAddingRichTextTag = true;
                    if (letter == '>')
                    {
                        isAddingRichTextTag = false;
                    }
                }
                else
                {
                    text.maxVisibleCharacters++;
                    yield return new WaitForSecondsRealtime(1 / textAnimationSpeed);
                }
            }

            // if (_waitingConfirmation)
            // {
            //     EventSystem.current.SetSelectedGameObject(confirmButton.gameObject);
            //
            //     confirmButton.gameObject.SetActive(true);
            //     cancelButton.gameObject.SetActive(true);
            // }

            _isTextAnimating = false;
        }
    }
}