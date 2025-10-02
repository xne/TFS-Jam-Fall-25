using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private MenuStack menuStack;
    [SerializeField] private Menu dialogueMenu;

    [SerializeField] private TMP_Text nameplateText;
    [SerializeField] private TMP_Text textboxText;
    [SerializeField] private UnityEngine.UI.Button option1Button;
    [SerializeField] private TMP_Text option1Text;
    [SerializeField] private UnityEngine.UI.Button option2Button;
    [SerializeField] private TMP_Text option2Text;
    [SerializeField] private UnityEngine.UI.Button advanceButton;
    [SerializeField] private Image characterImage;

    public Story story;
    public bool IsActive => dialogueMenu.isActiveAndEnabled;

    public void PushDialogue(TextAsset inkJSONAsset, string characterName, Sprite characterSprite)
    {
        nameplateText.text = characterName;
        characterImage.sprite = characterSprite;
        characterImage.SetNativeSize();
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
        if (!(story.canContinue || story.currentChoices.Count > 0))
        {
            menuStack.Pop();
            Game.Unpause();
        }

        if (story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            RefreshContentView(text);
            foreach (var tag in story.currentTags)
            {
                if (tag.StartsWith("SFX:"))
                {
                    var sfxName = tag[4..];

                    AudioManager.Instance.Play($"Audio/{sfxName}");
                }
            }
        }
        
        if (story.currentChoices.Count > 0)
        {
            option1Button.gameObject.SetActive(true);
            option1Text.text = story.currentChoices[0].text.Trim();

            option2Button.gameObject.SetActive(true);
            option2Text.text = story.currentChoices[1].text.Trim();

            advanceButton.gameObject.SetActive(false);
        }
        else
        {
            option1Button.gameObject.SetActive(false);
            option2Button.gameObject.SetActive(false);

            advanceButton.gameObject.SetActive(true);
        }
    }

    private void RefreshContentView(string text)
    {
        textboxText.text = text;
    }
}
