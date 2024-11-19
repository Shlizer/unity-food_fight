using UnityEngine;

namespace FoodFight
{
    public class GameManagerProvider : MonoBehaviour
    {
        public GameManager gameManager;

        public GameManager GetGameManager() => gameManager;
    }
}
