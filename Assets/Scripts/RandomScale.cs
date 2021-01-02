using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleMin = Vector3.zero;
    [SerializeField] private Vector3 _scaleMax = Vector3.one;

    private void Awake()
    {
        float scaleX = Random.Range(_scaleMin.x, _scaleMax.x);
        float scaleY = Random.Range(_scaleMin.y, _scaleMax.y);
        float scaleZ = Random.Range(_scaleMin.z, _scaleMax.z);

        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
