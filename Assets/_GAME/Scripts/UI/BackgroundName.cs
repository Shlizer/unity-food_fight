using UnityEngine;

namespace FoodFight.UI
{
    public class BackgroundName : MonoBehaviour
    {
        public GameManager gameManager;
        public TMPro.TMP_Text text;

        private void Awake()
        {
            if (gameManager == null) gameManager = GetComponentInParent<GameManagerProvider>()?.GetGameManager();

            gameManager.OnBackgroundChange.AddListener(OnChange);
        }

        private void OnDestroy()
        {
            gameManager.OnBackgroundChange.RemoveListener(OnChange);
        }

        private void OnChange(string name)
        {
            text.text = name;
        }
    }
}
