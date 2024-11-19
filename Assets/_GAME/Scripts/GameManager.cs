using UnityEngine;
using UnityEngine.Events;

namespace FoodFight
{
    [CreateAssetMenu(fileName = "Game Manager", menuName = "Food Fight/Manager")]
    public class GameManager : ScriptableObject
    {
        [Header("Gameplay flow")]
        public SoundManager soundManager;
        public GameState gameState { get; private set; } = GameState.Stopped;
        //public bool isPlaying { get; private set; } = false;
        //public bool isPaused { get; private set; }
        public int score = 0;
        [HideInInspector] public UnityEvent<GameState> OnGameStateChange;
        [HideInInspector] public UnityEvent<int> OnScoreChange;

        [Header("Background")]
        public BackgroundList backgroundList;
        public int selectedBackground = -1;
        [HideInInspector] public UnityEvent<string> OnBackgroundChange;

        [Header("Spawner")]
        public float minSpawnDelay = .25f;
        public float maxSpawnDelay = 1f;

        public float minAngle = -15f;
        public float maxAngle = -15f;

        public float minForce = 18f;
        public float maxForce = 22f;

        public float maxLifetime = 5f;

        [Header("Blade")]
        public float minSliceVelocity = 0.01f;
        public float sliceForce = 5f;
        public InputMode inputMode = InputMode.Unknown;
        public InputMode inputModeForced = InputMode.Unknown;

        public void NextBackground()
        {
            if (selectedBackground < 0) selectedBackground = Random.Range(0, backgroundList.prefabs.Count);

            if (selectedBackground == backgroundList.prefabs.Count - 1) selectedBackground = 0;
            else selectedBackground++;

            OnBackgroundChange?.Invoke(backgroundList.prefabs[selectedBackground].name);
        }

        public void PrevBackground()
        {
            if (selectedBackground < 0) selectedBackground = Random.Range(0, backgroundList.prefabs.Count);

            if (selectedBackground == 0) selectedBackground = backgroundList.prefabs.Count - 1;
            else selectedBackground--;

            OnBackgroundChange?.Invoke(backgroundList.prefabs[selectedBackground].name);
        }

        public void AddScore(int score = 1)
        {
            this.score += score;
            OnScoreChange?.Invoke(this.score);

            soundManager.PlayGameScored();
        }

        private void ChangeGameState(GameState state)
        {
            OnGameStateChange?.Invoke(state);
            gameState = state;
        }

        public void Play()
        {
            score = 0;
            OnScoreChange?.Invoke(score);
            ChangeGameState(GameState.Playing);
            Time.timeScale = 1;
        }

        public void Pause()
        {
            ChangeGameState(GameState.Paused);
            Time.timeScale = 0;
        }

        public void Resume()
        {
            ChangeGameState(GameState.Playing);
            Time.timeScale = 1;
        }

        public void Stop()
        {
            ChangeGameState(GameState.Stopped);
            Time.timeScale = 0;
        }

        public void Exit() => Application.Quit();
    }

    [System.Serializable]
    public enum InputMode
    {
        Unknown,
        Mouse,
        Touch,
    }

    [System.Serializable]
    public enum GameState
    {
        Stopped,
        Paused,
        Playing,
    }
}
