namespace Interaction
{
    public interface IInteractable
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        float MultipleUse { get; }
        bool IsInteractable { get; }

        void OnStartHover(IInteractor interactor);

        void OnStartInteract(IInteractor interactor);
        void OnInteract(IInteractor interactor);
        void OnEndInteract(IInteractor interactor);
        void OnEndHover(IInteractor interactor);
    }
}