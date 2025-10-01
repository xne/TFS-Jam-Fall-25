using UnityEngine;

public class StoryOptionButton : Button
{
    public int choiceIndex;

    protected override void OnClick()
    {
        var dialogueManager = DialogueManager.Instance;
        if (dialogueManager.story == null)
        {
            Debug.LogError("Story is null");
            return;
        }

        dialogueManager.story.ChooseChoiceIndex(choiceIndex);
        dialogueManager.RefreshView();
    }
}
