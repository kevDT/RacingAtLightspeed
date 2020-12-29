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


    public static readonly float LIGHTSPEED = 20.0f;
    public static readonly float LIGHTSPEED_SQUARED = 400.0f;

    private SpaceshipMovement _playerMovement = null;
    public Vector3 PlayerVelocity
    { get { return _playerMovement.PlayerVelocity; } }
    public Vector3 PlayerPosition
    { get { return _playerMovement.PlayerPosition; } }


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

    private void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceshipMovement>();
    }

    public void UpdatePlayerStatus(Vector3 playerPos, Vector3 playerVel)
    {
        //Weird jittering occurs if only z-vel !=0
        if (Mathf.Approximately(playerVel.x, 0.0f) && Mathf.Approximately(playerVel.y, 0.0f))
            playerVel.x = playerVel.y = 0.001f;

        Shader.SetGlobalVector("_PlayerVelocity", playerVel / LIGHTSPEED);
        Shader.SetGlobalVector("_PlayerPosition", playerPos);
    }

    public void OnApplicationQuit()
    {
        Shader.SetGlobalVector("_PlayerVelocity", new Vector3(0.001f, 0.001f, 0));
        Shader.SetGlobalVector("_PlayerPosition", Vector3.zero);
    }
}
