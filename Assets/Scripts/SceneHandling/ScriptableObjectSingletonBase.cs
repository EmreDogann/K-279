using UnityEngine;

namespace SceneHandling
{
    public abstract class ScriptableObjectSingletonBase : ScriptableObject
    {
        public bool ShouldPersist { get; protected set; }
        public bool IncludeInBuild { get; protected set; }

        protected bool UseInBuild => ShouldPersist && IncludeInBuild;
        protected virtual void OnEnable() {}

        /// <summary>
        ///     Load method invoked when the Scriptable Manager is loaded in memory
        /// </summary>
        protected virtual void OnLoad() {}
    }
}