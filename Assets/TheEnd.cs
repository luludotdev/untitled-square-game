using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TheEnd : MonoBehaviour
{
    [SerializeField]
    private SphereCollider _collider;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float _speed = 1f;

    [SerializeField]
    private float _floatOffset = 0.2f;

    private Vector3 _initialPosition;

    void Start() {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;

        _initialPosition = transform.position;
    }

    void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, _speed * Time.deltaTime * -15f, _speed * Time.deltaTime * -50f);

        float sin = (Mathf.Sin(Time.time * 1.5f) + 1f) * 0.5f;
        Vector3 vectorOffset = new Vector3(0f, _floatOffset, 0f);

        Vector3 top = _initialPosition + vectorOffset;
        Vector3 bottom = _initialPosition - vectorOffset;
        transform.position = Vector3.Lerp(top, bottom, sin);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_collider.center, _collider.radius);
    }
}
