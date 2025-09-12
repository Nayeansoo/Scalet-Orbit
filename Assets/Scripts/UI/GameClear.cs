using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public RectTransform clearText;   // "Game Clear" 텍스트
    public RectTransform nextButton;  // "Next Stage" 버튼
    public GameObject GameClearCanvas;

    private Tween clearTween;
    private Tween nextTween;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        if (clearText != null) clearText.anchoredPosition = new Vector2(0, -712);
        if (nextButton != null) nextButton.anchoredPosition = new Vector2(0, -771);

        StartCoroutine(ClearTextAnim());
        StartCoroutine(NextButtonAnim());
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (clearTween != null && clearTween.IsActive()) clearTween.Kill();
        if (nextTween != null && nextTween.IsActive()) nextTween.Kill();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayInitUI());
    }

    IEnumerator DelayInitUI()
    {
        yield return null;

        GameClearCanvas = GameObject.Find("Canvas");
        if (GameClearCanvas == null) yield break;

        Transform panel = GameClearCanvas.transform.Find("Panel");
        if (panel == null) yield break;

        clearText = panel.Find("Game Clear")?.GetComponent<RectTransform>();
        nextButton = panel.Find("Next Stage")?.GetComponent<RectTransform>();
        if (clearText == null || nextButton == null) yield break;

        clearText.anchoredPosition = new Vector2(0, -712);
        nextButton.anchoredPosition = new Vector2(0, -771);

        if (GameClearCanvas.activeSelf)
        {
            StartCoroutine(ClearTextAnim());
            StartCoroutine(NextButtonAnim());
        }
    }

    public void StartGameClearSequence()
    {
        StartCoroutine(ClearTextAnim());
        StartCoroutine(NextButtonAnim());
    }

    public void OnClickNextStage()
    {
        DOTween.KillAll();
        GameManager.instance.LoadNextStage(); // GameManager에 다음 스테이지 로딩 함수 필요
    }

    IEnumerator ClearTextAnim()
    {
        yield return new WaitForSeconds(1.0f);
        if (clearText != null && clearText.gameObject.activeInHierarchy)
            clearTween = clearText.DOAnchorPos(new Vector2(0, 144), 3.0f).SetEase(Ease.OutQuint);
    }

    IEnumerator NextButtonAnim()
    {
        yield return new WaitForSeconds(3.0f);
        if (nextButton != null && nextButton.gameObject.activeInHierarchy)
            nextTween = nextButton.DOAnchorPos(new Vector2(0, -161), 3.0f).SetEase(Ease.OutQuint);
    }
}
