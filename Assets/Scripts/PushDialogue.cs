using UnityEngine;

public class PushDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSONAsset;
    [SerializeField] private string characterName;
    [SerializeField] private Sprite characterSprite;

    void Start()
    {
        DialogueManager.Instance.PushDialogue(inkJSONAsset, characterName, characterSprite);
    }
}
