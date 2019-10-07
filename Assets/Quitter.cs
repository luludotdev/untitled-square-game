using System;
using UnityEngine;

[RequireComponent(typeof(KeepObject))]
public class Quitter : MonoBehaviour
{
    private PlayerInput _input;

    [SerializeField]
    private CanvasGroup _group;

    void Awake() {
        _input = new PlayerInput();
        _group.alpha = 0f;

        _input.Player.Quit.started += ctx => {
            _group.alpha = 1f;
        };

        _input.Player.Quit.performed += ctx => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        };

        _input.Player.Quit.canceled += ctx => {
            _group.alpha = 0f;
        };
    }

    void OnEnable()
    {
        _input.Player.Enable();
    }

    void OnDisable()
    {
        _input.Player.Disable();
    }
}
