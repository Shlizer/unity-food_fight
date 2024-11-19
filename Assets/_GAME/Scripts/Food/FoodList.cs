using System.Collections.Generic;
using UnityEngine;

namespace FoodFight
{
    [CreateAssetMenu(fileName = "Food List", menuName = "Food Fight/Food List")]
    public class FoodList : ScriptableObject
    {
        public List<FoodElement> elements;

        [System.Serializable]
        public struct FoodElement
        {
            public Food prefab;
            public int score;
            public float probability;
        }
    }
}
