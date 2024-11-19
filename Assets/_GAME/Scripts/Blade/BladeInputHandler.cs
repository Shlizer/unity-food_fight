using System.Collections.Generic;
using UnityEngine;

namespace FoodFight
{
    public class BladeInputHandler : MonoBehaviour
    {
        public GameManager gameManager;
        public BladeMouse bladeMousePrefab;
        public BladeTouch bladeTouchPrefab;

        private Camera mainCamera;
        private List<BladeBase> blades = new List<BladeBase>();

        void Awake()
        {
            mainCamera = Camera.main;
            gameManager.OnPlayChange.AddListener(ChangePlaying);
        }

        private void OnDestroy()
        {
            gameManager.OnPlayChange.RemoveListener(ChangePlaying);
        }

        void ChangePlaying(bool playing)
        {
            if (playing) StartPlaying(); else StopPlaying();
        }

        void StartPlaying()
        {
            ChangeInputMode(HasMultitouch()
                ? GameManager.InputMode.Touch
                : GameManager.InputMode.Mouse
            );
        }

        void StopPlaying()
        {
            ClearBlades();
        }

        private void ClearBlades()
        {
            for (int i = 0; i < blades.Count; i++) Destroy(blades[i]?.gameObject);
            blades.Clear();
        }

        public void ChangeInputMode(GameManager.InputMode inputMode)
        {
            gameManager.inputMode = inputMode;

            ClearBlades();

            if (gameManager.inputMode == GameManager.InputMode.Mouse)
            {
                blades.Add(Instantiate<BladeBase>(bladeMousePrefab));
                blades[0].transform.parent = transform;
                blades[0].transform.name = $"[ BLADE MOUSE ]";
                blades[0].gameManager = gameManager;
            }
        }

        private bool HasMultitouch() => !(Input.touchSupported && Input.multiTouchEnabled);

        void Update()
        {
            if (gameManager.inputMode == GameManager.InputMode.Touch)
            {
                while (blades.Count < Input.touchCount)
                {
                    var blade = Instantiate(bladeTouchPrefab);
                    blade.transform.parent = transform;
                    blade.transform.name = $"[ BLADE TOUCH {blades.Count} ]";
                    blade.gameManager = gameManager;
                    blades.Add(blade);
                }

                for (int i = 0; i < blades.Count; i++)
                {
                    if (Input.touchCount > i)
                    {
                        (blades[i] as BladeTouch).SetTouch(Input.GetTouch(i));
                        if (!blades[i].IsSlicing) blades[i].StartSlicing();
                    } else
                    {
                        if (blades[i].IsSlicing) blades[i].StopSlicing();
                    }
                }
            }
        }
        private void LateUpdate()
        {
            if (!gameManager.isPlaying) return;

            if (HasMultitouch() && gameManager.inputMode != GameManager.InputMode.Touch)
                ChangeInputMode(GameManager.InputMode.Mouse);
            else if (!HasMultitouch() && gameManager.inputMode != GameManager.InputMode.Mouse)
                ChangeInputMode(GameManager.InputMode.Touch);
        }
    }
}
