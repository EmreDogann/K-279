using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// From: https://forum.unity.com/threads/solved-but-unhappy-scriptableobject-awake-never-execute.488468/#post-4483018
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public abstract class ManagedScriptableObject : ScriptableObject
{
    protected abstract void OnBegin();
    protected abstract void OnEnd();

#if UNITY_EDITOR
    protected virtual void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayStateChange;
    }

    protected virtual void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayStateChange;
    }

    private void OnPlayStateChange(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            OnBegin();
        }
        else if (state == PlayModeStateChange.ExitingPlayMode)
        {
            OnEnd();
        }
    }
#else
        protected virtual void OnEnable()
        {
            OnBegin();
        }
 
        protected virtual void OnDisable()
        {
            OnEnd();
        }
#endif
}