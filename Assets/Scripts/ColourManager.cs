using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeepObject))]
public class ColourManager : MonoBehaviour
{
    public static ColourManager instance;

    [SerializeField]
    private float _transitionSpeed = 4f;

    [HideInInspector]
    public bool HasColour;

    private float _lerpValue;

    [Serializable]
    struct ColourMaterial {
        public Material Material;

        [NonSerialized]
        public Color Original;
        public Color Target;
    }

    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private Color _skyboxTarget;
    private Color _originalSkybox;

    [SerializeField]
    private List<ColourMaterial> _materials = new List<ColourMaterial>();

    void Start() {
        instance = this;

        for (int i = 0; i < _materials.Count; i++) {
            var r = _materials[i];
            r.Original =  r.Material.GetColor("_BaseColor");

            _materials[i] = r;
        }

        _originalSkybox = _cam.backgroundColor;
    }

    void Update() {
        float target = HasColour ? 1f : 0f;
        if (target == _lerpValue) return;

        _lerpValue = Mathf.Lerp(_lerpValue, target, Time.deltaTime * _transitionSpeed);

        const float minmax = 0.0005f;
        if (_lerpValue <= minmax) _lerpValue = 0f;
        if (_lerpValue >= 1f - minmax) _lerpValue = 1f;

        foreach (var r in _materials) {
            r.Material.SetColor("_BaseColor", Color.Lerp(r.Original, r.Target, _lerpValue));
        }

        _cam.backgroundColor = Color.Lerp(_originalSkybox, _skyboxTarget, _lerpValue);
    }

    void OnDestroy() {
        foreach (var r in _materials) {
            r.Material.SetColor("_BaseColor", r.Original);
        }
    }
}
