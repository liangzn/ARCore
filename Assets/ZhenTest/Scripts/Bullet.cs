using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Bullet moving direction.
    private Vector3 direction = Vector3.forward;

    // Set the life to 10 seconds. After that, destroy bullet.
    private float life = 10f;

    // Set the direction of button when creating.
    public void Setup (Vector3 _position) 
    {
        transform.position = _position;
	}

    void Update()
    {
        transform.Translate(direction * Time.deltaTime);

        // Destroy the bullet after 10 seconds.
        if (life > 0)
            life -= Time.deltaTime;
        else
            BulletManager.Instance.DestroyBullet(this);

    }
}
