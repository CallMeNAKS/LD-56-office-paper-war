using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            GameManager.Instance.RestartGame();
            gameObject.SetActive(false);
        }
    }
}
