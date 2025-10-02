using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 1f;

    private void Update()
    {
        transform.position += speed * Time.deltaTime * (Vector3)direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.TryGetComponent<IInteractable>(out var interactable))
            return;

        interactable.Interact();
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
