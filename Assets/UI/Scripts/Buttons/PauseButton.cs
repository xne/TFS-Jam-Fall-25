public class PauseButton : PushMenuButton
{
    protected override void OnClick()
    {
        Game.Pause();
        base.OnClick();
    }
}
