using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameState : MonoBehaviour
{
    private static readonly object padlock = new object();
    private static GameState instance = null;
    public static GameState Instance
    {
        get { return instance; }
    }

    private const float LIGHTSPEED = 10.0f;

    [SerializeField] Material[] _relativityMaterials = null;

    private void Awake()
    {
        lock (padlock)
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }
    }

    public void UpdatePlayerStatus(Vector3 playerPos, Vector3 playerVel)
    {
        //Weird jittering occurs if only z-vel !=0
        if ((playerVel.x < 0.001f && playerVel.x > -0.001f) && (playerVel.y < 0.001f && playerVel.y > -0.001f))
            playerVel.x = playerVel.y = 0.001f;

        foreach (Material material in _relativityMaterials)
        {
            material.SetVector("_PlayerVelocity", playerVel / LIGHTSPEED); //v/c
            material.SetVector("_PlayerPosition", playerPos);
        }
    }
}
