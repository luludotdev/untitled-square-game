using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LoadUnloadTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _collider;

    private enum LoadType {
        Load,
        Unload,
    }

    [Serializable]
    private struct LoadStruct {
        public GameObject Object;
        public LoadType LoadType;
    }

    [SerializeField]
    private List<LoadStruct> _objects;

    void Start() {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        foreach (LoadStruct s in _objects) {
            if (s.LoadType == LoadType.Load) s.Object.SetActive(true);
            if (s.LoadType == LoadType.Unload) s.Object.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, .753f, 1f);
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
