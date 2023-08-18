using System;
using System.Collections;
using Controllers;
using DG.Tweening;
using Events;
using Lights;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items
{
    public class ItemInspector : MonoBehaviour
    {
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

        [Separator("Events")]
        [SerializeField] private BoolEventChannelSO pauseEvent;

        private Sequence _inspectPopup;
        private IItem _currentInspectionItem;
        private Controller _currentController;
        private Action<bool> _currentCallback;

        private bool _isTextAnimating;
        private string _messageTarget;

        private void Awake()
        {
            _inspectPopup = DOTween.Sequence();

            _inspectPopup
                .AppendInterval(fadeDuration)
                .Append(image.DOFade(1.0f, fadeDuration))
                .InsertCallback(fadeDuration * 0.75f,
                    () => { StartCoroutine(DisplayMessage(_currentInspectionItem.GetItemInfo().itemName)); })
                .SetAutoKill(false)
                .SetUpdate(true)
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
            InspectItem(item, null, callback);
        }

        public void InspectItem(IItem item, Controller controller, Action<bool> callback)
        {
            _currentInspectionItem = item;
            _currentController = controller;
            _currentCallback = callback;

            image.sprite = item.GetItemInfo().inspectImage;

            LightControl.OnLightControl?.Invoke(false, fadeDuration);
            _inspectPopup.PlayForward();

            pauseEvent.Raise(true);
        }

        public void ConfirmInspect()
        {
            ClosePopup();
            _currentCallback?.Invoke(true);
        }

        public void CancelInspect()
        {
            ClosePopup();
            _currentCallback?.Invoke(false);
        }

        private void ClosePopup()
        {
            _inspectPopup.Rewind();
            text.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);

            LightControl.OnLightControl?.Invoke(true, fadeDuration);
            pauseEvent.Raise(false);
        }

        private IEnumerator DisplayMessage(string itemName)
        {
            _isTextAnimating = true;
            bool isAddingRichTextTag = false;

            _messageTarget = $"Pick up <b>{itemName}</b>?";

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

            EventSystem.current.SetSelectedGameObject(confirmButton.gameObject);

            confirmButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);

            _isTextAnimating = false;
        }
    }
}