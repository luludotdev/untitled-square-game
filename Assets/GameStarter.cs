using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private SceneReference _mainScene;

    public void StartGame() {
        SceneManager.LoadSceneAsync(_mainScene);
    }

    public void StartGamePlus() {
        StartCoroutine(StartGamePlusAsync());
    }

    IEnumerator StartGamePlusAsync() {
        DontDestroyOnLoad(this);

        var task = SceneManager.LoadSceneAsync(_mainScene);
        while (!task.isDone) yield return null;

        // Do stuff

        Destroy(this);
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
