using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private MenuStack menuStack;
    [SerializeField] private Menu dialogueMenu;

    private void Start()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;

        menuStack.Push(dialogueMenu);
    }
}
