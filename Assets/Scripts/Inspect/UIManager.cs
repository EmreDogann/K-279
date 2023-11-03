using System;
using System.Collections.Generic;
using Inspect.Views;
using UnityEngine;

namespace Inspect
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private View startingView;
        [SerializeField] private bool lockStartingView;

        private readonly Stack<View> _history = new Stack<View>();

        private View _currentView;
        private View[] _views;

        public static UIManager Instance { get; private set; }

        public static Action<View, View> OnViewSwap;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _views = FindObjectsOfType(typeof(View), true) as View[];

            if (_views == null)
            {
                Debug.LogWarning("Warning - View Manager: No views in the scene have been found!");
                return;
            }

            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].Initialize();
            }

            if (startingView != null)
            {
                Show(startingView);
            }
        }

        private void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public bool IsOnlyView()
        {
            return Instance._history.Count == 0;
        }

        public bool IsStartingView(View view)
        {
            return view == startingView;
        }

        public View GetCurrentView()
        {
            return Instance._currentView;
        }

        public T GetView<T>() where T : View
        {
            for (int i = 0; i < Instance._views.Length; i++)
            {
                if (Instance._views[i] is T tView)
                {
                    return tView;
                }
            }

            return null;
        }


        public void Show<T>(bool remember = true) where T : View
        {
            for (int i = 0; i < Instance._views.Length; i++)
            {
                if (Instance._views[i] is not T)
                {
                    continue;
                }

                Show(Instance._views[i], remember);
            }
        }

        // For use with UnityEvents inspector.
        public void Show(View view)
        {
            Show(view, true);
        }

        // For use with UnityEvents inspector.
        public void ShowNoRemember(View view)
        {
            Show(view, false);
        }

        public void Show(View view, bool remember = true)
        {
            if (Instance._currentView != null)
            {
                if (remember)
                {
                    Instance._history.Push(Instance._currentView);
                }

                if (!Instance._currentView.ShouldAlwaysShow())
                {
                    Instance._currentView.Close();
                }
            }

            OnViewSwap?.Invoke(Instance._currentView, view);
            view.Open(false);

            Instance._currentView = view;
        }

        public void Back()
        {
            if (IsStartingView(Instance._currentView) && lockStartingView)
            {
                return;
            }

            if (IsOnlyView())
            {
                Instance._currentView.Close();
                Instance._currentView = null;
                return;
            }

            Instance._currentView.Close();
            OnViewSwap?.Invoke(Instance._currentView, Instance._history.Peek());
            Instance._currentView = Instance._history.Pop();

            if (!Instance._currentView.ShouldAlwaysShow())
            {
                Instance._currentView.Open(true);
            }
        }
    }
}