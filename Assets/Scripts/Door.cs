using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string level = "Scenes/Levels/Level1";
    [SerializeField] private Vector2 playerPosition;
    public bool locked = false;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (locked)
            sr.sprite = lockedSprite;
    }

    public void Unlock()
    {
        locked = false;
        sr.sprite = unlockedSprite;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !locked)
        {
            LevelManager.Instance.LoadLevel(level, playerPosition);
        }
    }
}
