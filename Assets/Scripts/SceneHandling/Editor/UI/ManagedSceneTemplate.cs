using SceneHandling.Editor.UI.Controls;
using UnityEditor;
using UnityEngine.UIElements;

namespace SceneHandling.Editor.UI
{
    public class ManagedSceneTemplate
    {
        public ManagedScene managedScene;

        private ManagedSceneField _managedSceneField;
        private Button _deleteButton;
        private bool _isInitialized;

        public ManagedSceneTemplate(ManagedScene scene = null)
        {
            managedScene = scene;
        }

        private void Init(VisualElement element)
        {
            if (_isInitialized)
            {
                UnbindGUI();
            }

            _deleteButton = element.Q<Button>("deleteManagedSceneButton");
            _deleteButton.clicked += OnDeleteButton_Clicked;

            _managedSceneField = element.Q<ManagedSceneField>("managedSceneField");

            _managedSceneField.RegisterValueChangedCallback(OnFieldChanged);

            _isInitialized = true;
        }

        public void CreateGUI(VisualElement element)
        {
            Init(element);

            _managedSceneField.SetEnabled(true);
            if (managedScene)
            {
                // _managedSceneField.Bind(new SerializedObject(managedScene.SceneAsset));
                _managedSceneField.SetValueWithoutNotify(managedScene.SceneAsset);
            }
        }

        private void UnbindGUI()
        {
            _deleteButton.clicked -= OnDeleteButton_Clicked;
            _managedSceneField.UnregisterValueChangedCallback(OnFieldChanged);
        }

        private void OnFieldChanged(ChangeEvent<SceneAsset> evt)
        {
            ManagedScene newManagedScene = SceneManagerAssets.FindManagedAsset(evt.newValue);

            _managedSceneField.SetValueWithoutNotify(evt.newValue);
            managedScene = newManagedScene ? newManagedScene : SceneManagerAssets.Create(evt.newValue);
        }

        private void OnDeleteButton_Clicked()
        {
            SceneManagerAssets.DeleteAsset(managedScene.Guid);
        }

        public void OnDestroy()
        {
            UnbindGUI();
            // SceneManagerAssets.DeleteAsset(managedScene.Guid);
            SceneManagerSettings.Instance.managedScenes.Remove(managedScene);
        }
    }
}