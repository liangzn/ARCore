using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager<PlayerManager>, IRestart {

    public GameObject PlayerPrefab;

    private void Awake()
    {
        instance = this;
        UIManager.RestartHandler += Restart;
    }

    private void OnDestroy()
    {
        UIManager.RestartHandler -= Restart;
    }

    public Transform CreatePlayer(Vector3 position, Quaternion rotation)
    {
        Player enemy = Instantiate(PlayerPrefab, position, rotation).GetComponent<Player>();

        if (EnemyManager.Instance.IsAllCreated() && IsCreated())
            GameManager.Instance.SetGameState(GameState.FIGHTING);

        return enemy.transform;
    }

    public bool IsCreated()
    {
        return Player.instance != null;
    }

    // Implement IRestart inferface.
    public void Restart()
    {
        Destroy(Player.instance.gameObject);
    }
}
