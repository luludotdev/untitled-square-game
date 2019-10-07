using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class DeathTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _collider;

    [SerializeField]
    private Material _material;

    public UnityEvent OnDeath;

    private MeshFilter _filter;
    private MeshRenderer _renderer;

    void Start() {
        _filter = gameObject.AddComponent<MeshFilter>();
        _renderer = gameObject.AddComponent<MeshRenderer>();

        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;

        if (_material) {
            Mesh mesh = new Mesh();

            Vector3 center = Vector3.zero;
            float x = _collider.bounds.size.x / 2;
            float y = _collider.bounds.size.y / 2;
            float z = (_collider.bounds.size.z / 2) - 0.1f;

            mesh.vertices = new Vector3[8] {
                new Vector3(center.x + x, center.y + y, center.z + z),
                new Vector3(center.x + x, center.y + y, center.z - z), // Top Right
                new Vector3(center.x - x, center.y + y, center.z + z),
                new Vector3(center.x - x, center.y + y, center.z - z), // Top Left
                new Vector3(center.x + x, center.y - y, center.z + z),
                new Vector3(center.x + x, center.y - y, center.z - z), // Bottom Right
                new Vector3(center.x - x, center.y - y, center.z + z),
                new Vector3(center.x - x, center.y - y, center.z - z), // Bottom Left
            };

            mesh.triangles = new int[] {
                0,
                1,
                2,
                2,
                1,
                3,
                3,
                1,
                7,
                1,
                5,
                7,
            };

            mesh.RecalculateNormals();
            _filter.mesh = mesh;
            _renderer.material = _material;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        SpawnManager.instance.Respawn();
        OnDeath?.Invoke();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
