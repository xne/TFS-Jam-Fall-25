using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : Button
{
    public string scene;

    protected override void Start()
    {
        if (string.IsNullOrEmpty(scene))
        {
            Debug.LogError($"{name} does not have a scene reference");
            return;
        }

        base.Start();
    }

    protected override void OnClick() => SceneManager.LoadScene(scene);
}
