using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private MenuStack menuStack;
    [SerializeField] private Menu dialogueMenu;

    public void PushDialogue()
    {
        menuStack.Push(dialogueMenu);
    }
}
