using UnityEngine;

namespace FoodFight
{
    public class Food : MonoBehaviour
    {
        public GameObject whole;
        public GameObject sliced;
        public int score = 1;

        private Rigidbody foodRigidbody;
        private Collider foodCollider;
        private ParticleSystem juice;

        private void Awake()
        {
            foodRigidbody = GetComponent<Rigidbody>();
            foodCollider = GetComponent<Collider>();
            juice = GetComponentInChildren<ParticleSystem>();
            whole.SetActive(true);
            sliced.SetActive(false);
            whole.transform.rotation = Random.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BladeBase blade = other.GetComponent<BladeBase>();
                Slice(blade.direction, blade.transform.position, blade.GetForce());
                blade.AddScore(score);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                BladeBase blade = collision.collider.GetComponent<BladeBase>();
                Slice(blade.direction, blade.transform.position, blade.GetForce());
                blade.AddScore(score);
            }
        }

        public void SetScore(int score) => this.score = score;

        private void Slice(Vector3 direction, Vector3 position, float force)
        {
            whole.SetActive(false);
            sliced.SetActive(true);

            foodCollider.enabled = false;
            juice.Play();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody slice in slices)
            {
                slice.linearVelocity = foodRigidbody.linearVelocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }
    }
}