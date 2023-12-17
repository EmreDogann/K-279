using System;
using Inspect;
using Inspect.Views;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace UI
{
    public class ViewManagerInput : MonoBehaviour
    {
        public static Action<bool> OnCancelEvent;
        private InputAction _cancel;
        private InputSystemUIInputModule _uiInputModule;

        private void Awake()
        {
            _uiInputModule = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<InputSystemUIInputModule>();

            if (_uiInputModule != null)
            {
                _cancel = _uiInputModule.cancel.action;
            }
        }

        private void OnEnable()
        {
            _cancel.started += OnCancel;
        }

        private void OnDisable()
        {
            _cancel.started -= OnCancel;
        }

        public void OnCancel(InputAction.CallbackContext ctx)
        {
            View viewActive = ViewManager.Instance.GetCurrentView();
            OnCancelEvent?.Invoke(viewActive);

            // if (!viewActive)
            // {
            // ViewManager.Instance.Show<PauseMenuView>();
            // }
            // else
            // {
            // ViewManager.Instance.Back();
            // }
        }
    }
}