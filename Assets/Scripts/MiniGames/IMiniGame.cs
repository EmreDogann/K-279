namespace MiniGame
{
    public enum MiniGameState
    {
        IDLE,
        PLAYING,
        ENDEDWON,
        ENDEDLOST
    }
    public interface IMiniGame
    {
        public void StartGame();

        public bool GameEnded();

        public MiniGameState GetGameState();
    }
}
