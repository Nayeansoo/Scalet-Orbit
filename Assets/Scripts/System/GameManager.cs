using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerPrefab;
    public GameObject HP1Prefab, HP2Prefab, HP3Prefab, HP4Prefab, HP5Prefab, HP6Prefab;
    public GameObject GameOverCanvas;
    [SerializeField] private GameObject subSystemPrefab;

    private Player player;
    private GameObject[] hpIcons = new GameObject[6];
    private GameObject subSystemInstance;

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

    void Start()
    {
        SpawnPlayer();
        StartCoroutine(SpawnHPIcons());
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitAfterSceneLoad());
    }

    IEnumerator InitAfterSceneLoad()
    {
        yield return null;

        GameOverCanvas = GameObject.Find("Canvas");
        if (GameOverCanvas != null)
            GameOverCanvas.SetActive(false);

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

    public void RestartScene()
    {
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

        // 조금 대기해서 모든 객체 완전히 제거
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene("SampleScene");
    }


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

    public void GameOver()
    {
        if (GameOverCanvas == null)
        {
            GameOverCanvas = GameObject.Find("Canvas");
            if (GameOverCanvas == null) return;
        }

        GameOverCanvas.SetActive(true);

        var gameOverScript = GameOverCanvas.GetComponentInChildren<GameOver>();
        if (gameOverScript != null)
        {
            gameOverScript.StartCoroutine("GameOverText");
            gameOverScript.StartCoroutine("RetryText");
        }
    }

    public Player GetPlayer() => player;
}
