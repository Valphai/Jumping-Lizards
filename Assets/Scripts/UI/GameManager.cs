using System;

namespace UI
{
    public static class GameManager
    {
        public static event Action OnEnd;
        public static event Action OnRestart;

        public static void GameEnd() => OnEnd?.Invoke();
        public static void RestartGame() => OnRestart?.Invoke();
    }
}