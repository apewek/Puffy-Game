using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    private int enemiesKilled = 0;
    private int totalEnemies;
    private bool isVictoryShown = false;

    private void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        SubscribeToAllEnemies();
    }

    private void SubscribeToAllEnemies()
    {
        EnemyEntity[] allEnemies = FindObjectsByType<EnemyEntity>(FindObjectsSortMode.None);

        if (allEnemies.Length == 0)
            return;

        totalEnemies = allEnemies.Length;

        foreach (EnemyEntity enemy in allEnemies)
        {
            if (enemy != null)
            {
                enemy.OnDeath += Enemy_OnDeath;
            }
        }
    }

    private void Enemy_OnDeath(object sender, System.EventArgs e)
    {
        if (isVictoryShown) return;

        enemiesKilled++;

        EnemyEntity deadEnemy = sender as EnemyEntity;
        if (deadEnemy != null)
        {
            deadEnemy.OnDeath -= Enemy_OnDeath;
        }

        if (enemiesKilled >= totalEnemies)
        {
            isVictoryShown = true;
            ShowVictoryScreen();
        }
    }

    private void ShowVictoryScreen()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        EnemyEntity[] allEnemies = FindObjectsByType<EnemyEntity>(FindObjectsSortMode.None);
        foreach (EnemyEntity enemy in allEnemies)
        {
            if (enemy != null)
            {
                enemy.OnDeath -= Enemy_OnDeath;
            }
        }
    }
}