using System.Collections.Generic;
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

    private List<GameObject> _spawnList = new List<GameObject>();

    private void OnEnable()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            float difficulty = GameManager.Instance.Difficulty;
            if (difficulty == 1)
            {
                health = 40;
                damage = 5;
            }
            if (difficulty > 1)
            {
                health += 10;
                damage += 3;
            }
            if (difficulty > 2)
            {
                health += 10;
                damage += 2;
            }
        }
    }


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
        _spawnList.Add(newCreatureObject);

        Creature newCreature = newCreatureObject.GetComponent<Creature>();
        newCreature.Initialize(creatureName, health, damage, speed, creatureSprite);
    }

    public void RemoveCreature()
    {
        foreach (GameObject creatureObject in _spawnList)
        {
            Destroy(creatureObject);
        }
        gameObject.SetActive(false);
    }
}
