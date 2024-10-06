using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _endGameUI;
    [SerializeField] private GameObject _winGameUI;

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

    private void Start()
    {
        // ������������� � ������ ����
    }

    private void Update()
    {
        // ���������� ������ ����
    }

    public void StartGame()
    {
        // ������ ������ ����
    }

    public void EndGame()
    {
        // ������ ���������� ����
    }
}
