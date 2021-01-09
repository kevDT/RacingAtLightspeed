using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = Vector3.zero;

    public virtual Vector3 ObjectVelocity
    { 
        get { return _velocity; }
    }

    public virtual void Update()
    {
        transform.Translate(_velocity * Time.smoothDeltaTime);
        GetComponentInChildren<Renderer>().material.SetVector("_ObjectVelocity", _velocity / GameState.LIGHTSPEED);
    }
}
