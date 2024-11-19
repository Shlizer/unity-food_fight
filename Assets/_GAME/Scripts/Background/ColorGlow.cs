using UnityEngine;

namespace FoodFight
{
    public class ColorGlow : MonoBehaviour
    {
        public float speed = 1.0f;
        [ColorUsage(true, true)]
        public Color color1;
        [ColorUsage(true, true)]
        public Color color2;

        Renderer matRenderer;
        float startTime;

        void Start()
        {
            startTime = Time.time;
            matRenderer = GetComponent<Renderer>();
            matRenderer.material.EnableKeyword("_EMISSION");
        }

        // Update is called once per frame
        void Update()
        {
            float frac = Mathf.Abs(Mathf.Sin(Time.time - startTime) * speed);

            matRenderer.material.SetColor("_EmissionColor", Color.Lerp(color1, color2, frac));
        }
    }
}
