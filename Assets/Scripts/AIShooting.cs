using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    private ShootProjectile _projectile = null;
    private Collider _collider = null;

    private bool _alive = true;

    private void Awake()
    {
        _collider = GetComponentInChildren<Collider>();
        _projectile = GetComponent<ShootProjectile>();
    }

    private void Start()
    {
        StartCoroutine(DetectDebris());
    }

    private IEnumerator DetectDebris()
    {
        float raycastInterval = 1.0f;
        float rayDistance = 15.0f;

        while (_alive)
        {
            RaycastHit hit;
            Ray ray = new Ray(_collider.bounds.center, transform.forward);

            if (Physics.Raycast(ray, out hit, rayDistance))
                _projectile.OnShootProjectile();

            yield return new WaitForSeconds(raycastInterval);
        }
    }

    private void OnDestroy()
    {
        _alive = false;
    }
}
