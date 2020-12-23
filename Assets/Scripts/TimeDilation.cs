using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilation : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1.0f;

    private Movement _objectMovement = null;

    private void Awake()
    {
        _objectMovement = GetComponent<Movement>();
    }

    private void Update()
    {

        Vector3 relativeVel = (_objectMovement.ObjectVelocity - GameState.Instance.PlayerVelocity)
            / (1 - (Vector3.Scale(_objectMovement.ObjectVelocity, GameState.Instance.PlayerVelocity).magnitude / GameState.LIGHTSPEED_SPQUARED));

        _lifeTime -= Time.deltaTime * (Mathf.Sqrt(1 - (relativeVel.sqrMagnitude / GameState.LIGHTSPEED_SPQUARED)));

        if (_lifeTime <= 0)
            Destroy(gameObject);
    }
}
