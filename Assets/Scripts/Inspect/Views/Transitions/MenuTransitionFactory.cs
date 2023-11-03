using System;
using UnityEngine;

namespace Inspect.Views.Transitions
{
    [Serializable]
    public class MenuTransitionFactory
    {
        public enum MenuTransitionType
        {
            Simple,
            Fade,
            Slide,
            SlideFade
        }
        public MenuTransitionType TransitionType = MenuTransitionType.Simple;

        [SerializeField] private SimpleMenuTransition SimpleMenuTransition = new SimpleMenuTransition();
        [SerializeField] private FadeMenuTransition FadeMenuTransition = new FadeMenuTransition();
        [SerializeField] private SlideMenuTransition SlideMenuTransition = new SlideMenuTransition();
        [SerializeField] private SlideFadeMenuTransition SlideFadeMenuTransition = new SlideFadeMenuTransition();

        public MenuTransition CreateTransition()
        {
            return GetTransitionFromType(TransitionType);
        }

        public Type GetClassType(MenuTransitionType menuType)
        {
            return GetTransitionFromType(menuType).GetType();
        }

        private MenuTransition GetCurrentTransition()
        {
            return GetTransitionFromType(TransitionType);
        }

        private MenuTransition GetTransitionFromType(MenuTransitionType type)
        {
            switch (type)
            {
                case MenuTransitionType.Simple:
                    return SimpleMenuTransition;
                case MenuTransitionType.Fade:
                    return FadeMenuTransition;
                case MenuTransitionType.Slide:
                    return SlideMenuTransition;
                case MenuTransitionType.SlideFade:
                    return SlideFadeMenuTransition;
                default:
                    return SimpleMenuTransition;
            }
        }
    }
}