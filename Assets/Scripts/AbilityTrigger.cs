using System;
using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayerAbilities _abilities;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private Ability _ability;

    void Start()
    {
        if (_abilities == null) throw new NullReferenceException("Player Abilities cannot be null!");
        if (_collider == null) throw new NullReferenceException("Collider cannot be null!");

        _collider.isTrigger = true;
    }

    void OnTriggerEnter()
    {
        _abilities.Unlock(_ability);
    }
}
