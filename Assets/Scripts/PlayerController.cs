using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public float moveSpeed = 2f;
    public float attackTime = 1f;

    private Animator animator;

    private InputAction moveAction;

    private bool isAttacking = false;
    private float attackTimer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();

        moveAction = InputSystem.actions.FindAction("Move");
        InputSystem.actions.FindAction("Attack").performed += AttackAction_Performed;
    }

    private void Update()
    {
        if (Game.isPaused)
            return;

        if (isAttacking)
        {
            if (attackTimer < attackTime)
            {
                attackTimer += Time.deltaTime;
                return;
            }
            else
            {
                isAttacking = false;
                attackTimer = 0f;
            }
        }

        var moveInput = moveAction.ReadValue<Vector2>();

        Move(moveInput);
    }

    private void Move(Vector2 input)
    {
        transform.position += moveSpeed * Time.deltaTime * (Vector3)input;

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
        isAttacking = true;

        animator.Play("Attack");
    }
}
