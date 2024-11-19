using System.Collections;
using UnityEngine;

namespace FoodFight.UI
{
    public class ScoreShower : MonoBehaviour
    {
        public GameManager gameManager;
        public TMPro.TMP_Text text;
        public ParticleSystem effectOnChange;

        public float maxScale = 2f;
        public float scaleDuration = 0.3f;

        private Coroutine coroutine;

        private void Awake()
        {
            gameManager.score = 0;
            gameManager.OnScoreChange.AddListener(OnChange);
            gameManager.OnPlayChange.AddListener(ChangePlaying);
            ChangePlaying(gameManager.isPlaying);
            OnChange(gameManager.score);
        }

        private void OnDestroy()
        {
            gameManager.OnScoreChange.RemoveListener(OnChange);
            gameManager.OnPlayChange.RemoveListener(ChangePlaying);
        }

        void ChangePlaying(bool playing)
        {
            if (playing) gameObject.SetActive(true); else gameObject.SetActive(false);
        }

        private void OnChange(int score)
        {
            text.text = score.ToString();

            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(Scale());

            effectOnChange.Stop();
            effectOnChange.Play();
        }

        IEnumerator Scale()
        {
            text.transform.localScale = Vector3.one * maxScale;
            float counter = 0;

            while (counter < scaleDuration)
            {
                counter += Time.deltaTime;
                text.transform.localScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one, counter/ scaleDuration);
                yield return null;
            }

            text.transform.localScale = Vector3.one;
        }
    }
}
