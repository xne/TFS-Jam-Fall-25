using UnityEngine.SceneManagement;

public class LoadSceneButton : Button
{
    public string scene;

    protected override void OnClick()
    {
        SceneManager.LoadScene(scene);
    }
}
