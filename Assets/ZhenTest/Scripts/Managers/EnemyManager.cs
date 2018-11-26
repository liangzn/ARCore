using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : BaseManager<EnemyManager>, IRestart {

    public GameObject EnemyPrefab;

    // Tracking all enemies.
    private HashSet<Enemy> enemies = new HashSet<Enemy>();

    // Set the maximum of enemies.
    private const int MAXIMUM = 3;
    private int count = 0;

    private void Awake()
    {
        instance = this;
        UIManager.RestartHandler += Restart;
    }

    private void Start()
    {
        UpdateHUD();
    }

    private void OnDestroy()
    {
        UIManager.RestartHandler -= Restart;
    }

    public Transform CreateEnemy(Vector3 position, Quaternion rotation)
    {
        if (count >= MAXIMUM)
            return null;

        count++;
        UpdateHUD();
        Enemy enemy = Instantiate(EnemyPrefab, position, rotation).GetComponent<Enemy>();
        enemies.Add(enemy);

        if (IsAllCreated() && PlayerManager.Instance.IsCreated())
            GameManager.Instance.SetGameState(GameState.FIGHTING);

        return enemy.transform;
    }

    public bool IsAllCreated()
    {
        return count == MAXIMUM;
    }

    public bool IsAllDestroyed()
    {
        return count == 0;
    }

    public void DestroyEnemy(Enemy enemy)
    {
        count--;
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);

        // End the game.
        if (count == 0)
            GameManager.Instance.SetGameState(GameState.VICTORY);
    }

    public void DestroyAllEnemies()
    {
        count = 0;
        foreach(Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemies.Clear();
    }

    public void UpdateHUD()
    {
        UIManager.instance.EnemyText.text = string.Format("Enemy: {0}", count);
    }

    public void Restart()
    {
        DestroyAllEnemies();
        count = 0;
        UpdateHUD();
    }
}
