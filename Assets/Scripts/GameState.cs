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


    [SerializeField] Material[] _relativityMaterials = null;

    public static readonly float LIGHTSPEED = 20.0f;
    public static readonly float LIGHTSPEED_SPQUARED = 400.0f;

    private GameObject _player = null;
    public Vector3 PlayerVelocity
    { get { return _player.GetComponent<SpaceshipMovement>().PlayerVelocity; } }


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
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdatePlayerStatus(Vector3 playerPos, Vector3 playerVel)
    {
        //Weird jittering occurs if only z-vel !=0
        if (Mathf.Approximately(playerVel.x, 0.0f) && Mathf.Approximately(playerVel.y, 0.0f))
            playerVel.x = playerVel.y = 0.001f;

        foreach (Material material in _relativityMaterials)
        {
            material.SetVector("_PlayerVelocity", playerVel / LIGHTSPEED);
            material.SetVector("_PlayerPosition", playerPos);
        }
    }

    public void OnApplicationQuit()
    {
        foreach (Material material in _relativityMaterials)
        {
            material.SetVector("_PlayerVelocity", new Vector3(0.001f, 0.001f, 0)); //v/c
            material.SetVector("_PlayerPosition", Vector3.zero);
        }
    }
}
