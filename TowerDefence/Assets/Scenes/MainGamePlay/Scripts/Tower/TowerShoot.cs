using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerShoot : MonoBehaviour
{
    public int type = 0;

    //Interactable
    [SerializeField] public float ShootSpeed = 0.1f;
    [SerializeField] public float speed = 1;
    [SerializeField] public int damage = 1;
    [SerializeField] public GameObject BulletType;

    public Vector3 Target;
    public bool enable;

    private Vector3 selfPosition;
    private Transform selfTransform;
    private SpriteRenderer spriteRender;
    public IEnumerator shoot;

    void Start()
    {
        shoot = EveryShootTick();
        selfPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        selfTransform = GetComponent<Transform>();
    }

    public IEnumerator EveryShootTick()
    {
        while(enable)
        {
            GameObject bullet = Instantiate(BulletType, selfPosition, Quaternion.identity);

            bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);

            BulletLife bulletScript = bullet.GetComponent<BulletLife>();
            bulletScript.enable = true;
            bulletScript.speed  += speed;
            bulletScript.damage += damage;
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
