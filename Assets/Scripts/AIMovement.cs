using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private Transform _goal = null;

    private Vector3 _velocityAI = Vector3.zero;

    public override Vector3 ObjectVelocity
    {
        get { return _velocityAI; }
    }

    private void Awake()
    {
        _velocityAI = (_goal.position - transform.position).normalized * _speed;
    }

    public override void Update()
    {
        _velocityAI = (_goal.position - transform.position).normalized * _speed;
        transform.Translate(_velocityAI * Time.smoothDeltaTime);

        Material[] materials = GetComponentInChildren<Renderer>().materials;
        foreach (Material material in materials)
        {
            material.SetVector("_ObjectVelocity", _velocityAI / GameState.LIGHTSPEED);
        }
    }
}
