using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject HP1Prefab, HP2Prefab, HP3Prefab, HP4Prefab, HP5Prefab, HP6Prefab;
    [SerializeField] private GameObject subSystemPrefab;

    [Header("UI Canvases")]
    public GameObject GameOverCanvas;   // Game Over 전용 Canvas
    public GameObject GameClearCanvas;  // Game Clear 전용 Canvas

    private Player player;
    private GameObject[] hpIcons = new GameObject[6];
    private GameObject subSystemInstance;

    private bool isGameEnded = false; // ✅ GameOver/GameClear 중복 실행 방지

    // -------------------- 싱글톤 --------------------
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // -------------------- 초기화 --------------------
    void Start()
    {
        SpawnPlayer();
        StartCoroutine(SpawnHPIcons());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitAfterSceneLoad());
    }

    IEnumerator InitAfterSceneLoad()
    {
        yield return null;

        if (GameOverCanvas != null)
            GameOverCanvas.SetActive(false);

        if (GameClearCanvas != null)
            GameClearCanvas.SetActive(false);

        isGameEnded = false; // 씬 로드 시 항상 초기화

        if (player == null)
            SpawnPlayer();

        StartCoroutine(SpawnHPIcons());
    }

    void SpawnPlayer()
    {
        var playerObj = Instantiate(playerPrefab, new Vector2(-3.53f, -3.36f), Quaternion.identity);
        player = playerObj.GetComponent<Player>();
    }

    void SpawnSubSystem()
    {
        if (subSystemInstance != null)
            Destroy(subSystemInstance);

        subSystemInstance = Instantiate(subSystemPrefab);
    }

    // -------------------- 씬 제어 --------------------
    // 🔴 Game Over → 현재 스테이지 재시작
    public void RestartScene()
    {
        Debug.Log("▶ RestartScene 실행됨");
        StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
        if (player != null)
        {
            Destroy(player.gameObject);
            player = null;
        }

        if (subSystemInstance != null)
        {
            Destroy(subSystemInstance);
            subSystemInstance = null;
        }

        yield return new WaitForSeconds(0.1f);

        Debug.Log("씬 전환 시도: SampleScene");
        SceneManager.LoadScene("SampleScene"); // 현재 스테이지 이름
    }

    // 🟢 Game Clear → MainMenu 이동
    public void LoadNextStage()
    {
        Debug.Log("▶ LoadNextStage 실행됨");
        StartCoroutine(LoadNextStageRoutine());
    }

    IEnumerator LoadNextStageRoutine()
    {
        if (player != null)
        {
            Destroy(player.gameObject);
            player = null;
        }

        if (subSystemInstance != null)
        {
            Destroy(subSystemInstance);
            subSystemInstance = null;
        }

        yield return new WaitForSeconds(0.1f);

        Debug.Log("씬 전환 시도: MainMenu");
        SceneManager.LoadScene("MainMenu"); // ✅ 반드시 Build Settings에 등록되어 있어야 함
    }

    // -------------------- HP 아이콘 --------------------
    IEnumerator SpawnHPIcons()
    {
        Vector3[] positions = {
            new Vector3(6.36f, -0.6f, 0),
            new Vector3(6.817f, -0.855f, 0),
            new Vector3(6.793693f, -1.349f, 0),
            new Vector3(6.360732f, -1.59695f, 0),
            new Vector3(5.90255f, -1.348943f, 0),
            new Vector3(5.936178f, -0.8487265f, 0)
        };

        GameObject[] prefabs = { HP1Prefab, HP2Prefab, HP3Prefab, HP4Prefab, HP5Prefab, HP6Prefab };

        for (int i = 0; i < 6; i++)
        {
            if (hpIcons[i] != null)
                Destroy(hpIcons[i]);

            hpIcons[i] = Instantiate(prefabs[i], positions[i], Quaternion.identity);
        }

        yield return null;
    }

    void Update()
    {
        if (player == null) return;

        for (int i = 0; i < 6; i++)
        {
            if (hpIcons[i] != null)
                hpIcons[i].SetActive(i < player.hp);
        }

        if (subSystemInstance == null && player.ItemCount > 0)
            SpawnSubSystem();
    }

    // -------------------- GameOver / GameClear --------------------
    public void GameOver()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Debug.Log("🟥 Game Over 발생");

        if (GameOverCanvas != null)
            GameOverCanvas.SetActive(true);

        var gameOverScript = GameOverCanvas.GetComponentInChildren<GameOver>();
        if (gameOverScript != null)
        {
            gameOverScript.StartCoroutine("GameOverText");
            gameOverScript.StartCoroutine("RetryText");
        }
    }

    public void GameClear()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Debug.Log("🟩 Game Clear 발생");

        if (GameClearCanvas != null)
            GameClearCanvas.SetActive(true);

        var gameClearScript = GameClearCanvas.GetComponentInChildren<GameClear>();
        if (gameClearScript != null)
        {
            gameClearScript.StartCoroutine("ClearTextAnim");
            gameClearScript.StartCoroutine("NextButtonAnim");
        }
    }

    // -------------------- Getter --------------------
    public Player GetPlayer() => player;
}
