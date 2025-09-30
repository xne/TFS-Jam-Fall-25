using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Animator animator;

    private InputAction moveAction;

    private void Start()
    {
        animator = GetComponent<Animator>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        var input = moveAction.ReadValue<Vector2>();
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
}
