using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : BaseManager<BulletManager>, IRestart {

    public GameObject bulletPrefab;

    private const int MAXIMUM = 8;
    private int count = MAXIMUM;

    // Tracking all bullets.
    private HashSet<Bullet> bullets = new HashSet<Bullet>();

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

    public Transform CreateBullet(Vector3 _position)
    {
        if (count == 0)
            return null;

        count--;
        UpdateHUD();
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.Setup(_position);
        bullets.Add(bullet);
        return bullet.transform;
    }

    public void DestroyBullet(Bullet bullet)
    {
        // End the game if there is no bullet.
        if (count == 0)
        {
            if (EnemyManager.Instance.IsAllDestroyed())
                GameManager.Instance.SetGameState(GameState.VICTORY);
            else
                GameManager.Instance.SetGameState(GameState.DEFEAT);
        }

        bullets.Remove(bullet);
        Destroy(bullet.gameObject);
    }

    public void DestroyAllBullets()
    {
        foreach (Bullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        bullets.Clear();
    }

    public void UpdateHUD()
    {
        UIManager.instance.BulletText.text = string.Format("Bullet: {0}", count);
    }

    // Implement IRestart inferface.
    public void Restart()
    {
        DestroyAllBullets();
        count = MAXIMUM;
        UpdateHUD();
    }
}
