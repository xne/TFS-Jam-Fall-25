using UnityEngine;

public abstract class PersistentSingleton<T> : Singleton<T> where T : Component
{
    protected override void Awake()
    {
        base.Awake();

        if (gameObject == null)
            return;

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}
