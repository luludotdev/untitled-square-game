using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _collider;

    [DraggablePoint]
    public Vector3 _spawnPoint;

    void Start() {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        SpawnManager.instance.SetSpawnPoint(_spawnPoint);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(.118f, .933f, .039f);
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
