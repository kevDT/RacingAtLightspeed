using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _projectileTemplate = null;
    [SerializeField] private Transform _shootSocket = null;

    private const float COOLDOWN = 0.5f;
    private bool _overheated = false;

    public void OnShootProjectile()
    {
        if (_overheated || Time.timeScale < 1.0f)
            return;

        Instantiate(_projectileTemplate, _shootSocket.position, Quaternion.identity);
        _overheated = true;

        StartCoroutine(ResetCooldown());
    }

    public void ShootProjectileFrom(Vector3 pos)
    {
        if (_overheated)
            return;

        Instantiate(_projectileTemplate, pos, Quaternion.identity);
        _overheated = true;

        StartCoroutine(ResetCooldown());
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(COOLDOWN);
        _overheated = false;
    }
}
