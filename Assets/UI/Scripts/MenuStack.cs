using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MenuStack : MonoBehaviour
{
    [SerializeField] protected Menu firstMenu;

    protected Stack<Menu> menuStack = new();

    protected void Start()
    {
        if (firstMenu == null)
        {
            Debug.LogWarning("First menu is null");
            return;
        }

        menuStack.Push(firstMenu);
    }

    public void Push(Menu nextMenu)
    {
        if (nextMenu == null)
        {
            Debug.LogError("Next menu is null");
            return;
        }

        if (menuStack.TryPeek(out var menu))
            menu.gameObject.SetActive(false);

        menuStack.Push(nextMenu);
        nextMenu.gameObject.SetActive(true);
    }

    public Menu Pop()
    {
        if (!menuStack.TryPop(out var menu))
        {
            Debug.LogError("Menu stack is empty");
            return null;
        }

        menu.gameObject.SetActive(false);

        if (menuStack.TryPeek(out var nextMenu))
            nextMenu.gameObject.SetActive(true);
        else
            Debug.LogWarning("Menu stack is empty");

        return menu;
    }

    public Menu Peek() => menuStack.Peek();

    public void Clear()
    {
        while (menuStack.Count > 0)
            menuStack.Pop().gameObject.SetActive(false);
    }
}
