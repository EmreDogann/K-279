using System.Collections;
using Cinemachine;
using Inspect.Views.Triggers;
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

        private void OnEnable()
        {
            DialogueViewTrigger.TriggerDialogueView += OnTriggerDialogueView;
        }

        private void OnDisable()
        {
            DialogueViewTrigger.TriggerDialogueView -= OnTriggerDialogueView;
        }

        private void OnTriggerDialogueView(CinemachineVirtualCamera vCamNew, string messageToDisplay)
        {
            vCam = vCamNew;
            textAnimator.SetMessage(messageToDisplay);
            ViewManager.Instance.Show(this);
        }

        protected override IEnumerator Show()
        {
            if (vCam != null)
            {
                vCam.gameObject.SetActive(true);
            }

            textAnimator.StartAnimation();
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

            textAnimator.StopAnimation();
            _isShowing = false;
            yield break;
        }
    }
}