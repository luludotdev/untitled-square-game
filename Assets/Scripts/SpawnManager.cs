using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField]
    private Rigidbody _player;

    private Vector3 _spawnPoint;

    public UnityEvent OnRespawn;
    public UnityEvent OnSpawnChanged;

    void Awake()
    {
        instance = this;
        _spawnPoint = _player.transform.position;
    }

    public void Respawn() {
        _player.velocity = Vector3.zero;
        _player.transform.position = _spawnPoint;

        OnRespawn?.Invoke();
    }

    public void SetSpawnPoint(Vector3 spawnPoint) {
        _spawnPoint = spawnPoint;
        OnSpawnChanged?.Invoke();
    }
}
