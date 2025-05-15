using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public RectTransform gameover;
    public RectTransform retry;
    public GameObject GameOverCanvas;

    private Tween gameoverTween;
    private Tween retryTween;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        if (gameover != null) gameover.anchoredPosition = new Vector2(0, -712);
        if (retry != null) retry.anchoredPosition = new Vector2(0, -771);

        StartCoroutine(GameOverText());
        StartCoroutine(RetryText());
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (gameoverTween != null && gameoverTween.IsActive()) gameoverTween.Kill();
        if (retryTween != null && retryTween.IsActive()) retryTween.Kill();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayInitUI());
    }

    IEnumerator DelayInitUI()
    {
        yield return null;

        GameOverCanvas = GameObject.Find("Canvas");
        if (GameOverCanvas == null) yield break;

        Transform panel = GameOverCanvas.transform.Find("Panel");
        if (panel == null) yield break;

        gameover = panel.Find("Game Over")?.GetComponent<RectTransform>();
        retry = panel.Find("Retry")?.GetComponent<RectTransform>();
        if (gameover == null || retry == null) yield break;

        gameover.anchoredPosition = new Vector2(0, -712);
        retry.anchoredPosition = new Vector2(0, -771);

        if (GameOverCanvas.activeSelf)
        {
            StartCoroutine(GameOverText());
            StartCoroutine(RetryText());
        }
    }

    public void StartGameOverSequence()
    {
        StartCoroutine(GameOverText());
        StartCoroutine(RetryText());
    }

    public void OnClickRestart()
    {
        DOTween.KillAll();
        GameManager.instance.RestartScene();
    }

    IEnumerator GameOverText()
    {
        yield return new WaitForSeconds(1.0f);
        if (gameover != null && gameover.gameObject.activeInHierarchy)
            gameoverTween = gameover.DOAnchorPos(new Vector2(0, 144), 3.0f).SetEase(Ease.OutQuint);
    }

    IEnumerator RetryText()
    {
        yield return new WaitForSeconds(3.0f);
        if (retry != null && retry.gameObject.activeInHierarchy)
            retryTween = retry.DOAnchorPos(new Vector2(0, -161), 3.0f).SetEase(Ease.OutQuint);
    }
}
