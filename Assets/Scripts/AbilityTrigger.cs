using System;
using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    private PlayerAbilities _abilities;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private Ability _ability;

    [SerializeField]
    private CanvasGroup _canvas;

    [SerializeField]
    private float _canvasFadeMultiplier = 1.5f;

    private bool _canvasVisible = false;

    void Start()
    {
        if (_collider == null) throw new NullReferenceException("Collider cannot be null!");

        _abilities = PlayerAbilities.instance;
        _collider.isTrigger = true;

        if (_canvas != null) _canvas.alpha = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        _abilities.Unlock(_ability);
        _canvasVisible = true;
    }

    void Update() {
        float target = _canvasVisible ? 1f : 0f;

        if (_canvas)
            _canvas.alpha = Mathf.Lerp(_canvas.alpha, target, Time.deltaTime * _canvasFadeMultiplier);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
