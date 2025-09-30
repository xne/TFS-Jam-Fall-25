using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string level = "Scenes/Levels/Level1";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Game.Pause();
            LevelManager.Instance.LoadLevel(level);
        }
    }
}
