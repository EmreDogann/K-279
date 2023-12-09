using UnityEngine;

namespace Inspect.Views
{
    public class ViewLink : View
    {
        [SerializeField] private View[] linkedViews;

        public override void Initialize()
        {
            base.Initialize();

            foreach (View view in linkedViews)
            {
                view.OnViewOpen += ViewOpened;
                view.OnViewClose += ViewClosed;
            }

            ViewManager.OnViewSwap += ViewShown;
        }

        public void OnDestroy()
        {
            foreach (View view in linkedViews)
            {
                view.OnViewOpen -= ViewOpened;
                view.OnViewClose -= ViewClosed;
            }

            ViewManager.OnViewSwap -= ViewShown;
        }

        public void ViewOpened(View viewToOpen)
        {
            if (!IsActive())
            {
                Open(false);
            }
        }

        public void ViewClosed(View viewToClose)
        {
            if (ViewManager.Instance.IsOnlyView())
            {
                Close();
            }
        }

        public void ViewShown(View viewToClose, View viewToOpen)
        {
            bool keepOpen = true;
            foreach (View view in linkedViews)
            {
                if (view == viewToOpen)
                {
                    keepOpen = true;
                    break;
                }

                if (view != viewToClose && !view.IsActive())
                {
                    keepOpen = false;
                }
            }

            if (!keepOpen)
            {
                Close();
            }
        }
    }
}