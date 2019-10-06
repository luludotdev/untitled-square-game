using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class DeathTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _collider;

    public UnityEvent OnDeath;

    void Start() {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        SpawnManager.instance.Respawn();
        OnDeath?.Invoke();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
