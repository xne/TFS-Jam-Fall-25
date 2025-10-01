public class StoryAdvanceButton : Button
{
    protected override void OnClick()
    {
        var dialogueManager = DialogueManager.Instance;
        dialogueManager.RefreshView();
    }
}
