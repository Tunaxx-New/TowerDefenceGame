using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerShoot : MonoBehaviour
{
    [SerializeField] float ShootSpeed = 0.1f;

    [SerializeField] GameObject BulletType;
    [SerializeField] float speed = 1;
    [SerializeField] int damage = 1;
    public Vector3 Target;

    private Vector3 selfPosition;
    private Transform selfTransform;

    void Start()
    {
        selfPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        selfTransform = GetComponent<Transform>();
    }

    IEnumerator EveryShootTick()
    {
        for (; ; )
        {
            GameObject bullet = Instantiate(BulletType, selfPosition, Quaternion.identity);

            bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);

            BulletLife bulletScript = bullet.GetComponent<BulletLife>();
            bulletScript.enable = true;
            bulletScript.speed = speed;
            bulletScript.damage = damage;
            bulletScript.Target = Target;
            bulletScript.lifetime = bulletScript.lifetime * ShootSpeed;
            if (Target == null)
            {
                Destroy(bullet);
            }

            yield return new WaitForSeconds(ShootSpeed);
        }
    }
}
