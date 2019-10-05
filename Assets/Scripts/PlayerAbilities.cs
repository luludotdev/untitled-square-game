using System;
using UnityEngine;
using UnityEngine.Events;

[Flags] public enum Ability {
    None = 0,
    MoveRight = 1 << 0,
    MoveLeft = 1 << 1,
    Jump = 1 << 2,
    Crouch = 1 << 3,
    DoubleJump = 1 << 4,
}

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities instance;

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

    void Start() {
        instance = this;
    }
}
