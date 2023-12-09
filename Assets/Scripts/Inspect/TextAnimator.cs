using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events;
using MyBox;
using TMPro;
using UnityEngine;

namespace Inspect
{
    public class TextAnimator : MonoBehaviour
    {
        [SerializeField] private Camera playerCam;
        [SerializeField] private Camera inspectCam;

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
        private string _currentMessageTarget;
        private List<string> _messages = new List<string>();
        private int _messageIndex;

        private Coroutine _displayMessageCoroutine;

        private readonly char[] _punctuation = { ',', '.', '-', '?', '!' };

        private void Start()
        {
            if (inspectCam == null)
            {
                Debug.LogError("Inspect cam must be assigned!");
            }
        }

        public void StartAnimation(string text)
        {
            pauseEvent.Raise(true);
            ToggleInspectCam(true);

            textMesh.gameObject.SetActive(true);

            _messages.Clear();
            _messages = text.Split('\n').ToList();
            _messageIndex = 0;

            if (_displayMessageCoroutine != null)
            {
                StopCoroutine(_displayMessageCoroutine);
            }

            _displayMessageCoroutine = StartCoroutine(DisplayMessage());
        }

        public bool SkipAnimation()
        {
            if (_isTextAnimating && textMesh.maxVisibleCharacters < _currentMessageTarget.Length)
            {
                textMesh.maxVisibleCharacters = _currentMessageTarget.Length;
                return false;
            }

            NextMessage();
            if (_messageIndex >= _messages.Count)
            {
                StopAnimation();
                return true;
            }

            return false;
        }

        public void StopAnimation()
        {
            ToggleInspectCam(false);

            if (_displayMessageCoroutine != null)
            {
                StopCoroutine(_displayMessageCoroutine);
            }

            textMesh.text = string.Empty;
            textMesh.gameObject.SetActive(false);

            pauseEvent.Raise(false);
        }

        private void ToggleInspectCam(bool active)
        {
            playerCam.gameObject.SetActive(!active);
            inspectCam.gameObject.SetActive(active);
        }

        private void NextMessage()
        {
            _messageIndex++;

            if (_messageIndex < _messages.Count)
            {
                if (_displayMessageCoroutine != null)
                {
                    StopCoroutine(_displayMessageCoroutine);
                }

                _displayMessageCoroutine = StartCoroutine(DisplayMessage());
            }
        }

        private IEnumerator DisplayMessage()
        {
            _isTextAnimating = true;
            _isAtEllipsis = false;

            _currentMessageTarget = _messages[_messageIndex];

            textMesh.text = _currentMessageTarget;
            textMesh.maxVisibleCharacters = 0;

            bool isAddingRichTextTag = false;
            for (int i = 0; i < _currentMessageTarget.Length; i++)
            {
                char letter = _currentMessageTarget[i];
                if (textMesh.maxVisibleCharacters == _currentMessageTarget.Length)
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
            if (_currentMessageTarget.Length - i < 3)
            {
                return false;
            }

            char currentLetter = _currentMessageTarget[i];

            if (currentLetter == '.' && _currentMessageTarget[i + 1] == '.' && _currentMessageTarget[i + 2] == '.')
            {
                _isAtEllipsis = true;
                return true;
            }

            return false;
        }
    }
}