using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private string firstLevel = "Scenes/Levels/Level1";

    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private float fadeSpeed = 1f;

    private string level;
    public string Level => level;

    private bool isFading = false;
    private bool isLoading = false;

    private string nextLevel;
    private Vector2 playerPosition;
    private bool skipFade;

    private void Start()
    {
        fadeAnimator.speed = fadeSpeed;

        LoadLevel(firstLevel, PlayerController.Instance.transform.position, true);
    }

    public void LoadLevel(string nextLevel, Vector2 playerPosition, bool skipFade = false)
    {
        if (isFading || isLoading)
        {
            Debug.LogWarning("Already loading a level");
            return;
        }

        if (string.IsNullOrEmpty(nextLevel))
        {
            Debug.LogWarning("Level is null");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            Debug.LogWarning($"Level cannot be loaded");
            return;
        }

        Game.Pause();

        this.nextLevel = nextLevel;
        this.playerPosition = playerPosition;
        this.skipFade = skipFade;

        if (skipFade)
            StartCoroutine(LoadLevelCoroutine());
        else
            StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        isFading = true;

        fadeAnimator.Play("FadeOut");
        fadeAnimator.Update(0f);

        while (fadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        isFading = false;

        StartCoroutine(LoadLevelCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        isFading = true;

        fadeAnimator.Play("FadeIn");
        fadeAnimator.Update(0f);

        while (fadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        isFading = false;

        if (!DialogueManager.Instance.IsActive)
            Game.Unpause();
    }

    private IEnumerator LoadLevelCoroutine()
    {
        var projectiles = FindObjectsByType<Projectile>(FindObjectsSortMode.None);
        foreach (var projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }

        isLoading = true;

        if (SceneManager.GetSceneByName(level).isLoaded)
        {
            var unloadOp = SceneManager.UnloadSceneAsync(level);
            while (!unloadOp.isDone)
                yield return null;
        }

        var loadOp = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;

        level = nextLevel;

        isLoading = false;

        PlayerController.Instance.transform.position = playerPosition;

        if (skipFade)
        {
            if (!DialogueManager.Instance.IsActive)
                Game.Unpause();
        }
        else
            StartCoroutine(FadeInCoroutine());
    }
}
