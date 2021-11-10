using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int Rank;
    public int health;

    [SerializeField] Collider2D[] IgnoreCollision;

    //Checking enemy entered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Tower")
        {
            collision.gameObject.SendMessage("AddTarget", this.gameObject);
            collision.gameObject.SendMessage("SetTarget", this.gameObject);
        } else if (tag == "Bullet")
        {
            BulletLife bullet = collision.gameObject.GetComponent<BulletLife>();
            health -= bullet.damage;
            if (health <= 0)
            {
                collision.gameObject.SendMessage("DelTarget", this.gameObject);
                Death();
            }
            collision.gameObject.SendMessage("Crash", this.gameObject);
        } else if (tag == "Fence")
        {

        }
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        //Ignore collider
        Collider2D collider = GetComponent<Collider2D>();
        foreach (Collider2D otherCollider in IgnoreCollision) Physics2D.IgnoreCollision(collider, otherCollider);
    }
}
