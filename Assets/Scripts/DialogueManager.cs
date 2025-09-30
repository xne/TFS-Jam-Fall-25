using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private MenuStack menuStack;
    [SerializeField] private Menu dialogueMenu;

    private void Start()
    {
        Game.Pause();
        menuStack.Push(dialogueMenu);
    }
}
