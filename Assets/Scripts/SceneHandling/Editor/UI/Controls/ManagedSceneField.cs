using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace SceneHandling.Editor.UI.Controls
{
    internal class CallbackToken<T> : IDisposable
    {
        public INotifyValueChanged<T> Element;
        public EventCallback<ChangeEvent<T>> ChangeEvent;
        public int CallbackHashCode;

        public void Dispose()
        {
            Element.UnregisterValueChangedCallback(ChangeEvent);
        }
    }

    public class ManagedSceneField : ObjectField, INotifyValueChanged<SceneAsset>
    {
        private readonly List<CallbackToken<Object>> _registeredCallbacksTokens =
            new List<CallbackToken<Object>>();

        public new SceneAsset value
        {
            get => base.value as SceneAsset;
            set => base.value = value;
        }

        public override void SetValueWithoutNotify(Object newValue)
        {
            base.SetValueWithoutNotify(newValue);
            // Debug.Log($"Value changed to: {newValue}");
        }

        public void SetValueWithoutNotify(SceneAsset newValue)
        {
            base.SetValueWithoutNotify(newValue);
        }

        public ManagedSceneField()
        {
            allowSceneObjects = false;
            objectType = typeof(SceneAsset);
        }

        public void RegisterValueChangedCallback(EventCallback<ChangeEvent<SceneAsset>> callback)
        {
            EventCallback<ChangeEvent<Object>> changeEvent = evt =>
                callback.Invoke(ChangeEvent<SceneAsset>.GetPooled(evt.previousValue as SceneAsset,
                    evt.newValue as SceneAsset));

            this.RegisterValueChangedCallback(changeEvent);
            _registeredCallbacksTokens.Add(new CallbackToken<Object>
                { Element = this, ChangeEvent = changeEvent, CallbackHashCode = callback.GetHashCode() });
        }

        public void UnregisterValueChangedCallback(EventCallback<ChangeEvent<SceneAsset>> callback)
        {
            int tokenIndex =
                _registeredCallbacksTokens.FindIndex(token => callback.GetHashCode() == token.CallbackHashCode);
            if (tokenIndex != -1)
            {
                _registeredCallbacksTokens[tokenIndex].Dispose();
                _registeredCallbacksTokens.RemoveAt(tokenIndex);
            }
        }

        public new class UxmlFactory : UxmlFactory<ManagedSceneField, UxmlTraits>
        {
            public override string uxmlNamespace => "SceneManager";
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription m_propertyPath = new UxmlStringAttributeDescription
                { name = "Binding-path" };
            private readonly UxmlStringAttributeDescription m_label = new UxmlStringAttributeDescription
                { name = "Label" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is ManagedSceneField field)
                {
                    field.label = m_label.GetValueFromBag(bag, cc);
                    field.bindingPath = m_propertyPath.GetValueFromBag(bag, cc);
                }
            }
        }
    }
}