using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public SceneReference _mainScene;
    public SceneReference _endScene;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        StartCoroutine(LoadEndScene());
        GlobalCamera.instance.SetZoom(6);
    }

    IEnumerator LoadEndScene() {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.path == _mainScene.ScenePath) continue;
            if (scene.path == _endScene.ScenePath) continue;

            var task = SceneManager.UnloadSceneAsync(scene);
            while (!task.isDone) yield return null;
        }

        var load = SceneManager.LoadSceneAsync(_endScene);
        while (!load.isDone) yield return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_collider.center, _collider.radius);
    }
}
