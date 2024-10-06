using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _moneyText;

    [SerializeField] private List<GameObject> _inkBottles;

    public int Money;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        ChangeMoneyUI();
    }

    public void RemoveMoney(int amount)
    {
        Money -= amount;
        ChangeMoneyUI();
    }

    private void ChangeMoneyUI()
    {
        _moneyText.text = Money.ToString();
        if (Money == 0)
        {
            ChangeState(_inkBottles[0]);
        }
        else if (Money >= 1 && Money < 5)
        {
            ChangeState(_inkBottles[1]);
        }
        else if (Money >= 5 && Money < 10)
        {
            ChangeState(_inkBottles[2]);
        }
        else if (Money >= 10 && Money < 15)
        {
            ChangeState(_inkBottles[3]);
        }
        else if (Money >= 15)
        {
            ChangeState(_inkBottles[4]);
        }
    }

    private void ChangeState(GameObject gameObject)
    {
        foreach (var item in _inkBottles) { item.SetActive(false); }
        gameObject.SetActive(true);
    }
}
