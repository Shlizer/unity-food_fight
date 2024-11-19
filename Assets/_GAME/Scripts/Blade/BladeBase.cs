using UnityEngine;

namespace FoodFight
{
    public abstract class BladeBase : MonoBehaviour
    {
        public GameManager gameManager;

        protected Camera mainCamera;
        protected Collider bladeCollider;
        protected TrailRenderer trailRenderer;
        protected bool slicing;
        public Vector3 direction { get; protected set; }

        public bool IsSlicing { get { return slicing; } private set { } }

        private void Awake()
        {
            mainCamera = Camera.main;
            bladeCollider = GetComponent<Collider>();
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        private void OnEnable()
        {
            StopSlicing();
        }

        private void OnDisable()
        {
            StopSlicing();
        }

        public virtual void StartSlicing()
        {
            SetNewPosition();
            slicing = true;
            bladeCollider.enabled = true;
            trailRenderer.Clear();
            trailRenderer.enabled = true;
        }

        public virtual void StopSlicing()
        {
            slicing = false;
            bladeCollider.enabled = false;
            trailRenderer.enabled = false;
        }

        protected abstract void SetNewPosition();

        public void AddScore(int score = 1) => gameManager.AddScore(score);
        public float GetForce() => gameManager.sliceForce;
    }
}
