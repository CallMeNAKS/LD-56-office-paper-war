using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    public List<GameObject> objectsToActivate;

    public float activationInterval = 2f;

    public UnityEvent onAllObjectsActivated;

    private int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(ActivateObjects());
    }

    private IEnumerator ActivateObjects()
    {
        while (true)
        {
            while (currentIndex < objectsToActivate.Count)
            {
                objectsToActivate[currentIndex].SetActive(true);

                yield return new WaitForSeconds(activationInterval);

                currentIndex++;
            }

            onAllObjectsActivated.Invoke();

            currentIndex = 0;

            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(false);
            }
        }
    }
}