using UnityEngine;

public class RaspbController : EnemyController
{
    private SpriteRenderer sr;

    public Sprite defaultSprite;
    public Sprite targetSprite;
    public Sprite jumpSprite;

    public float targetingTime = 2f;
    private float targetingTimer;

    public float jumpSpeed = 5f;
    public float targetSpeed = 3f;

    private GameObject targetPrefab;
    private GameObject target;

    private enum Action
    {
        Target,
        Jump,
        None
    }

    private Action currentAction = Action.None;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        targetPrefab = Resources.Load<GameObject>("Prefabs/Target");
    }

    protected override void Update()
    {
        base.Update();

        if (currentAction == Action.Target)
        {
            targetingTimer += Time.deltaTime;
            var direction = (PlayerController.Instance.transform.position - target.transform.position).normalized;
            target.transform.position += targetSpeed * Time.deltaTime * direction;
            if (targetingTimer > targetingTime)
            {
                targetingTimer = 0;
                Jump();
                currentAction = Action.Jump;
            }
        }
        else if (currentAction == Action.Jump)
        {
            var difference = (target.transform.position - transform.position);
            var direction = difference.normalized;
            var magnitude = difference.magnitude;
            if (magnitude > 0.1f)
            {
                transform.position += jumpSpeed * Time.deltaTime * direction;
            }
            else
            {
                Destroy(target);
                sr.sprite = defaultSprite;
                currentAction = Action.None;
                takingAction = false;
            }
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (target)
            Destroy(target);
        targetingTimer = 0f;
        sr.sprite = defaultSprite;
        currentAction = Action.None;
        takingAction = false;
    }

    protected override void TakeAction()
    {
        Target();
        currentAction = Action.Target;
    }

    private void Target()
    {
        Debug.Log("Target");
        sr.sprite = targetSprite;
        target = Instantiate(targetPrefab, transform.position, Quaternion.identity);
    }

    private void Jump()
    {
        Debug.Log("Jump");
        sr.sprite = jumpSprite;
    }
}
