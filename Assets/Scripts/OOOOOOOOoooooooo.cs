using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OOOOOOOOoooooooo : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    private float _fadeSpeed = 10f;

    private AudioSource _oooo;

    void Start() {
        _oooo = GetComponent<AudioSource>();
    }

    void Update() {
        if (_oooo.isPlaying && _oooo.volume < 1f)
            _oooo.volume += Time.deltaTime / _fadeSpeed;
        else if (_oooo.volume > 1f)
            _oooo.volume = 1f;
    }

    public void OoooTime(Ability ability) {
        if (ability == Ability.Sound)
            _oooo.Play();
    }
}
