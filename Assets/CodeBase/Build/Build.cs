using System.Collections;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private GameObject _objectsToBuild;
    [SerializeField] private GameObject _buildUI;
    [SerializeField] private GameObject _brushPrefab;
    [SerializeField] private GameObject _buildText;
    [SerializeField] private GameObject _upgradeText;
    [SerializeField] private Transform _buildPosition;

    private bool _isBuild = false;
    private bool _isBuilt = false;
    private GameObject _currentBrush;
    [SerializeField] private int _coast;
    [SerializeField] private int _upgradeCoast;
    [SerializeField] private float _buildTime = 3f;
    [SerializeField] private float _moveDistance = 1f;

    private int _upgradeIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isBuild && _upgradeIndex != 3)
        {
            _buildUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_buildUI.activeSelf && collision.CompareTag("Player"))
        {
            _buildUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (_buildUI.activeSelf)
        {
            if (Input.GetKey(KeyCode.F) && CashManager.Instance.Money >= _coast)
            {
                if (!_isBuild && !_isBuilt)
                {
                    StartCoroutine(BuildProcess());
                }
                else if (_isBuilt)
                {
                    UpgradeBuild();
                }
            }
        }
    }

    private void UpgradeBuild()
    {
        CashManager.Instance.RemoveMoney(_upgradeCoast);
        UpgraderBuild upgraderBuild = _objectsToBuild.GetComponent<UpgraderBuild>();
        upgraderBuild.DecreaseActivationInterval(3);
        _upgradeIndex++;
        if (_upgradeIndex >= 3)
        {
            _buildUI.SetActive(false);
        }
    }

    private IEnumerator BuildProcess()
    {
        _buildText.SetActive(false);
        _isBuild = true;
        CashManager.Instance.RemoveMoney(_coast);

        _currentBrush = Instantiate(_brushPrefab, _buildPosition.position, Quaternion.identity);

        Vector3 startPosition = _currentBrush.transform.position;
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime / _buildTime;

            float xOffset = Mathf.Sin(progress * Mathf.PI * 2) * _moveDistance;
            float yOffset = progress * _moveDistance;

            _currentBrush.transform.position = startPosition + new Vector3(xOffset, yOffset, 0);

            yield return null;
        }

        Destroy(_currentBrush);
        _objectsToBuild.SetActive(true);
        _isBuild = false;
        _isBuilt = true;
        _upgradeText.SetActive(true);
    }
}
