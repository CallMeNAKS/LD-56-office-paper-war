using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public ProgressBar objectActivator;
    public GameObject creaturePrefab;
    public Sprite creatureSprite;
    public Transform spawnPosition;

    private void Start()
    {
        objectActivator.onAllObjectsActivated.AddListener(SpawnCreature);
    }

    private void SpawnCreature()
    {
        GameObject newCreatureObject = Instantiate(creaturePrefab, spawnPosition.position, Quaternion.identity);

        Creature newCreature = newCreatureObject.GetComponent<Creature>();
        newCreature.Initialize("PaperMan", 100, 2.5f, creatureSprite);
    }
}
