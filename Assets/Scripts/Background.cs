using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Transform _background;

    [SerializeField]
    private Vector2 _bounds = new Vector2(1f, 1f);

    void Start()
    {
        if (_player == null) throw new NullReferenceException("Player cannot be null!");
        if (_background == null) throw new NullReferenceException("Background cannot be null!");
    }

    void Update()
    {
        if (_player.position.x > _background.position.x + _bounds.x) {
            _background.position += new Vector3(_bounds.x, 0f, 0f);
        }

        if (_player.position.x < _background.position.x - _bounds.x) {
            _background.position -= new Vector3(_bounds.x, 0f, 0f);
        }

        if (_player.position.y > _background.position.y + _bounds.y) {
            _background.position += new Vector3(0f, _bounds.y, 0f);
        }

        if (_player.position.y < _background.position.y - _bounds.y) {
            _background.position -= new Vector3(0f, _bounds.y, 0f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(_bounds.x, _bounds.y, .1f));
    }
}
