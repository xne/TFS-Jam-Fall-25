using UnityEngine;

public class PushMenuButton : Button
{
    public Menu nextMenu;

    protected override void Start()
    {
        if (nextMenu == null)
        {
            Debug.LogError($"{name} does not have a next menu reference");
            return;
        }

        base.Start();
    }

    protected override void OnClick() => menuStack.Push(nextMenu);
}
