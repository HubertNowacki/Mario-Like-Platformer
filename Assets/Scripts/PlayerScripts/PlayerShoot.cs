using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject Bullet;

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet_spawn = Instantiate(Bullet, transform.position, Quaternion.identity);
            bullet_spawn.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }

    void Update()
    {
        ShootBullet();
    }
}
