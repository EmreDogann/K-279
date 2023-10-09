using System.Collections;
using UnityEngine;

namespace Utils
{
    public class StaticCoroutine
    {
        private static StaticCoroutineRunner _runner;

        public static Coroutine Start(IEnumerator coroutine)
        {
            EnsureRunner();
            return _runner.StartCoroutine(coroutine);
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