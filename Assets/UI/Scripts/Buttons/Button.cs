using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public abstract class Button : MonoBehaviour
{
    UnityEngine.UI.Button button;

    protected Menu menu;
    protected MenuStack menuStack;

    protected virtual void Start()
    {
        menu = GetComponentInParent<Menu>();

        if (menu == null)
        {
            Debug.LogError($"{name} is not part of a menu");
            return;
        }

        menuStack = menu.GetComponentInParent<MenuStack>();

        if (menuStack == null)
        {
            Debug.LogError($"{menu.name} is not part of a menu stack");
            return;
        }

        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
