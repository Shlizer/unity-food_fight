using System.Collections.Generic;
using UnityEngine;

namespace FoodFight
{
    [CreateAssetMenu(fileName = "Food List", menuName = "Food Fight/Food List")]
    public class FoodList : ScriptableObject
    {
        public List<Food> prefabs;
    }
}
