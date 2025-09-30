public class UnpauseButton : PopMenuButton
{
    protected override void OnClick()
    {
        base.OnClick();
        Game.Unpause();
    }
}
