using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            // Destroy bullet.
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null)
                BulletManager.Instance.DestroyBullet(bullet);

            // Destroy enemy.
            EnemyManager.Instance.DestroyEnemy(this);
        }
    }
}
