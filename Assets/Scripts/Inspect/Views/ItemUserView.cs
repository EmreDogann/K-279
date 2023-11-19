using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Inspect.Views
{
    public class ItemUserView : View
    {
        [SerializeField] private float beforeWaitDuration;
        [SerializeField] private float afterWaitDuration;

        private Action<bool> _currentCallback;
        private bool _isShowing;

        public void SetupItemUserView(Action<bool> onCompletionTrigger,
            CinemachineVirtualCamera vCamOverride = null)
        {
            _currentCallback = onCompletionTrigger;

            if (vCamOverride != null)
            {
                vCam = vCamOverride;
            }
        }

        private void Update()
        {
            if (!_isShowing) {}
        }

        private IEnumerator TakeOrGiveAnimation()
        {
            yield return new WaitForSecondsRealtime(beforeWaitDuration);
            _currentCallback?.Invoke(true);
            yield return new WaitForSecondsRealtime(afterWaitDuration);
            ViewManager.Instance.Back();
        }

        protected override IEnumerator Show()
        {
            if (vCam != null)
            {
                vCam.gameObject.SetActive(true);
            }

            StartCoroutine(TakeOrGiveAnimation());
            // Wait one frame so input does not trigger right away.
            yield return null;
            _isShowing = true;
        }

        protected override IEnumerator Hide()
        {
            if (vCam != null)
            {
                vCam.gameObject.SetActive(false);
            }

            _isShowing = false;
            yield break;
        }
    }
}