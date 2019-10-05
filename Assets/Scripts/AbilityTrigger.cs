using System;
using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    private PlayerAbilities _abilities;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private Ability _ability;

    void Start()
    {
        if (_collider == null) throw new NullReferenceException("Collider cannot be null!");

        _abilities = PlayerAbilities.instance;
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        _abilities.Unlock(_ability);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
