using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Food List", menuName = "GAME/Food List")]
    public class FoodList : ScriptableObject
    {
        public List<Food> prefabs;
    }
}
