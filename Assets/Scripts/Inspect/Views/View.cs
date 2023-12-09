using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Inspect.Views
{
    public class View : MonoBehaviour
    {
        [SerializeField] private bool blockInteractions;

        [SerializeField] protected CinemachineVirtualCamera vCam;
        // [SerializeField] private bool overrideCameraPriority;
        // [ConditionalField(nameof(overrideCameraPriority))] [SerializeField] private int cameraPriority;

        private Coroutine _coroutine;
        private bool _isActive;

        public Action<View> OnViewOpen;
        public Action<View> OnViewClose;

        public virtual void Initialize()
        {
            if (vCam != null)
            {
                vCam.gameObject.SetActive(false);
            }
        }

        public bool CanBlockInteractions()
        {
            return blockInteractions;
        }

        public void OpenView()
        {
            Open(false);
        }

        internal virtual void Open(bool beingReopened, int camPriority = 0)
        {
            _isActive = true;
            OnViewOpen?.Invoke(this);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(Show());
        }

        public void CloseView()
        {
            Close();
        }

        internal virtual void Close()
        {
            if (!_isActive)
            {
                return;
            }

            OnViewClose?.Invoke(this);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(WaitForHide());
        }

        public bool IsActive()
        {
            return _isActive;
        }

        protected virtual IEnumerator Show()
        {
            vCam.gameObject.SetActive(true);
            yield break;
        }

        protected virtual IEnumerator Hide()
        {
            vCam.gameObject.SetActive(false);
            yield break;
        }

        private IEnumerator WaitForHide()
        {
            yield return Hide();
            _isActive = false;
        }
    }
}