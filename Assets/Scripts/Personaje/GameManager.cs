using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isReturningFromCombat = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "EscenaPrincipal")
        {
            // Only restore player position if they are returning from combat
            if (isReturningFromCombat)
            {
                RestorePlayerPosition();
                RemoveDefeatedEnemy();
                isReturningFromCombat = false;  // Reset flag after restoration
            }
        }
    }

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.SetFloat("PlayerZ", position.z);
        PlayerPrefs.Save();
    }

    public void RestorePlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                player.transform.position = new Vector3(x, y, z);
            }
        }
    }

    public void SaveEnemyData(string enemyName)
    {
        PlayerPrefs.SetString("LastEnemy", enemyName);
        PlayerPrefs.Save();
    }

public void RemoveDefeatedEnemy()
{
    if (PlayerPrefs.HasKey("LastEnemy"))
    {
        string enemyName = PlayerPrefs.GetString("LastEnemy");
        GameObject enemyToRemove = GameObject.Find(enemyName);

        if (enemyToRemove != null)
        {
            Destroy(enemyToRemove);
            Debug.Log("Enemy " + enemyName + " removed from the scene.");
        }

        PlayerPrefs.DeleteKey("LastEnemy"); // Clean up after removing the enemy
    }
}


    public void LoadCombatScene()
    {
        // Set the flag to true when entering combat so we restore the position on return
        isReturningFromCombat = true;
        SceneManager.LoadScene("EscenaCombate");
    }

public void ReturnToMainScene()
{
    SceneManager.LoadScene("EscenaPrincipal");
    SceneManager.sceneLoaded += OnSceneLoaded; // Register a callback when the scene is loaded
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "EscenaPrincipal")
    {
        RemoveDefeatedEnemy();  // Remove the enemy after combat
        RestorePlayerPosition(); // Restore the player's position
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister the callback
    }
}
}
