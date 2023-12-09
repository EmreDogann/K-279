using System.Collections.Generic;
using System.Linq;
using Events;
using Items;
using Lights;
using MyBox;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inspect
{
    // A variant of 'TextAnimator' which uses the Update() method rather than coroutines.
    public class ItemTextAnimator : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI textMesh;

        [Separator("Animation")]
        [SerializeField] private float fadeDuration;
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
        private int _letterIndex;
        private bool _isAddingRichTextTag;

        private readonly char[] _punctuation = { ',', '.', '-', '?', '!' };

        private IItem _currentItem;

        private float _currentTime;
        private float _waitTime;

        private void Start()
        {
            textMesh.gameObject.SetActive(true);
            textMesh.gameObject.SetActive(false);

            itemImage.gameObject.SetActive(true);
            itemImage.gameObject.SetActive(false);
        }

        public void SetMessage(string message)
        {
            _messages.Clear();
            _messages = message.Split('\n').ToList();
            _messageIndex = -1;
        }

        public void StartAnimation(ItemInfoSO itemInfo)
        {
            pauseEvent.Raise(true);

            textMesh.gameObject.SetActive(true);

            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemInfo.inspectImage;

            SetMessage($"Picked up <b>{itemInfo.itemName}</b>");

            NextMessage();
            LightManager.Instance.ToggleLights(false, fadeDuration);
        }

        public bool SkipAnimation()
        {
            // Checking if less than total length allows skipping if all characters are shown but it's still animating
            // (e.g. waiting on last punctuation waitTime).
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
            textMesh.text = string.Empty;
            textMesh.gameObject.SetActive(false);

            itemImage.gameObject.SetActive(false);

            LightManager.Instance.ToggleLights(true, fadeDuration);

            pauseEvent.Raise(false);
        }

        private void NextMessage()
        {
            _messageIndex++;
            _letterIndex = 0;

            if (_messageIndex < _messages.Count)
            {
                _currentMessageTarget = _messages[_messageIndex];

                textMesh.text = _currentMessageTarget;
                textMesh.maxVisibleCharacters = 0;
                _isTextAnimating = true;
                EvaluateWaitTime();
            }
            else
            {
                _isTextAnimating = false;
            }
        }

        private void Update()
        {
            if (!_isTextAnimating)
            {
                return;
            }

            _currentTime += Time.unscaledDeltaTime;
            if (_currentTime >= _waitTime)
            {
                _currentTime = 0.0f;
                textMesh.maxVisibleCharacters++;
                if (textMesh.maxVisibleCharacters >= _currentMessageTarget.Length)
                {
                    _isTextAnimating = false;
                    return;
                }

                _letterIndex++;
                EvaluateWaitTime();
            }
        }

        private void EvaluateWaitTime()
        {
            char letter = _currentMessageTarget[_letterIndex];
            if (letter == '<' || _isAddingRichTextTag)
            {
                _isAddingRichTextTag = true;
                if (letter == '>')
                {
                    _isAddingRichTextTag = false;
                }
            }
            else
            {
                if (!_isAtEllipsis && !CheckForEllipsis(_letterIndex))
                {
                    if (_punctuation.Contains(letter))
                    {
                        _waitTime = punctuationWaitTime;
                    }
                }

                if (letter != '.')
                {
                    _isAtEllipsis = false;
                }

                _waitTime =
                    1 / (_isAtEllipsis ? ellipsisSpeed : textAnimationSpeed);
            }
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