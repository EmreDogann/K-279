using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using SceneHandling.Editor.Toolbox;
using SceneHandling.Utility;
using UnityEditor;
using UnityEngine;
using Utils.Reflection;
using Object = UnityEngine.Object;

namespace SceneHandling.Editor.UI
{
    [CustomPropertyDrawer(typeof(ManagedScene))] [CanEditMultipleObjects]
    public class ManagedScenePropertyDrawer : PropertyDrawer
    {
        private static readonly int ObjectFieldHash = "s_ObjectFieldHash".GetHashCode();

        private readonly Func<SerializedProperty, bool> _propertyIsScriptGetter;
        private readonly Func<object, int> _lastUsedControlIDGetter;
        private readonly Func<object, int> _objectSelectorInstanceIDGetter;
        private SerializedObject _sceneAssetSObject;
        private bool _cancelTriggered;
        private SerializedProperty _sceneAssetProperty;
        private SerializedProperty _guidProperty;

        private readonly Func<ManagedScene, string> _managedScenePathGetter;

        private enum SceneBundlingState
        {
            NoScene = 0,
            Nowhere = 1,
            InBuildDisabled = 2,
            InBuildEnabled = 3
        }
        private SceneBundlingState _bundlingState;
        private EditorBuildSettingsScene _buildEntry;

        public ManagedScenePropertyDrawer()
        {
            Type objectSelectorType = typeof(EditorGUI).Assembly.GetType("UnityEditor.ObjectSelector");

            _propertyIsScriptGetter =
                FastInvoke.BuildUntypedGetter<SerializedProperty, bool>(
                    typeof(SerializedProperty).GetProperty("isScript", BindingFlags.Instance | BindingFlags.NonPublic));
            _lastUsedControlIDGetter =
                FastInvoke.BuildStaticMemberGetter<int>(typeof(EditorGUIUtility), "s_LastControlID");
            _objectSelectorInstanceIDGetter =
                FastInvoke.BuildMethodGetterStaticInstance<int>(objectSelectorType, "get", "GetInstanceID");

            _managedScenePathGetter = FastInvoke.BuildUntypedGetter<ManagedScene, string>(
                typeof(ManagedScene).GetProperty(nameof(ManagedScene.ScenePath),
                    BindingFlags.Instance | BindingFlags.Public));
        }

