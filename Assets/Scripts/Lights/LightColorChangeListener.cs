using UnityEngine;

namespace Lights
{
    public abstract class LightColorChangeListener : MonoBehaviour
    {
        public virtual void Awake()
        {
            LightManager.OnChangeColor += OnChangeColor;
        }

        public virtual void OnDestroy()
        {
            LightManager.OnChangeColor -= OnChangeColor;
        }

        public abstract void OnChangeColor(LightData data);
    }
}