using UnityEngine;

public class UnpauseButton : PopMenuButton
{
    protected override void OnClick()
    {
        base.OnClick();

        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
