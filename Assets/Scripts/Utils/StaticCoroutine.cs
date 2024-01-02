using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
#endif

namespace Utils
{
    public static class StaticCoroutine
    {
        private static StaticCoroutineRunner _runner;

        public static Coroutine Run(IEnumerator coroutine)
        {
            EnsureRunner();
            return _runner.StartCoroutine(coroutine);
        }

        public static void RunCallback(Action callback, Func<bool> condition, float? after = null,
            bool nextFrame = false)
        {
            Coroutine()?.StartStaticCoroutine();

            IEnumerator Coroutine()
            {
                if (after.HasValue)
                {
                    yield return new WaitForSeconds(after.Value);
                }
                else if (nextFrame)
                {
                    yield return null;
                }
                else if (condition != null && !condition.Invoke())
                {
                    yield return null;
                }

                callback?.Invoke();
            }
        }

        public static void StartStaticCoroutine(this IEnumerator coroutine)
        {
            if (coroutine == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                EnsureRunner();
                _runner.StartCoroutine(coroutine);
            }
            else
            {
                //If com.unity.editorcoroutines is installed, then we'll use that to provide editor functionality
#if UNITY_EDITOR
                EditorCoroutineUtility.StartCoroutineOwnerless(coroutine);
#endif

                // Type type = Type.GetType(
                //     "Unity.EditorCoroutines.Editor.EditorCoroutineUtility, Unity.EditorCoroutines.Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                //     false);
                // MethodInfo method = type?.GetMethod("StartCoroutineOwnerless");
                // method?.Invoke(null, new object[] { coroutine });
            }
        }

        private static void EnsureRunner()
        {
            if (_runner == null)
            {
                _runner = new GameObject("[Static Coroutine Runner]").AddComponent<StaticCoroutineRunner>();
                Object.DontDestroyOnLoad(_runner.gameObject);
            }
        }

        private class StaticCoroutineRunner : MonoBehaviour {}
    }
}