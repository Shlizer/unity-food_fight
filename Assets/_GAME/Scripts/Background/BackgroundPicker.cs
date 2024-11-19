using UnityEngine;

namespace FoodFight
{
    public class BackgroundPicker : MonoBehaviour
    {
        public GameManager gameManager;
        private GameObject background;

        private void Awake()
        {
            if (gameManager == null) gameManager = GetComponentInParent<GameManagerProvider>()?.GetGameManager();

            gameManager.OnBackgroundChange.AddListener(OnChange);
        }

        private void Start()
        {
            gameManager.NextBackground();
        }

        private void OnDestroy()
        {
            gameManager.OnBackgroundChange.RemoveListener(OnChange);
        }

        public void SpawnBackground()
        {
            if (background != null) Destroy(background);

            GameObject prefab = gameManager.backgroundList.prefabs[gameManager.selectedBackground];
            background = Instantiate(prefab, transform);
        }

        private void OnChange(string name)
        {
            SpawnBackground();
        }
    }
}
