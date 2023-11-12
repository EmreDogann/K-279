using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Interaction
{
    public class MouseCursorGraphic : MonoBehaviour
    {
        [SerializeField] private CursorLockMode cursorLockMode;
        [SerializeField] private bool cursorVisible;

        [SerializeField] private Image cursorImage;

        [Range(0.0f, 1.0f)] [SerializeField] private float nonInteractableFadeValue;
        [Range(0.0f, 5.0f)] [SerializeField] private float nonInteractableFadeDuration;

        private float _originalAlpha;

        private void Awake()
        {
            _originalAlpha = cursorImage.color.a;

            Cursor.lockState = cursorLockMode;
            Cursor.visible = cursorVisible;
        }

        private void Update()
        {
            Vector3 mousePosSS = Mouse.current.position.value;
            // mousePosSS.z = Mathf.Abs(_main.transform.position.z - _currentZValue);
            //
            // Vector3 mousePosWS = _main.ScreenToWorldPoint(mousePosSS);
            //
            // Debug.Log(mousePosWS);
            //
            // spriteMouseCursor.transform.position = mousePosWS;

            // Plane plane = new Plane(-_main.transform.forward, _currentZValue);
            // Ray ray = _main.ScreenPointToRay(Mouse.current.position.ReadValue());
            // spriteMouseCursor.transform.position = ray.GetPoint(Mathf.Abs(_main.transform.position.z - _currentZValue));

            cursorImage.rectTransform.anchoredPosition = mousePosSS;
            // mousePosSS.z = Mathf.Abs(_main.transform.position.z - playerCollider.transform.position.z);
            //
            // Vector3 cursorPos = _main.ScreenToWorldPoint(mousePosSS);
            // Vector2 playerPos = playerCollider.ClosestPointOnBounds(cursorPos);
            // float distance = Vector2.Distance(playerPos, cursorPos);

            // if (distance > _interactionRange && _interactableState)
            // {
            //     cursorImage.DOKill();
            //     cursorImage.DOFade(nonInteractableFadeValue, nonInteractableFadeDuration);
            //     _interactableState = false;
            // }
            // else if (distance < _interactionRange && !_interactableState)
            // {
            //     cursorImage.DOKill();
            //     cursorImage.DOFade(_originalAlpha, nonInteractableFadeDuration);
            //     _interactableState = true;
            // }
        }

        public void SetInteractState(bool canInteract)
        {
            if (!canInteract)
            {
                cursorImage.DOKill();
                cursorImage.DOFade(nonInteractableFadeValue, nonInteractableFadeDuration);
            }
            else
            {
                cursorImage.DOKill();
                cursorImage.DOFade(_originalAlpha, nonInteractableFadeDuration);
            }
        }
    }
}