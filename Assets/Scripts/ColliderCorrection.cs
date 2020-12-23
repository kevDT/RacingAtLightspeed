using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCorrection : MonoBehaviour
{
    private Collider _collider = null;

    private Vector3 _offset = Vector3.zero;

    private void Awake()
    {
        _collider = GetComponent<Collider>();

        _offset = transform.TransformPoint(GetComponent<MeshFilter>().mesh.vertices[0]);
    }
    private void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        //_collider.transform.position = transform.position - (transform.TransformPoint(mesh.vertices[0]) - _offset);
        Debug.Log(transform.TransformPoint(mesh.vertices[0]));
        mesh.vertices[0] = mesh.vertices[0] + new Vector3(2, 2, 2);
    }
}
