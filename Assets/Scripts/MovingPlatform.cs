using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    [Range(0.0005f, 10f)]
    private float _speed = 1f;

    [SerializeField]
    private float _startOffset = 0f;

    [DraggablePoint]
    public Vector3 _position1;
    [DraggablePoint]
    public Vector3 _position2;

    private Scene? _scene;

    void Update() {
        float sin = (Mathf.Sin((Time.time + _startOffset) * _speed) + 1f) * 0.5f;
        transform.position = Vector3.Lerp(_position1, _position2, sin);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_position1, _position2);
    }

    void OnCollisionEnter(Collision col) {
        if (col.transform.tag != "Player") return;

        _scene = col.gameObject.scene;
        col.transform.parent = transform;
    }

    void OnCollisionExit(Collision col) {
        if (col.transform.tag != "Player") return;
        if (_scene == null) throw new NullReferenceException();

        col.transform.parent = null;
        SceneManager.MoveGameObjectToScene(col.gameObject, (Scene)_scene);
        _scene = null;
    }
}
