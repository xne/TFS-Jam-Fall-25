using UnityEngine;

public class QueenVexController : EnemyController
{
    public Vector2 roomExtents = new(8f, 3.5f);
    public float animTime = 0.25f;

    private CapsuleCollider2D cc;
    private SpriteRenderer sr;

    public Sprite defaultSprite;
    public Sprite actionSprite;

    private float animTimer;
    private bool isAnimating = false;

    private enum Action
    {
        Shoot,
        Teleport
    }

    private Action nextAction = Action.Shoot;

    private void Start()
    {
        cc = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        if (isAnimating)
        {
            animTimer += Time.deltaTime;
            if (animTimer >= animTime)
            {
                animTimer = 0f;
                isAnimating = false;
                takingAction = false;
                sr.sprite = defaultSprite;
                switch (nextAction)
                {
                    case Action.Shoot:
                        Shoot();
                        nextAction = Action.Teleport;
                        break;
                    case Action.Teleport:
                        Teleport();
                        nextAction = Action.Shoot;
                        break;
                }
            }
        }
    }

    public override void Interact()
    {
        base.Interact();

        animTimer = 0f;
        isAnimating = false;
        takingAction = false;
        sr.sprite = defaultSprite;

        Teleport();
        nextAction = Action.Shoot;

        if (health == 0)
        {
            var door = FindAnyObjectByType<Door>();
            if (door)
                door.Unlock();
        }
    }

    protected override void TakeAction()
    {
        sr.sprite = actionSprite;
        isAnimating = true;
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }

    private void Teleport()
    {
        Vector2 teleportPosition;

        do teleportPosition = GetRandTeleportPosition();
        while (!IsValidTeleportPosition(teleportPosition));

        transform.position = teleportPosition;
    }

    private Vector2 GetRandTeleportPosition()
    {
        var x = Random.Range(-roomExtents.x, roomExtents.x);
        var y = Random.Range(-roomExtents.y, roomExtents.y);

        return new Vector2(x, y);
    }

    private bool IsValidTeleportPosition(Vector2 targetPos)
    {
        return !Physics2D.OverlapCapsule(
            targetPos,
            cc.size,
            cc.direction,
            0f
        );
    }
}
