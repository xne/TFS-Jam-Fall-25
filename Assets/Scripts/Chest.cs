using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public Sprite openedSprite;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        var player = PlayerController.Instance;

        player.PerformAction();

        player.animator.Play("PickupWand");

        player.hasWand = true;

        sr.sprite = openedSprite;

        Destroy(this);
    }
}
