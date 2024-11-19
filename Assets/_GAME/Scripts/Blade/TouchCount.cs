using UnityEngine;

namespace Game
{
    public class TouchCount : MonoBehaviour
    {
        public TMPro.TMP_Text counter;

        void Update()
        {
            counter.text = Input.touchCount.ToString();
        }
    }
}
