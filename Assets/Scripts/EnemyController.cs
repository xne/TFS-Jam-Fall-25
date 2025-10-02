using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IInteractable
{
    [SerializeField] protected int health = 3;
    [SerializeField] protected float actionTime = 1f;

    protected float actionTimer;

    protected bool takingAction = false;

    protected virtual void Update()
    {
        if (!takingAction)
        {
            actionTimer += Time.deltaTime;
            if (actionTimer >= actionTime)
            {
                actionTimer = 0;
                takingAction = true;
                TakeAction();
            }
        }
    }

    protected abstract void TakeAction();

    public virtual void Interact()
    {
        if (--health == 0)
        {
            Destroy(gameObject);
        }
    }
}
