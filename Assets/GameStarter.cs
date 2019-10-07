using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private SceneReference _mainScene;

    void Awake() {
        string ngpPath = Path.Combine(Application.persistentDataPath, "newgameplus.dat");
        bool showNewGamePlus = File.Exists(ngpPath);

        // Do stuff
    }

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

        PlayerAbilities.instance.UnlockAll();
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
