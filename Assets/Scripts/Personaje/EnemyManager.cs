using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; 
    public List<GameObject> enemies = new List<GameObject>(); 

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

    // Add enemy to the list when it spawns
    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    // Remove enemy from the list after it's defeated
    public void RemoveEnemy(string enemyName)
    {
        GameObject enemyToRemove = enemies.Find(e => e.name == enemyName);
        if (enemyToRemove != null)
        {
            enemies.Remove(enemyToRemove);
            Destroy(enemyToRemove);
        }
    }

    // Check if an enemy was already defeated in previous sessions
    public bool IsEnemyDefeated(string enemyName)
    {
        return PlayerPrefs.GetInt(enemyName + "_defeated", 0) == 1;
    }

    // Mark enemy as defeated
    public void MarkEnemyAsDefeated(string enemyName)
    {
        PlayerPrefs.SetInt(enemyName + "_defeated", 1);
    }
}
