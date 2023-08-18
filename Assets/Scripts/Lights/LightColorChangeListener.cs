using UnityEngine;

namespace Lights
{
    public abstract class LightColorChangeListener : MonoBehaviour
    {
        public virtual void OnEnable()
        {
            LightManager.OnChangeColor += OnChangeColor;
        }

        public virtual void OnDisable()
        {
            LightManager.OnChangeColor -= OnChangeColor;
        }

        public abstract void OnChangeColor(LightData data);
    }
}