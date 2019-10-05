using System;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(KeepObject))]
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class GlobalCamera : MonoBehaviour
{
    public static GlobalCamera instance;

    public CinemachineVirtualCamera VirtualCamera;

    [SerializeField]
    [Range(1f, 10f)]
    private float _zoomSpeed = 3f;
    private float _targetSize;

    public float Zoom => _targetSize;

    void Start() {
        instance = this;

        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _targetSize = VirtualCamera.m_Lens.OrthographicSize;
    }

    void Update() {
        if (VirtualCamera.m_Lens.OrthographicSize == _targetSize) return;

        VirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(VirtualCamera.m_Lens.OrthographicSize, _targetSize, Time.deltaTime * _zoomSpeed);
    }

    public void SetZoom(float zoom) {
        _targetSize = zoom;
    }
}
