public class PopMenuButton : Button
{
    protected override void OnClick() => menuStack.Pop();
}
