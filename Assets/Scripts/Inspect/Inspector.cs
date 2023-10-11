using System;
using System.Collections;
using System.Linq;
using Controllers;
using Events;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Inspect
{
    public class Inspector : MonoBehaviour
    {
        [SerializeField] private Camera playerCam;
        [SerializeField] private Camera inspectCam;

        [SerializeField] private Controller controller;
        [SerializeField] private TextMeshProUGUI textMesh;

        [Separator("Animation")]
        [Tooltip("Characters to show per second")]
        [SerializeField] private float textAnimationSpeed;
        [Tooltip("Character to show per second when reaching ellipsis")]
        [SerializeField] private float ellipsisSpeed;
        [Tooltip("The time to wait in seconds when reaching punctuation")]
        [SerializeField] private float punctuationWaitTime;

        [Separator("Events")]
        [SerializeField] private BoolEventChannelSO pauseEvent;

        private bool _isTextAnimating;
        private bool _isAtEllipsis;
        private string _messageTarget;
        private Action<bool> _currentCallback;
        private IInspectable _currentInspectable;
        private PixelPerfectCamera _inspectPixelPerfectCamera;

        private readonly char[] _punctuation = { ',', '.', '-', '?', '!' };

        private void Start()
        {
            if (inspectCam == null)
            {
                Debug.LogError("Inspect cam must be assigned!");
                return;
            }

            _inspectPixelPerfectCamera = inspectCam.GetComponent<PixelPerfectCamera>();
        }

        private void Update()
        {
            if (_isTextAnimating)
            {
                if (controller.input.RetrieveInteractPress())
                {
                    textMesh.maxVisibleCharacters = _messageTarget.Length;
                }
            }
            else
            {
                if (_currentInspectable != null && controller.input.RetrieveInteractPress())
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

            ToggleInspectCam(true);

            _currentInspectable.GetCameraAngle().gameObject.SetActive(true);
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

            ToggleInspectCam(false);

            textMesh.text = string.Empty;
            textMesh.gameObject.SetActive(false);

            _currentInspectable = null;
            _currentCallback = null;

            pauseEvent.Raise(false);
        }

        private void ToggleInspectCam(bool active)
        {
            playerCam.gameObject.SetActive(!active);
            inspectCam.gameObject.SetActive(active);

            if (_inspectPixelPerfectCamera != null)
            {
                _inspectPixelPerfectCamera.enabled = !active;
            }
        }

        private IEnumerator DisplayMessage(string itemName)
        {
            _isTextAnimating = true;
            bool isAddingRichTextTag = false;

            _messageTarget = itemName;

            textMesh.gameObject.SetActive(true);
            textMesh.text = _messageTarget;
            textMesh.maxVisibleCharacters = 0;

            for (int i = 0; i < _messageTarget.Length; i++)
            {
                char letter = _messageTarget[i];
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

                    if (!_isAtEllipsis && !CheckForEllipsis(i))
                    {
                        if (_punctuation.Contains(letter))
                        {
                            yield return new WaitForSecondsRealtime(punctuationWaitTime);
                            continue;
                        }
                    }

                    if (letter != '.')
                    {
                        _isAtEllipsis = false;
                    }

                    yield return new WaitForSecondsRealtime(
                        1 / (_isAtEllipsis ? ellipsisSpeed : textAnimationSpeed));
                }
            }

            _isTextAnimating = false;
        }

        private bool CheckForEllipsis(int i)
        {
            if (_messageTarget.Length - i < 3)
            {
                return false;
            }

            char currentLetter = _messageTarget[i];

            if (currentLetter == '.' && _messageTarget[i + 1] == '.' && _messageTarget[i + 2] == '.')
            {
                _isAtEllipsis = true;
                return true;
            }

            return false;
        }
    }
}