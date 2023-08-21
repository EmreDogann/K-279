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
        [SerializeField] private TextMeshProUGUI textMesh;

        [Separator("Animation")]
        [Tooltip("Characters to show per second")]
        [SerializeField] private float textAnimationSpeed;

        [Separator("Events")]
        [SerializeField] private BoolEventChannelSO pauseEvent;

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
                    textMesh.maxVisibleCharacters = _messageTarget.Length;
                }
            }
            else
            {
                if (_currentInspectable != null && controller.input.RetrieveInteractInput())
                {
                    _currentCallback?.Invoke(false);
                    ClosePopup();
                }
            }
        }

        public void OpenInspect(IInspectable inspectable, Action<bool> onCompleteCallback = null)
        {
            _currentInspectable = inspectable;
            _currentCallback = onCompleteCallback;

            pauseEvent.Raise(true);

            inspectable.GetCameraAngle().gameObject.SetActive(true);
            StartCoroutine(DisplayMessage(_currentInspectable.GetMessage()));
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

            textMesh.text = string.Empty;
            textMesh.gameObject.SetActive(false);

            _currentInspectable = null;
            _currentCallback = null;

            pauseEvent.Raise(false);
        }

        private IEnumerator DisplayMessage(string itemName)
        {
            _isTextAnimating = true;
            bool isAddingRichTextTag = false;

            _messageTarget = itemName;

            textMesh.gameObject.SetActive(true);
            textMesh.text = _messageTarget;
            textMesh.maxVisibleCharacters = 0;

            foreach (char letter in _messageTarget)
            {
                if (textMesh.maxVisibleCharacters == _messageTarget.Length)
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
                    textMesh.maxVisibleCharacters++;
                    textMesh.ForceMeshUpdate(true);

                    yield return new WaitForSecondsRealtime(1 / textAnimationSpeed);
                }
            }

            _isTextAnimating = false;
        }
    }
}