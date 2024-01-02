using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Utils.ObservableCollections;

namespace SceneHandling.Editor.UI
{
    public class SceneManagerEditorWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset rootView;
        [SerializeField] private VisualTreeAsset sceneTemplate;

        private ListView _sceneAssetListView;
        private ListView _managedScenesListView;
        private ToolbarButton _saveButton;
        private ToolbarButton _loadButton;
        private ToolbarButton _clearButton;
        private ToolbarButton _initButton;

        private Button _openSettingsExplorerButton;
        private Button _openSettingsFileButton;

        private Button _removeListItemsButton;
        private Button _printSceneListSizeButton;

        private List<ManagedSceneTemplate> _sceneTemplatesList;

        internal static SceneManagerEditorWindow Instance { get; private set; }

        [MenuItem("Tools/Scene Manager/Open Scene Manager")]
        private static void ShowWindow()
        {
            SceneManagerEditorWindow window = GetWindow<SceneManagerEditorWindow>();
            window.titleContent = new GUIContent("Scene Manager");
        }

        private void CreateGUI()
        {
            Instance = this;

            TemplateContainer template = rootView.Instantiate();
            template.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
            rootVisualElement.Add(template);

            _sceneAssetListView = rootVisualElement.Q<ListView>("sceneAssetList");
            _sceneAssetListView.makeItem += () => new ObjectField
            {
                allowSceneObjects = false, objectType = typeof(SceneAsset), style = { paddingBottom = 4 }
            };
            _sceneAssetListView.itemsSource = SceneManagerSettings.Instance.scenes.GetList();
            _sceneAssetListView.bindItem += (element, i) =>
            {
                ((ObjectField)element).value = SceneManagerSettings.Instance.scenes[i];
                element.Q(className: "unity-object-field__input").SetEnabled(false);
            };

            SceneManagerSettings.Instance.scenes.CollectionChanged += ScenesList_CollectionChanged;

            _sceneTemplatesList =
                SceneManagerSettings.Instance.managedScenes?.Select((scene, _) => new ManagedSceneTemplate(scene))
                    ?.ToList() ?? new List<ManagedSceneTemplate>();

            _managedScenesListView = rootVisualElement.Q<ListView>("managedSceneList");
            _managedScenesListView.makeItem = sceneTemplate.Instantiate;
            _managedScenesListView.itemsSource = _sceneTemplatesList;
            _managedScenesListView.bindItem = (element, i) =>
            {
                if (_sceneTemplatesList.ElementAtOrDefault(i) is not ManagedSceneTemplate managedSceneTemplate)
                {
                    managedSceneTemplate = new ManagedSceneTemplate();
                    _sceneTemplatesList[i] = managedSceneTemplate;
                }

                managedSceneTemplate.CreateGUI(element);
            };
            _managedScenesListView.itemsRemoved += indices =>
            {
                foreach (int index in indices)
                {
                    if (_sceneTemplatesList[index] is ManagedSceneTemplate managedSceneTemplate)
                    {
                        managedSceneTemplate.OnDestroy();
                    }
                }

                // EditorApplication.delayCall += () => _managedScenesListView.Rebuild();
            };

            SceneManagerSettings.Instance.managedScenes.CollectionChanged += ManagedScenesList_CollectionChanged;

            #region Toolbar Buttons

            _saveButton = rootVisualElement.Q<ToolbarButton>("saveSettings");
            _loadButton = rootVisualElement.Q<ToolbarButton>("reloadSettings");
            _clearButton = rootVisualElement.Q<ToolbarButton>("clearSettings");
            _initButton = rootVisualElement.Q<ToolbarButton>("initSettings");

            _saveButton.clicked += () =>
            {
                SceneManagerSettings.Instance.Save();

                ReloadLists();
            };
            _loadButton.clicked += () =>
            {
                SceneManagerSettings.Instance.Load();

                ReloadLists();
            };
            _clearButton.clicked += () =>
            {
                SceneManagerSettings.Instance.Create();

                _sceneAssetListView.itemsSource = new List<SceneAsset>();
                _sceneAssetListView.Rebuild();
            };
            _initButton.clicked += () =>
            {
                SceneManagerSettings.Instance.ResetState();
                SceneManagerSettings.Instance.scenes = new ObservableList<SceneAsset>(AssetDatabase
                    .FindAssets("t:SceneAsset")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<SceneAsset>).ToList());
                SceneManagerSettings.Instance.Save();

                ReloadLists();
            };

            _openSettingsExplorerButton = rootVisualElement.Q<Button>("openSettingsExplorer");
            _openSettingsExplorerButton.clicked += () =>
            {
                EditorUtility.RevealInFinder(SceneManagerSettings.Instance.TryGetFilePathDirectory() + "/");
            };

            _openSettingsFileButton = rootVisualElement.Q<Button>("openSettingsFile");
            _openSettingsFileButton.clicked += () =>
            {
                Application.OpenURL(SceneManagerSettings.Instance.TryGetFilePath(true));
            };

            _removeListItemsButton = rootVisualElement.Q<Button>("removeListItems");
            _removeListItemsButton.clicked += () =>
            {
                SceneManagerSettings.Instance.scenes.RemoveRange(0, 10);
                SceneManagerSettings.Instance.Save();

                ReloadLists();
            };

            _printSceneListSizeButton = rootVisualElement.Q<Button>("printSceneListSize");
            _printSceneListSizeButton.clicked += () =>
            {
                Debug.Log($"Scene List Size {SceneManagerSettings.Instance.scenes.Count}");
            };

            #endregion
        }

        private void ReloadLists()
        {
            // _sceneTemplatesList =
            //     SceneManagerSettings.Instance.managedScenes?.Select((scene, i) => new ManagedSceneTemplate(scene, i))
            //         .ToList() ?? new List<ManagedSceneTemplate>();
            // _managedScenesListView.itemsSource = _sceneTemplatesList;
            _managedScenesListView.Rebuild();

            _sceneAssetListView.itemsSource = SceneManagerSettings.Instance.scenes.GetList();
            _sceneAssetListView.Rebuild();
        }

        private void ScenesList_CollectionChanged(in NotifyCollectionChangedEventArgs<SceneAsset> evt)
        {
            _sceneAssetListView.itemsSource = SceneManagerSettings.Instance.scenes.GetList();
            _sceneAssetListView.Rebuild();
        }

        private void ManagedScenesList_CollectionChanged(in NotifyCollectionChangedEventArgs<ManagedScene> evt)
        {
            if (evt.Action == NotifyCollectionChangedAction.Remove)
            {
                Debug.Log($"Item removed from ManagedScenes: {evt.OldItem}");
                EditorApplication.delayCall += () => _managedScenesListView.Rebuild();
            }
            else if (evt.Action == NotifyCollectionChangedAction.Add)
            {
                if (evt.IsSingleItem)
                {
                    int index = evt.NewStartingIndex;
                    EditorApplication.delayCall += () => _managedScenesListView.RefreshItem(index);
                }
            }
        }

        private void OnDestroy()
        {
            SceneManagerSettings.Instance.scenes.CollectionChanged -= ScenesList_CollectionChanged;
            SceneManagerSettings.Instance.managedScenes.CollectionChanged -= ManagedScenesList_CollectionChanged;
        }
    }
}