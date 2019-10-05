using System;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private GlobalCamera _cam;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private float _orthographicSize = 1f;

    void Start()
    {
        if (_collider == null) throw new NullReferenceException("Collider cannot be null!");

        _cam = GlobalCamera.instance;
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        _cam.SetZoom(_orthographicSize);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
