using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public Sprite activatedSprite;

    private SpriteRenderer sr;

    private bool activated = false;
    public bool Activated => activated;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        activated = true;

        sr.sprite = activatedSprite;
    }
}
