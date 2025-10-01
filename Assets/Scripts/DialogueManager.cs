using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private MenuStack menuStack;
    [SerializeField] private Menu dialogueMenu;

    [SerializeField] private TMP_Text nameplateText;
    [SerializeField] private TMP_Text textboxText;
    [SerializeField] private TMP_Text option1Text;
    [SerializeField] private TMP_Text option2Text;

    [SerializeField] private TextAsset inkJSONAsset;
    public Story story;

    public void PushDialogue()
    {
        menuStack.Push(dialogueMenu);
        StartStory(inkJSONAsset.text);
    }

    public void StartStory(string text)
    {
        story = new Story(text);
        RefreshView();
    }

    public void RefreshView()
    {
        while (story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            RefreshContentView(text);
        }

        if (story.currentChoices.Count > 0)
        {
            option1Text.text = story.currentChoices[0].text.Trim();
            option2Text.text = story.currentChoices[1].text.Trim();
        }
        else
        {
            menuStack.Pop();
            Game.Unpause();
        }
    }

    private void RefreshContentView(string text)
    {
        textboxText.text = text;
    }
}
