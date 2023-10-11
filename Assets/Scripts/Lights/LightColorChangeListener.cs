using UnityEngine;

namespace Lights
{
    public abstract class LightColorChangeListener : MonoBehaviour
    {
        public virtual void Awake()
        {
            LightManager.OnChangeState += OnChangeColor;
        }

        public virtual void OnDestroy()
        {
            LightManager.OnChangeState -= OnChangeColor;
        }

        public abstract void OnChangeColor(LightData data);
    }
}