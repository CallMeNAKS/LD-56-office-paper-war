using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public ProgressBar objectActivator;
    public GameObject creaturePrefab;
    public Sprite creatureSprite;
    public Transform spawnPosition;

    public string creatureName;
    public int health;
    public int damage;
    public float speed;


    private void Start()
    {
        if (objectActivator != null)
        {
            objectActivator.onAllObjectsActivated.AddListener(SpawnCreature);
        }
    }

    public void SpawnCreature()
    {
        GameObject newCreatureObject = Instantiate(creaturePrefab, spawnPosition.position, Quaternion.identity);

        Creature newCreature = newCreatureObject.GetComponent<Creature>();
        newCreature.Initialize(creatureName, health, damage, speed, creatureSprite);
    }
}
