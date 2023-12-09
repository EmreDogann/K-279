using AYellowpaper;
using MiniGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameManager : MonoBehaviour
{
    private int currentGameIndex;
    private bool gameCurrentlyPlaying;
    [SerializeField] private List<InterfaceReference<IMiniGame, MonoBehaviour>> miniGameList;
    [SerializeField] private List<UnityEvent> unityEventList;

    public void BeginMiniGame(int miniGameIndex)
    {
        currentGameIndex = miniGameIndex;
        gameCurrentlyPlaying = true;
        StartCoroutine(StartGameAndCheckForEnd());
    }
    IEnumerator StartGameAndCheckForEnd()
    {
        IMiniGame miniGame = miniGameList[currentGameIndex].Value;
        miniGame.StartGame();
        while (true)
        {
            yield return new WaitUntil(() => miniGame.GameEnded());
            unityEventList[currentGameIndex].Invoke();
            gameCurrentlyPlaying = false;
            break;
        }
    }
}
