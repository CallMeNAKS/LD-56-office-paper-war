using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _endGameUI;
    [SerializeField] private GameObject _winGameUI;

    [SerializeField] private List<CreatureSpawner> _creatureSpawners;
    [SerializeField] private GameObject _build;
    [SerializeField] private List<GameObject> _Spawners;
    [SerializeField] private List<Base> _Bases;

    public float Difficulty = 1f;
    private float _difficultyModifier = 0.2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        _endGameUI.SetActive(true);
    }

    public void GameWin()
    {
        _winGameUI.SetActive(true);
    }

    public void StartGame()
    {
        foreach (var spawner in _Spawners)
        {
            spawner.SetActive(true);
        }
    }

    public void ÑontinueGame()
    {
        float difficulty = Difficulty;
        ResetStaff();
        Difficulty = difficulty + _difficultyModifier;
        StartGame();
    }

    public void RestartGame()
    {
        Difficulty = 1f;
        ResetStaff();
        StartGame();
    }

    private void ResetStaff()
    {
        foreach (var creature in _creatureSpawners)
        {
            creature.RemoveCreature();
        }
        _build.SetActive(false);
        foreach (var baseBuild in _Bases)
        {
            baseBuild.ResetHealthUI();
        }
    }
}
