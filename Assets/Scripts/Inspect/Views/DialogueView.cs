using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Inspect.Views
{
    public class DialogueView : View
    {
        // [SerializeField] private bool overrideCameraPriority;
        // [ConditionalField(nameof(overrideCameraPriority))] [SerializeField] private int cameraPriority;
        [SerializeField] private TextAnimatorUpdate textAnimator;

        [TextArea(3, 5)]
        [SerializeField] private string dialogueToDisplay;

        private InputSystemUIInputModule _uiInputModule;
        private bool _isShowing;

        public override void Initialize()
        {
            base.Initialize();
            _uiInputModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();
        }

        private void Update()
        {
            if (!_isShowing)
            {
                return;
            }

            if (_uiInputModule.submit.action.WasPressedThisFrame() ||
                _uiInputModule.leftClick.action.WasPressedThisFrame())
            {
                if (textAnimator.SkipAnimation())
                {
                    ViewManager.Instance.Back();
                }
            }
        }

        protected override IEnumerator Show()
        {
            vCam.gameObject.SetActive(true);
            textAnimator.StartAnimation(dialogueToDisplay);
            // Wait one frame so input does not trigger right away.
            yield return null;
            _isShowing = true;
        }

        protected override IEnumerator Hide()
        {
            vCam.gameObject.SetActive(false);
            textAnimator.StopAnimation();
            _isShowing = false;
            yield break;
        }
    }
}