using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.root.gameObject);
    }
}
