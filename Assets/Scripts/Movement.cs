using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = Vector3.zero;

    public Vector3 ObjectVelocity
    { 
        get { return _velocity; }
        set { _velocity = value; }  
    }

    private void Update()
    {
        transform.Translate(_velocity * Time.deltaTime);
        GetComponentInChildren<Renderer>().material.SetVector("_ObjectVelocity", _velocity / GameState.LIGHTSPEED);
    }
}
