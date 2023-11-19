using Inspect.Views;
using Items;

namespace Interaction
{
    public enum ItemUserInteractionType
    {
        Default,
        GiveItem,
        TakeItem
    }
    public interface IInteractor
    {
        public ItemUserInteractionType ResolveInteraction(IItemUser itemUser, ItemUserView viewOverride = null);
    }
}