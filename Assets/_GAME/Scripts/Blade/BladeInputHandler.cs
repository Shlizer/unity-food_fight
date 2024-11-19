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
            if (gameManager == null) gameManager = GetComponentInParent<GameManagerProvider>()?.GetGameManager();

            gameManager.inputMode = InputMode.Unknown;
            Cursor.lockState = CursorLockMode.Confined;
            mainCamera = Camera.main;
            gameManager.OnGameStateChange.AddListener(ChangeGameState);
        }

        private void OnDestroy()
        {
            gameManager.OnGameStateChange.RemoveListener(ChangeGameState);
        }

        void ChangeGameState(GameState playing)
        {
            if (playing == GameState.Playing) StartPlaying(); else StopPlaying();
        }

        void StartPlaying()
        {
            CheckInputMode();
        }

        void CheckInputMode()
        {
            if (gameManager.inputModeForced == InputMode.Unknown)
            {
                if (HasMultitouch() && gameManager.inputMode != InputMode.Touch)
                    ChangeInputMode(InputMode.Touch);
                else if (!HasMultitouch() && gameManager.inputMode != InputMode.Mouse)
                    ChangeInputMode(InputMode.Mouse);
            } else
            {
                if (gameManager.inputMode != gameManager.inputModeForced)
                    ChangeInputMode(gameManager.inputModeForced);
            }
        }

        void StopPlaying()
        {
            ClearBlades();
        }

        private void ClearBlades()
        {
            for (int i = 0; i < blades.Count; i++) Destroy(blades[i]?.gameObject);
            blades.Clear();
            gameManager.inputMode = InputMode.Unknown;
        }

        public void ChangeInputMode(InputMode inputMode)
        {
            ClearBlades();

            gameManager.inputMode = inputMode;

            if (gameManager.inputMode == InputMode.Mouse)
            {
                blades.Add(Instantiate<BladeBase>(bladeMousePrefab));
                blades[0].transform.parent = transform;
                blades[0].transform.name = $"[ BLADE MOUSE ]";
                blades[0].gameManager = gameManager;
            }
        }

        private bool HasMultitouch() => Input.touchSupported && Input.multiTouchEnabled;

        void Update()
        {
            if (gameManager.inputMode == InputMode.Touch)
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
            if (gameManager.gameState != GameState.Playing) return;
            CheckInputMode();
        }
    }
}
