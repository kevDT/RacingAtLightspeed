using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovingObject : MonoBehaviour
{
    [SerializeField] GameObject _template = null;

    private bool _alive = true;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        float timeBetweenObjects = 3.0f;

        while (_alive)
        {
            yield return new WaitForSeconds(timeBetweenObjects);

            _spawnedObjects.Add(Instantiate(_template, transform.position, Quaternion.identity));

            yield return null;
        }
    }
}
