using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativisticObject : MonoBehaviour
{
    private GameObject _player = null;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();

        Vector3 playerVel = _player.GetComponent<SpaceshipMovement>().PlayerVelocity;
        Vector3 playerPos = _player.transform.position;

        //Weird jittering occurs if only z-vel !=0
        if ((playerVel.x < 0.001f && playerVel.x > -0.001f) && (playerVel.y < 0.001f && playerVel.y > -0.001f))
            playerVel.x = playerVel.y = 0.001f;

        renderer.material.SetVector("_PlayerVelocity", playerVel /5); //v/c
        renderer.material.SetVector("_PlayerPosition", playerPos);
    }
}
