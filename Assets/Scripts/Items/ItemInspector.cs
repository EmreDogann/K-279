using System;
using System.Collections;
using Controllers;
using Events;
using Lights;
using MyBox;
using TMPro;
using UnityEngine;
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

        [Separator("Animation")]
        [SerializeField] private float fadeDuration;
        [Tooltip("Characters to show per second")]
        [SerializeField] private float textAnimationSpeed;

        [Separator("Events")]
        [SerializeField] private BoolEventChannelSO pauseEvent;

        private IItem _currentInspectionItem;
        private Controller _currentController;
        private Action<bool> _currentCallback;

        private bool _isTextAnimating;
        private string _messageTarget;

        private void Awake()
        {
            // Turn on then off to call the object component's Awake functions.
            text.gameObject.SetActive(true);
            text.gameObject.SetActive(false);

            image.gameObject.SetActive(true);
            image.gameObject.SetActive(false);

            // confirmButton.gameObject.SetActive(false);
            // cancelButton.gameObject.SetActive(false);
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
            else
            {
                if (_currentController != null && _currentController.input.RetrieveInteractInput())
                {
                    _currentCallback?.Invoke(true);
                    ClosePopup();
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

            image.gameObject.SetActive(true);
            image.sprite = item.GetItemInfo().inspectImage;

            LightManager.Instance.ToggleLights(false, fadeDuration);
            StartCoroutine(DisplayMessage(_currentInspectionItem.GetItemInfo().itemName));

            pauseEvent.Raise(true);
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
            text.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            // confirmButton.gameObject.SetActive(false);
            // cancelButton.gameObject.SetActive(false);

            _currentInspectionItem = null;
            _currentController = null;
            _currentCallback = null;

            LightManager.Instance.ToggleLights(true, fadeDuration);
            pauseEvent.Raise(false);
        }

        private IEnumerator DisplayMessage(string itemName)
        {
            _isTextAnimating = true;
            bool isAddingRichTextTag = false;

            _messageTarget = $"Picked up <b>{itemName}</b>";

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

            // EventSystem.current.SetSelectedGameObject(confirmButton.gameObject);

            // confirmButton.gameObject.SetActive(true);
            // cancelButton.gameObject.SetActive(true);

            _isTextAnimating = false;
        }
    }
}