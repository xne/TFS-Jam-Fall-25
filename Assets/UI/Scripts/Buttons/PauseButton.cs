using UnityEngine;

public class PauseButton : PushMenuButton
{
    protected override void OnClick()
    {
        Time.timeScale = 0f;

        base.OnClick();
    }
}
