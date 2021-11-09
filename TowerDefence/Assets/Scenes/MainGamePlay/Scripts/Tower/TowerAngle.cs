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

    public EnemyStats enemy;
    private TowerShoot selfShoot;

    public void SetTarget(GameObject target)
    {
        //Checkinh rank of target
        enemy = target.GetComponent<EnemyStats>();
        Transform enemyTransformApprox = target.transform.Find("AnticipationAngle/AnticipationPos");
        if (FollowByRank)
        {
            if (FollowEnemyRank < enemy.Rank)
            {
                FollowEnemy = enemyTransformApprox;
                FollowEnemyRank = enemy.Rank;
            }
        }
        else
        {
            FollowEnemy = enemyTransformApprox;
        }
        selfShoot.StartCoroutine("EveryShootTick");
    }

    void Start()
    {
        selfTransform = GetComponent<Transform>();
        selfShoot = GetComponent<TowerShoot>();
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
            selfShoot.Target = FollowEnemy.position;
        }
    }
}
