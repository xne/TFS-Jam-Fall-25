using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string level = "Scenes/Levels/Level1";
    [SerializeField] private Vector2 playerPosition;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            LevelManager.Instance.LoadLevel(level, playerPosition);
        }
    }
}
