using UnityEngine;

namespace FoodFight
{
    public class BackgroundPicker : MonoBehaviour
    {
        public GameManager gameManager;
        private GameObject background;

        private void Awake()
        {
            if (gameManager.selectedBackground < 0) gameManager.selectedBackground = Random.Range(0, gameManager.backgroundList.prefabs.Count);
            SpawnBackground();

            gameManager.OnBackgroundChange.AddListener(OnChange);
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
