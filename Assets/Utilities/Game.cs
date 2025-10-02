using UnityEngine;

public static class Game
{
    public static bool isPaused = false;

    public static void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public static void Unpause()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}
