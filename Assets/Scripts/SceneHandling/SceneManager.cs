﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
#if UNITY_EDITOR
#endif

namespace SceneHandling
{
    public sealed class SceneManager
    {
        public static SceneManager Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod]
        [InitializeInEditorMethod]
        private static void OnApplicationInitialize()
        {
            SceneManagerSettings.Initialize(() =>
            {
                if (Instance == null)
                {
                    Instance = new SceneManager();
                }

                InitializeEditor();
            });
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadStartupScenes() {}

        // #if UNITY_STANDALONE && !UNITY_EDITOR
//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//         private static void LoadStartingSceneAtStartup()
//         {
//             if (SceneManager.sceneCount == 1 && SceneManager.GetActiveScene().buildIndex == 0)
//             {
//                 Instance.LoadScene(SceneUtility.GetScenePathByBuildIndex(1), true);
//             }
//         }
// #endif

        private static void InitializeEditor() {}

        public static void LoadSceneAsync(string scenePath, bool setActive, Action<bool> onLoadComplete = null)
        {
            AsyncOperation asyncOperation =
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

            void AsyncDelegate(AsyncOperation obj)
            {
                if (obj.isDone)
                {
                    if (setActive)
                    {
                        UnityEngine.SceneManagement.SceneManager.SetActiveScene(
                            UnityEngine.SceneManagement.SceneManager.GetSceneByPath(scenePath));
                    }

                    onLoadComplete?.Invoke(true);
                }
                else
                {
                    Debug.LogError($"Error (SceneLoaderManager): Scene {scenePath} failed loading");
                    onLoadComplete?.Invoke(false);
                }

                asyncOperation.completed -= AsyncDelegate;
            }

            asyncOperation.completed += AsyncDelegate;
        }

        public static void UnloadSceneAsync(string scenePath, Action<bool> onUnloadComplete = null)
        {
            AsyncOperation asyncOperation =
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scenePath, UnloadSceneOptions.None);

            void AsyncDelegate(AsyncOperation obj)
            {
                if (obj.isDone)
                {
                    Resources.UnloadUnusedAssets();
                    onUnloadComplete?.Invoke(true);
                }
                else
                {
                    Debug.LogError($"Error (SceneLoaderManager): Scene {scenePath} failed unloading");
                    onUnloadComplete?.Invoke(false);
                }

                asyncOperation.completed -= AsyncDelegate;
            }

            asyncOperation.completed += AsyncDelegate;
        }

        public static void SwapActiveScene(string scenePath)
        {
            StaticCoroutine.Run(SwapScenes(scenePath));
        }

        private static IEnumerator SwapScenes(string scenePath)
        {
            // This is required to fix some weird issue. https://issuetracker.unity3d.com/issues/loadsceneasync-allowsceneactivation-flag-is-ignored-in-awake?page=1#comments
            yield return null;

            yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene(), UnloadSceneOptions.None);
            // loadingAudioListener.enabled = true;
            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            // loadingAudioListener.enabled = false;

            yield return Resources.UnloadUnusedAssets();

            UnityEngine.SceneManagement.SceneManager.SetActiveScene(
                UnityEngine.SceneManagement.SceneManager.GetSceneByPath(scenePath));
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {}

        private static void OnSceneUnload(Scene scene) {}
    }
}