using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : Singleton<PlayerController>
{
    public float moveSpeed = 2f;
    public float attackTime = 1f;

    private Rigidbody2D rb;
    private Animator animator;

    private InputAction moveAction;
    private InputAction attackAction;

    private bool isAttacking = false;
    private float attackTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");

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

        if (isAttacking)
            UpdateAttack();
    }

    private void FixedUpdate()
    {
        if (Game.isPaused)
            return;

        if (!isAttacking)
            Move();
    }

    private void UpdateAttack()
    {
        if (attackTimer < attackTime)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            isAttacking = false;
            attackTimer = 0f;
        }
    }

    private void Move()
    {
        var input = moveAction.ReadValue<Vector2>();

        rb.linearVelocity = moveSpeed * (Vector3)input;

        if (Mathf.Approximately(input.x, 0f) && Mathf.Approximately(input.y, 0f))
        {
            animator.Play("Idle");
        }
        else
        {
            animator.SetFloat("BlendX", input.x);
            animator.SetFloat("BlendY", input.y);

            animator.Play("Move");
        }
    }

    private void AttackAction_Performed(InputAction.CallbackContext obj)
    {
        if (Game.isPaused)
            return;

        isAttacking = true;

        rb.linearVelocity = Vector2.zero;

        animator.Play("Attack");
    }
}
