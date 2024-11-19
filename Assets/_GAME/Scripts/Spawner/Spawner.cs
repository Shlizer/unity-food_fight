using Game;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    public GameManager gameManager;
    public FoodList foodList;

    private Collider spawnArea;

    void Awake()
    {
        spawnArea = GetComponent<Collider>();
        gameManager.OnPlayChange.AddListener(ChangePlaying);
    }

    private void OnDestroy()
    {
        gameManager.OnPlayChange.RemoveListener(ChangePlaying);
    }

    void ChangePlaying(bool playing)
    {
        if (playing) StartCoroutine(Spawn()); else StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2 * Random.Range(gameManager.minSpawnDelay, gameManager.maxSpawnDelay));

        while (enabled)
        {
            GameObject prefab = foodList.prefabs[Random.Range(0, foodList.prefabs.Count)].gameObject;

            Vector3 position = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            );

            Quaternion rotation = Quaternion.Euler(0,0, Random.Range(gameManager.minAngle, gameManager.maxAngle));
            GameObject food = Instantiate(prefab, position, rotation);
            Destroy(food, gameManager.maxLifetime);

            float force = Random.Range(gameManager.maxForce, gameManager.maxForce);
            food.GetComponent<Rigidbody>().AddForce(food.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(gameManager.minSpawnDelay, gameManager.maxSpawnDelay));
        }
    }
}
