using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    [Range(0.0005f, 10f)]
    private float _speed = 1f;

    [DraggablePoint]
    public Vector3 _position1;
    [DraggablePoint]
    public Vector3 _position2;

    void Update() {
        float sin = (Mathf.Sin(Time.time * _speed) + 1f) * 0.5f;
        transform.position = Vector3.Lerp(_position1, _position2, sin);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_position1, _position2);
    }
}
