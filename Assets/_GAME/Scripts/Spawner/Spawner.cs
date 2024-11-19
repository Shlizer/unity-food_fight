using System.Collections;
using UnityEngine;

namespace FoodFight
{
    [RequireComponent(typeof(Collider))]
    public class Spawner : MonoBehaviour
    {
        public GameManager gameManager;
        public FoodList foodList;

        private Collider spawnArea;
        private bool isSpawning = false;

        void Awake()
        {
            spawnArea = GetComponent<Collider>();
            if (gameManager == null) gameManager = GetComponentInParent<GameManagerProvider>()?.GetGameManager();
            gameManager.OnGameStateChange.AddListener(ChangePlaying);
        }

        private void OnDestroy()
        {
            gameManager.OnGameStateChange.RemoveListener(ChangePlaying);
        }

        void ChangePlaying(GameState state)
        {
            if (state == GameState.Playing && !isSpawning)
            {
                StartCoroutine(Spawn());
                isSpawning = true;
            }
            if (state == GameState.Stopped)
            {
                StopAllCoroutines();
                isSpawning = false;
            }
        }

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(2 * Random.Range(gameManager.minSpawnDelay, gameManager.maxSpawnDelay));

            while (enabled)
            {
                var element = foodList.elements[Random.Range(0, foodList.elements.Count)];
                GameObject prefab = element.prefab.gameObject;

                Vector3 position = new Vector3(
                    Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                    Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
                );

                Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(gameManager.minAngle, gameManager.maxAngle));
                GameObject food = Instantiate(prefab, position, rotation);
                food.transform.parent = transform;
                food.GetComponent<Food>().SetScore(element.score);
                Destroy(food, gameManager.maxLifetime);

                float force = Random.Range(gameManager.maxForce, gameManager.maxForce);
                food.GetComponent<Rigidbody>().AddForce(food.transform.up * force, ForceMode.Impulse);

                yield return new WaitForSeconds(Random.Range(gameManager.minSpawnDelay, gameManager.maxSpawnDelay));
            }
        }
    }
}
