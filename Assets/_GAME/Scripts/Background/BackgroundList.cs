using System.Collections.Generic;
using UnityEngine;

namespace FoodFight
{
    [CreateAssetMenu(fileName = "Background List", menuName = "Food Fight/Background List")]
    public class BackgroundList : ScriptableObject
    {
        public List<GameObject> prefabs;
    }
}
