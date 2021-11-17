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
    [SerializeField] public int increaseDam = 1;
    [SerializeField] public int damage = 0;
    [SerializeField] public GameObject BulletType;
    [SerializeField] public Animator fireAnim;

    public Vector3 Target;
    public bool enable = false;

    private Vector3 selfPosition;
    private Transform selfTransform;
    private SpriteRenderer spriteRender;
    public IEnumerator shoot;

    public void IncreaseDamage()
    {
        damage += increaseDam;
    }

    void Start()
    {
        shoot = EveryShootTick();
        selfPosition = new Vector3(transform.position.x, transform.position.y, -9);
        selfTransform = GetComponent<Transform>();
    }

    public void PlayFire()
    {
        fireAnim.Play("Fire", -1, 0f);
    }

    public IEnumerator EveryShootTick()
    {
        while (enable)
        {
            yield return new WaitForSeconds(ShootSpeed);
            if (Global.souls > 0)
            {
                GameObject bullet = Instantiate(BulletType, selfPosition, Quaternion.identity);

                bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);

                BulletLife bulletScript = bullet.GetComponent<BulletLife>();
                bulletScript.enable = true;
                bulletScript.speed += speed;
                bulletScript.damage += damage;
                bulletScript.Target = Target;
                bulletScript.lifetime = bulletScript.lifetime * ShootSpeed;
                if (Target == null)
                {
                    Destroy(bullet);
                }
                fireAnim.Play("Fire", -1, 0f);
            }
            //fireAnim.Play("Stop");
        }
        shoot = EveryShootTick();
    }
}
