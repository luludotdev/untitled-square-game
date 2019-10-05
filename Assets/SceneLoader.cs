using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(KeepObject))]
public class SceneLoader : MonoBehaviour
{
    public List<SceneReference> _scenes;

    void Start()
    {
        foreach (SceneReference _scene in _scenes) {
            Scene scene = SceneManager.GetSceneByPath(_scene.ScenePath);
            if (scene.isLoaded) continue;

            SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);
        }
    }
}
