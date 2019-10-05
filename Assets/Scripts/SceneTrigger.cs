using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class SceneTrigger : MonoBehaviour
{
    private enum TriggerType {
        Load,
        Unload,
    }

    [SerializeField]
    private SceneReference _scene;

    [SerializeField]
    private TriggerType _type;

    [SerializeField]
    private BoxCollider _collider;

    void Start() {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter()
    {
        Scene scene = SceneManager.GetSceneByPath(_scene.ScenePath);

        if (_type == TriggerType.Load) StartCoroutine(LoadScene(scene));
        else if (_type == TriggerType.Unload) StartCoroutine(UnloadScene(scene));
    }

    IEnumerator LoadScene(Scene scene) {
        if (scene.isLoaded) yield break;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
            yield return null;
    }

    IEnumerator UnloadScene(Scene scene) {
        if (!scene.isLoaded) yield break;
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(_scene);

        while (!asyncLoad.isDone)
            yield return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
