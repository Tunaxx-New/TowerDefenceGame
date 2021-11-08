using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAngle : MonoBehaviour
{
    //Follow look settings
    [SerializeField] public bool      FollowByRank = true;
    [SerializeField] public float     offset= 0;
    [SerializeField] public float     speed = 1;
    private int       FollowEnemyRank = -1;
    public Transform FollowEnemy;

    private Transform selfTransform;

    //Checking enemy entered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
            if (FollowByRank) {
                if (FollowEnemyRank < enemy.Rank)
                {
                    FollowEnemy = collision.gameObject.GetComponent<Transform>();
                    FollowEnemyRank = enemy.Rank;
                }
            } else
            {
                FollowEnemy = collision.gameObject.GetComponent<Transform>();
            }
        }
    }

    void Start()
    {
        selfTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (FollowEnemy != null)
        {
            //Accelerated
            Vector3 dir = FollowEnemy.position - selfTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

            transform.rotation = Quaternion.Slerp(selfTransform.rotation, rotation, speed * Time.deltaTime);
        }
    }
}
