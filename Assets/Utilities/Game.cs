using UnityEngine;

public static class Game
{
    public static bool isPaused = false;

    public static void Pause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        isPaused = true;
    }

    public static void Unpause()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPaused = false;
    }
}
