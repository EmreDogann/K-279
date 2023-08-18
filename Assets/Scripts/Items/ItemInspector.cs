using System;
using System.Collections;
using Controllers;
using DG.Tweening;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemInspector : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;

        [Separator("Buttons")]
        [SerializeField] private TextMeshProUGUI confirmButton;
        [SerializeField] private TextMeshProUGUI cancelButton;
        [SerializeField] private Material focusedTextMaterial;
        [SerializeField] private Material unfocusedTextMaterial;

        [Separator("Animation")]
        [SerializeField] private float fadeDuration;
        [Tooltip("Characters to show per second")]
        [SerializeField] private float textAnimationSpeed;

        private Sequence _inspectPopup;
        private IItem _currentInspectionItem;
        private Controller _currentController;

        private bool _isTextAnimating;
        private string _messageTarget;

        private void Awake()
        {
            _inspectPopup = DOTween.Sequence();

            _inspectPopup
                .Append(canvasGroup.DOFade(1.0f, fadeDuration))
                .InsertCallback(fadeDuration * 0.75f,
                    () => { StartCoroutine(DisplayMessage(_currentInspectionItem.GetItemInfo().itemName)); })
                .SetAutoKill(false)
                .Pause();
        }

        private void Update()
        {
            if (_isTextAnimating)
            {
                if (_currentController != null && _currentController.input.RetrieveInteractInput())
                {
                    text.maxVisibleCharacters = _messageTarget.Length;
                }
            }
        }

        public void InspectItem(IItem item, Action<bool> callback)
        {
            _currentController = null;
            _currentInspectionItem = item;
            image.sprite = item.GetItemInfo().inspectImage;
        }

        public void InspectItem(IItem item, Controller controller, Action callback)
        {
            _currentController = controller;
            _currentInspectionItem = item;
            image.sprite = item.GetItemInfo().inspectImage;
        }

        private IEnumerator DisplayMessage(string itemName)
        {
            _isTextAnimating = true;
            bool isAddingRichTextTag = false;

            _messageTarget = $"Pick up <b>{itemName}</b>?";
            text.text = _messageTarget;

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

            _isTextAnimating = false;
        }
    }
}