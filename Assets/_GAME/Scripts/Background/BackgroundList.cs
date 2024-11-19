using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Background List", menuName = "GAME/Background List")]
    public class BackgroundList : ScriptableObject
    {
        public List<GameObject> prefabs;
    }
}