        private void Init(SerializedProperty property)
        {
            _sceneAssetProperty = property.FindPropertyRelative("sceneAsset");
            _guidProperty = property.FindPropertyRelative("guid");

            if (_sceneAssetProperty == null)
            {
                _bundlingState = SceneBundlingState.NoScene;
            }
            else if (_buildEntry == null)
            {
                _bundlingState = SceneBundlingState.Nowhere;
            }
            else if (!_buildEntry.enabled)
            {
                _bundlingState = SceneBundlingState.InBuildDisabled;
            }
            else
            {
                _bundlingState = SceneBundlingState.InBuildEnabled;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                EditorGUI.BeginProperty(position, label, property);
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                GUIStyle style = EditorStyles.label;
                style.fontSize = EditorStyles.miniLabel.fontSize;
                EditorGUI.LabelField(position,
                    new GUIContent("[SerializeReference] is not supported, please use [SerializeField]"),
                    style);
                EditorGUI.EndProperty();
                return;
            }

            Init(property);

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            const float buildSettingsWidth = 20f;
            const float padding = 2f;

            Rect assetPos = position;
            assetPos.width -= buildSettingsWidth + padding;

            Rect buildSettingsPos = position;
            buildSettingsPos.x += position.width - buildSettingsWidth + padding;
            buildSettingsPos.width = buildSettingsWidth;

            // From: https://discussions.unity.com/t/how-can-i-get-the-last-assigned-controlid/55746/2
            // To my understanding, GetControlID just increments by 1 everytime you call it.
            // int objectFieldID = GUIUtility.GetControlID(ObjectFieldHash, FocusType.Keyboard, position) + 1;

            EditorGUI.BeginChangeCheck();
            Object objectRef = EditorGUI.ObjectField(
                _sceneAssetProperty.objectReferenceValue == null || property.hasMultipleDifferentValues
                    ? position
                    : assetPos,
                new GUIContent(),
                _sceneAssetProperty.objectReferenceValue as SceneAsset,
                typeof(SceneAsset), false);

            if (EditorGUI.EndChangeCheck())
            {
                _sceneAssetProperty.objectReferenceValue = objectRef;

                if (objectRef == null)
                {
                    _guidProperty.stringValue = GuidUtils.AllZeroGuid;
                }
                else
                {
                    string newPath = AssetDatabase.GetAssetPath(objectRef);
                    GUID newGuid = AssetDatabase.GUIDFromAssetPath(newPath);
                    _guidProperty.stringValue = newGuid.ToString();

                    SceneAsset sceneAsset = objectRef as SceneAsset;
                    ManagedScene asset = SceneManagerAssets.FindManagedAsset(sceneAsset);
                    if (asset)
                    {
                        property.boxedValue = asset;
                    }
                    else
                    {
                        ManagedScene managedScene = SceneManagerAssets.Create(sceneAsset);

                        property.boxedValue = managedScene;
                    }
                }
            }

            // Event evt = Event.current;
            // EventType eventType = evt.type;
            //
            // bool evaluateObjRef = false;
            // switch (eventType)
            // {
            //     case EventType.ExecuteCommand:
            //         if (evt.commandName.Equals("ObjectSelectorClosed") &&
            //             _lastUsedControlIDGetter(null) == objectFieldID &&
            //             // GUIUtility.keyboardControl == objectFieldID &&
            //             (property == null ||
            //              !_propertyIsScriptGetter(property)))
            //         {
            //             if (_objectSelectorInstanceIDGetter(null) != 0)
            //             {
            //                 evt.Use();
            //
            //                 if (!_cancelTriggered)
            //                 {
            //                     evaluateObjRef = true;
            //                 }
            //
            //                 DestroySerializedObject(ref _sceneAssetSObject);
            //                 _cancelTriggered = false;
            //             }
            //         }
            //
            //         if (evt.commandName.Equals("ObjectSelectorCanceled") &&
            //             _lastUsedControlIDGetter(null) == objectFieldID &&
            //             GUIUtility.keyboardControl == objectFieldID &&
            //             (property == null ||
            //              !_propertyIsScriptGetter(property)))
            //         {
            //             if (_objectSelectorInstanceIDGetter(null) != 0)
            //             {
            //                 // User canceled object selection; don't apply
            //                 evt.Use();
            //
            //                 _cancelTriggered = true;
            //             }
            //         }
            //
            //         if (evt.commandName.Equals("ObjectSelectorSelectionDone") &&
            //             _lastUsedControlIDGetter(null) == objectFieldID &&
            //             GUIUtility.keyboardControl == objectFieldID)
            //         {
            //             evt.Use();
            //
            //             evaluateObjRef = true;
            //             DestroySerializedObject(ref _sceneAssetSObject);
            //         }
            //
            //         break;
            //     case EventType.Used:
            //         if (evt.commandName.Equals("ObjectSelectorUpdated") &&
            //             _lastUsedControlIDGetter(null) == objectFieldID &&
            //             GUIUtility.keyboardControl == objectFieldID)
            //         {
            //             evt.Use();
            //
            //             DestroySerializedObject(ref _sceneAssetSObject);
            //             _sceneAssetSObject = new SerializedObject(objectRef);
            //             _sceneAssetSObject.ApplyModifiedProperties();
            //         }
            //
            //         break;
            // }

            if (_sceneAssetProperty.objectReferenceValue != null && !property.hasMultipleDifferentValues)
            {
                Color colorToRestore = GUI.color;
                GUIContent settingsIcon = EditorGUIUtility.IconContent("SettingsIcon");

// Backwards compatibility (https://github.com/starikcetin/Eflatun.SceneReference/issues/74)
#if UNITY_2022_1_OR_NEWER
                GUIStyle toolboxButtonStyle = EditorStyles.iconButton;
#else
                var toolboxButtonStyle = EditorStyles.miniButton;
                toolboxButtonStyle.padding = new RectOffset(1, 1, 1, 1);
#endif

                bool toolboxButton = GUI.Button(buildSettingsPos, settingsIcon, toolboxButtonStyle);
                if (toolboxButton)
                {
                    ToolboxPopupWindow toolboxPopupWindow = CreateToolboxPopupWindow(property);
                    PopupWindow.Show(buildSettingsPos, toolboxPopupWindow);
                }

                GUI.color = colorToRestore;
            }

            EditorGUI.EndProperty();
            property.serializedObject.ApplyModifiedProperties();
        }

        // TODO - UI Toolkit implementation...
        // public override VisualElement CreatePropertyGUI(SerializedProperty property)
        // {
        //     // Create a new VisualElement to be the root the property UI
        //     VisualElement container = new VisualElement();
        //
        //     ObjectField objectField = new ObjectField(property.displayName);
        //
        //     container.Add(objectField);
        //
        //     return container;
        // }

        private ToolboxPopupWindow CreateToolboxPopupWindow(SerializedProperty property)
        {
            // From: https://discussions.unity.com/t/serializedproperty-findpropertyrelative-returns-null-with-scriptableobjects/82576/5
            // We need to do this because, for a ScriptableObject (SO), the serialized property only goes as far as the reference to the ScriptableObject
            // itself. So we need to create a new SerializedObject to the SO and find the property using that object.
            // SerializedObject soObj = new SerializedObject(property.objectReferenceValue as ManagedScene);

            // SerializedProperty sceneAssetProperty = soObj.FindProperty("sceneAsset");
            // SerializedProperty pathProperty = soObj.FindAutoProperty("OriginalPath");
            Object asset = _sceneAssetProperty.objectReferenceValue;
            string path = _managedScenePathGetter(property.boxedValue as ManagedScene);

            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(_sceneAssetProperty.objectReferenceValue,
                out string guid,
                out long _);

            var tools = new List<ITool>();

            if (_bundlingState == SceneBundlingState.Nowhere)
            {
                tools.Add(new AddToBuildTool(path, asset));
            }

            if (_bundlingState == SceneBundlingState.InBuildDisabled)
            {
                tools.Add(new EnableInBuildTool(path, guid, asset));
            }

            return new ToolboxPopupWindow(tools);
        }

        private bool IsAssetType<T>(Object obj) where T : class
        {
            return obj is T;
        }

        private bool IsAssetType<T>(object obj) where T : class
        {
            return obj is T;
        }

        private void DestroySerializedObject([CanBeNull] ref SerializedObject serializedObject)
        {
            serializedObject?.Dispose();
            serializedObject = null;
        }
    }
}