using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float delayBeforeShow = 1f;

    private bool isGameOverShown = false;

    private void Awake()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Start()
    {
        // Подписываемся в Start, а не в OnEnable
        if (Player.Instance != null)
        {
            Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        }
        else
        {
            Debug.LogError("Player.Instance not found! Make sure Player object is in the scene.");
        }
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
            Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        if (!isGameOverShown)
        {
            isGameOverShown = true;
            StartCoroutine(ShowGameOverWithDelay());
        }
    }

    private IEnumerator ShowGameOverWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeShow);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

   
}