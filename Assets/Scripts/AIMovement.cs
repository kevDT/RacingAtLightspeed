using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private Transform _goal = null;

    private Collider _collider = null;

    private Vector3 _velocityAI = Vector3.zero;
    private Vector3 _originalvelAI = Vector3.zero;

    private bool _alive = true;

    public override Vector3 ObjectVelocity
    {
        get { return _velocityAI; }
    }

    private void Awake()
    {
        _velocityAI = _originalvelAI = (_goal.position - transform.position).normalized * _speed;
        _collider = GetComponentInChildren<Collider>();
    }

    private void Start()
    {
        StartCoroutine(DetectDebris());
    }

    public override void Update()
    {
        transform.Translate(_velocityAI * Time.smoothDeltaTime);

        Material[] materials = GetComponentInChildren<Renderer>().materials;
        foreach (Material material in materials)
        {
            material.SetVector("_ObjectVelocity", _originalvelAI / GameState.LIGHTSPEED);
        }
    }

    private void OnDestroy()
    {
        _alive = false;
    }

    [SerializeField] private LayerMask _avoidanceLayer = -1;
    private IEnumerator DetectDebris()
    {
        float raycastInterval = 0.25f;
        float rayDistance = 15.0f;
        RaycastHit hit;

        while (_alive)
        {
            Ray ray = new Ray(_collider.bounds.center, transform.forward);

            if (Physics.Raycast(ray, out hit, rayDistance, _avoidanceLayer))
                StartCoroutine(BoostAway());

            yield return new WaitForSeconds(raycastInterval);
        }
    }

    private IEnumerator BoostAway()
    {
        float boostDuration = 0.2f;
        float distanceFromAI = 15.0f;

        int randomX = Random.Range(0.0f, 1.0f) < 0.5f ? -1 : 1;
        int randomY = Random.Range(0.0f, 1.0f) < 0.5f ? -1 : 1;

        Vector3 strafePos = new Vector3(transform.position.x + distanceFromAI * randomX, transform.position.y + distanceFromAI * randomY, transform.position.z + distanceFromAI);

        _velocityAI = (strafePos - transform.position).normalized * _speed;

        yield return new WaitForSeconds(boostDuration);

        _velocityAI = _originalvelAI = (_goal.position - transform.position).normalized * _speed;
    }
}
