using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private string level = "Scenes/Levels/Level1";
    public string Level => level;

    private bool isLoading = false;

    private void Start()
    {
        LoadLevel(level);
    }

    public void LoadLevel(string nextLevel)
    {
        if (isLoading)
        {
            Debug.LogWarning("Already loading a level");
            return;
        }

        if (string.IsNullOrEmpty(nextLevel))
        {
            Debug.LogWarning("Level is null");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            Debug.LogWarning($"Level cannot be loaded");
            return;
        }

        StartCoroutine(LoadLevelCoroutine(nextLevel));
    }

    private IEnumerator LoadLevelCoroutine(string nextLevel)
    {
        Game.Pause();
        isLoading = true;

        if (SceneManager.GetSceneByName(level).isLoaded)
        {
            var unloadOp = SceneManager.UnloadSceneAsync(level);
            while (!unloadOp.isDone)
                yield return null;
        }

        var loadOp = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;

        level = nextLevel;

        isLoading = false;
        Game.Unpause();
    }
}
