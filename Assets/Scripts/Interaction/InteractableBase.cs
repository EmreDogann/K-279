using System;
using MyBox;
using UnityEngine;

namespace Interaction
{
    [Serializable]
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [field: Separator("Base Interactable Settings")]
        [field: SerializeField] public float HoldDuration { get; protected set; }
        [field: SerializeField] public bool HoldInteract { get; protected set; }
        // Not used right now
        public float MultipleUse { get; protected set; }
        [field: SerializeField] public bool IsInteractable { get; protected set; }
        [field: SerializeField] public bool IsHoverable { get; protected set; }

        protected float HoldProgress = 0.0f;

        public event Action OnInteracted;

        public virtual void OnStartHover(IInteractor interactor)
        {
            // Debug.Log("Start Hovered: " + gameObject.name);
        }

        public virtual void OnStartInteract(IInteractor interactor)
        {
            OnInteracted?.Invoke();
            // Debug.Log("Start Interacted: " + gameObject.name);
        }

        public virtual void OnInteract(IInteractor interactor)
        {
            // Debug.Log("Interacted: " + gameObject.name);
        }

        public virtual void OnEndInteract(IInteractor interactor)
        {
            // Debug.Log("End Interacted: " + gameObject.name);
        }

        public virtual void OnEndHover(IInteractor interactor)
        {
            // Debug.Log("End Hovered: " + gameObject.name);
        }

        public bool IsHoldInteractFinished()
        {
            return HoldProgress >= HoldDuration;
        }
    }
}