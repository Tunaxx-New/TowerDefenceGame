using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int Rank;
    public int hp;
    public int damage;
    public float AttackSpeed;

    [SerializeField] Collider2D[] IgnoreCollision;

    private GameObject currentTower;
    public GameObject currentGate;
    public IEnumerator attack;
    private bool nattacking = true;

    //Checking enemy entered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        string tag = obj.tag;
        if (tag == "Tower")
        {
            obj.SendMessage("AddTarget", this.gameObject);
            obj.SendMessage("SetTarget", this.gameObject);
            currentTower = obj;
        } else if (tag == "Bullet")
        {
            BulletLife bullet = obj.GetComponent<BulletLife>();
            hp -= bullet.damage;
            if (hp <= 0)
            {
                currentTower.SendMessage("DelTarget", this.gameObject);
                Death();
            }
            obj.SendMessage("Crash");
        } else if (tag == "Gate")
        {
            currentGate = obj;
            if (nattacking)
            {
                StartCoroutine(attack);
                nattacking = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Tower")
        {
            collision.gameObject.SendMessage("DelTarget", this.gameObject);
        } else if (tag == "Gate")
        {
            StopAttackFence(currentGate);
        }
    }

    public void StopAttackFence(GameObject obj)
    {
        if (obj == currentGate)
        {
            StopCoroutine(attack);
            currentGate = null;
            nattacking = true;
        }
    }
    public void ApplyDamage(int damage)
    {
        hp -= damage;
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        //Ignore collider
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        foreach (Collider2D otherCollider in IgnoreCollision) Physics2D.IgnoreCollision(collider, otherCollider);

        //SetAttack courutine
        attack = AttackCourutine();
    }
    private IEnumerator AttackCourutine()
    {
        for (; ; )
        {
            currentGate.SendMessage("ApplyDamageGate", damage);
            yield return new WaitForSeconds(AttackSpeed);
        }
    }
}
