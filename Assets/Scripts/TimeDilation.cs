using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilation : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1.0f;

    private Movement _objectMovement = null;
    public float LifeTime
    { set { _lifeTime = value; } }

    private void Awake()
    {
        _objectMovement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (!_objectMovement)
            return;

        Vector3 playerVelocity = GameState.Instance.PlayerVelocity;
        Vector3 objectVelocity = _objectMovement.ObjectVelocity;

        Vector3 leftTerm = objectVelocity - playerVelocity;
        Vector3 rightTerm = Vector3.one - Vector3.Scale(objectVelocity, playerVelocity) / GameState.LIGHTSPEED_SQUARED;
        Vector3 relativeVel = new Vector3(leftTerm.x / rightTerm.x, leftTerm.y / rightTerm.y, leftTerm.z / rightTerm.z);

        _lifeTime -= Time.smoothDeltaTime * (Mathf.Sqrt(1 - (relativeVel.sqrMagnitude / GameState.LIGHTSPEED_SQUARED)));

        if (_lifeTime <= 0)
            Destroy(gameObject);

    }
}
