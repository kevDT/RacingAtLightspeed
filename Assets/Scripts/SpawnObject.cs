using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    [SerializeField] private GameObject _object = null;

    // Update is called once per frame
    void Update()
    {
        Instantiate(_object);
    }
}
