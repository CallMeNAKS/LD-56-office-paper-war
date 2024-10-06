using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _health = 300;

    [SerializeField] private GameObject _healthUI;
    [SerializeField] private List<GameObject> _healthProgressUI;

    public void Hit(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            if (gameObject.CompareTag("Ally"))
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                GameManager.Instance.GameWin();
            }
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < _healthProgressUI.Count; i++)
        {
            _healthProgressUI[i].SetActive(i * 60 < _health);
        }
    }

    public void ResetHealthUI()
    {
        _health = 300;
        UpdateHealthUI();
    }
}
