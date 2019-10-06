using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColourTrigger : MonoBehaviour
{
    private ColourManager _manager;

    private enum TriggerType {
        Enable,
        Disable,
    }

    [SerializeField]
    private TriggerType _type;

    [SerializeField]
    private BoxCollider _collider;

    void Start() {
        _manager = ColourManager.instance;
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        bool enabled = _type == TriggerType.Enable;
        _manager.HasColour = enabled;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
