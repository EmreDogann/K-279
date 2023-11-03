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
            UIManager.Instance.Show(view);
        }

        [ButtonMethod]
        private void CloseView()
        {
            UIManager.Instance.Back();
        }
    }
}