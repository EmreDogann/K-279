using Controllers;
using Interactables;
using UnityEngine;
using Utils;

namespace Capabilities
{
    [RequireComponent(typeof(Controller))]
    public class Interact : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayerMask;

        private Controller _controller;

        private void Awake()
        {
            _controller = GetComponent<Controller>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            collision.gameObject.GetComponent<IInteractableObjects>().InteractionStart();
        }

        private void OnTriggerStay(Collider collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            bool isInteractKeyDown = _controller.input.RetrieveInteractInput();
            collision.gameObject.GetComponent<IInteractableObjects>().InteractionContinues(isInteractKeyDown);
        }

        private void OnTriggerExit(Collider collision)
        {
            if (!interactableLayerMask.Contains(collision.gameObject.layer))
            {
                return;
            }

            collision.gameObject.GetComponent<IInteractableObjects>().InteractionEnd();
        }
    }
}