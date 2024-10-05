using System.Collections;
using UnityEngine;

public class FaucetDropSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    public float fallSpeed = 5f;
    public float delayAfterFall = 2f;
    public CreatureSpawner CreatureSpawner;
    private void Start()
    {
        StartCoroutine(SpawnPrefabRoutine());
    }

    private IEnumerator SpawnPrefabRoutine()
    {
        while (true)
        {
            GameObject spawnedPrefab = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(FallAndTriggerEvent(spawnedPrefab));
            Destroy(spawnedPrefab,1);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator FallAndTriggerEvent(GameObject fallingPrefab)
    {
        while (fallingPrefab.transform.position.y > -4.5f)
        {
            fallingPrefab.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(delayAfterFall);
        OnPrefabFallen(fallingPrefab);
    }

    private void OnPrefabFallen(GameObject fallenPrefab)
    {
        CreatureSpawner.SpawnCreature();
    }
}
