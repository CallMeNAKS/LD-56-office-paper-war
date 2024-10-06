using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgraderBuild : MonoBehaviour
{
    [SerializeField] private List<GameObject> _holdObjects = new List<GameObject>();

    private bool _isHaveObject => _holdObjects.Exists(obj => obj.activeSelf);

    [SerializeField] private int _bonusDamage;
    [SerializeField] private float _activationInterval = 5f;


    private void Start()
    {
        StartCoroutine(ActivateHoldObjectsPeriodically());
    }

    public void DecreaseActivationInterval(float time)
    {
        _activationInterval -= time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.name);
        if (_isHaveObject && other.CompareTag("Ally"))
        {
            GiveBonus(other);
            RemoveOneHoldObject();
        }
    }

    private void GiveBonus(Collider2D other)
    {
        Creature component = other.GetComponent<Creature>();
        if (component != null)
        {
            component.BonusInitialize(_bonusDamage);
            component.AddEraser();
        }
    }

    private void RemoveOneHoldObject()
    {
        for (int i = 0; i < _holdObjects.Count; i++)
        {
            if (_holdObjects[i].activeSelf)
            {
                _holdObjects[i].SetActive(false);
                break;
            }
        }
    }

    private IEnumerator ActivateHoldObjectsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(_activationInterval);

            foreach (var obj in _holdObjects)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    break;
                }
            }
        }
    }
}
