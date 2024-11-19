using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [CreateAssetMenu(fileName = "Game Manager", menuName = "GAME/Manager")]
    public class GameManager : ScriptableObject
    {
        [Header("Gameplay flow")]
        public bool isPlaying { get; private set; } = false;
        public int score = 0;
        [HideInInspector] public UnityEvent<bool> OnPlayChange;
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

        public void NextBackground()
        {
            if (selectedBackground == backgroundList.prefabs.Count - 1) selectedBackground = 0;
            else selectedBackground++;

            OnBackgroundChange?.Invoke(backgroundList.prefabs[selectedBackground].name);
        }

        public void PrevBackground()
        {
            if (selectedBackground == 0) selectedBackground = backgroundList.prefabs.Count - 1;
            else selectedBackground--;

            OnBackgroundChange?.Invoke(backgroundList.prefabs[selectedBackground].name);
        }

        public void AddScore(int score = 1)
        {
            this.score += score;
            OnScoreChange?.Invoke(this.score);
        }

        public void Play()
        {
            score = 0;
            OnScoreChange?.Invoke(score);
            isPlaying = true;
            OnPlayChange?.Invoke(isPlaying);
        }

        public void Stop()
        {
            isPlaying = false;
            OnPlayChange?.Invoke(isPlaying);
        }

        public void Exit() => Application.Quit();

        [System.Serializable]
        public enum InputMode
        {
            Unknown,
            Mouse,
            Touch,
        }
    }
}
