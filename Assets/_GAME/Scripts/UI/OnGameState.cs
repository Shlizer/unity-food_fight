using UnityEngine;
using UnityEngine.Events;

namespace FoodFight
{
    public class OnGameState : MonoBehaviour
    {
        public GameManager gameManager;
        public GameState gameState;
        public UnityEvent OnChange;

        private void Awake()
        {
            if (gameManager == null) gameManager = GetComponentInParent<GameManagerProvider>()?.GetGameManager();

            gameManager.OnGameStateChange.AddListener(GameStateChanged);
        }

        private void OnDestroy()
        {
            gameManager.OnGameStateChange.RemoveListener(GameStateChanged);
        }

        private void GameStateChanged(GameState newState)
        {
            var currentState = gameManager.gameState;

            if (gameState == newState)
            {
                OnChange?.Invoke();
            }
        }
    }
}
