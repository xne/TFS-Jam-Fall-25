using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Boot
{
    private const string BootScene = "Boot";
    private const string FirstScene = "MainMenu";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeSceneLoad()
    {
        if (SceneManager.GetActiveScene().name != BootScene)
            SceneManager.LoadScene(BootScene, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(FirstScene);
    }
}
