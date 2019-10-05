using System;
using UnityEngine;
using UnityEngine.Events;

[Flags] public enum Ability {
    None = 0,
    Jump = 1 << 0,
    DoubleJump = 1 << 1,
}

public class PlayerAbilities : MonoBehaviour
{
    public UnityAction<Ability> AbilityUnlocked;

    [SerializeField]
    private Ability _abilities;

    public bool Has(Ability ability) {
        return (_abilities & ability) == ability;
    }

    public void Unlock(Ability ability) {
        _abilities = _abilities | ability;
        AbilityUnlocked?.Invoke(ability);
    }
}
