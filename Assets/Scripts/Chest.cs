using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        var player = PlayerController.Instance;

        player.PerformAction();

        player.animator.Play("PickupWand");

        player.hasWand = true;

        Destroy(this);
    }
}
