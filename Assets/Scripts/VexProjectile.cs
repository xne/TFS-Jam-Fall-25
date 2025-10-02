using UnityEngine;

public class VexProjectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 1f;

    private void Update()
    {
        transform.position += speed * Time.deltaTime * (Vector3)direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
