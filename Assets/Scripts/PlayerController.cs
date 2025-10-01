using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : Singleton<PlayerController>
{
    public float moveSpeed = 3f;
    public float actionTime = 0.25f;
    public float attackDistance = 0.5f;
    public LayerMask interactableMask;
    public bool hasWand = false;

    private Rigidbody2D rb;
    [HideInInspector] public Animator animator;

    private InputAction moveAction;
    private InputAction attackAction;

    private bool isPerformingAction = false;
    private float actionTimer = 0f;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");

        interactableMask = LayerMask.GetMask("Interactable");

        attackAction.performed += AttackAction_Performed;
    }

    private void OnDestroy()
    {
        attackAction.performed -= AttackAction_Performed;
    }

    private void Update()
    {
        if (Game.isPaused)
            return;

        if (isPerformingAction)
            UpdateAction();
    }

    private void FixedUpdate()
    {
        if (Game.isPaused)
            return;

        if (!isPerformingAction)
            Move();
    }

    private void UpdateAction()
    {
        if (actionTimer < actionTime)
        {
            actionTimer += Time.deltaTime;
        }
        else
        {
            isPerformingAction = false;
            actionTimer = 0f;
        }
    }

    private void Move()
    {
        var input = moveAction.ReadValue<Vector2>();

        rb.linearVelocity = moveSpeed * (Vector3)input;

        var inputX = !Mathf.Approximately(input.x, 0f);
        var inputY = !Mathf.Approximately(input.y, 0f);
        if (!inputX && !inputY)
        {
            animator.Play("Idle");
        }
        else
        {
            direction = inputY ? new(0f, Mathf.Sign(input.y)) : new(Mathf.Sign(input.x), 0f);

            animator.SetFloat("BlendX", input.x);
            animator.SetFloat("BlendY", input.y);

            animator.Play("Move");
        }
    }

    public void PerformAction()
    {
        isPerformingAction = true;

        rb.linearVelocity = Vector2.zero;
    }

    private void AttackAction_Performed(InputAction.CallbackContext obj)
    {
        if (Game.isPaused)
            return;

        PerformAction();

        if (hasWand)
        {
            animator.Play("RangedAttack");
        }
        else
        {
            animator.Play("Attack");

            var hit = Physics2D.Raycast(transform.position, direction, attackDistance, interactableMask);
            if (hit)
            {
                if (!hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    Debug.LogError("GameObjects in the Interactable layer must implement interface IInteractable");
                    return;
                }

                interactable.Interact();
            }
        }
    }
}
