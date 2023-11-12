using Inspect.Views;
using MyBox;
using UnityEngine;

namespace Inspect
{
    public class ViewTrigger : MonoBehaviour
    {
        [SerializeField] private View view;

        [ButtonMethod]
        private void ShowView()
        {
            ViewManager.Instance.Show(view);
        }

        [ButtonMethod]
        private void CloseView()
        {
            ViewManager.Instance.Back();
        }
    }
}