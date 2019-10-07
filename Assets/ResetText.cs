using System;
using System.Collections.Generic;
using UnityEngine;

public class ResetText : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objects;

    void Start() {
        foreach (var obj in _objects) {
            obj.SetActive(false);
        }
    }

    public void Trigger() {
        PlayerAbilities.instance.Unlock(Ability.Reset);

        foreach (var obj in _objects) {
            obj.SetActive(true);
        }
    }
}
