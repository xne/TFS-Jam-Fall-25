using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MenuStack menuStack;
    [SerializeField] private Menu pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Game.isPaused)
            {
                if (menuStack.Count > 2)
                    menuStack.Pop();
                else
                {
                    menuStack.Pop();
                    Game.Unpause();
                }
            }
            else
            {
                Game.Pause();
                menuStack.Push(pauseMenu);
            }
        }
    }
}
