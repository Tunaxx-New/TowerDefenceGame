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
    private bool isShooting = true;
    public Transform FollowEnemy;

    private Transform selfTransform;

    public EnemyStats enemy;
    private TowerShoot selfShoot;

    private GameObject[] gameObjects = new GameObject[1000];
    private int targetsSize = 0;
    private bool notarget = false;

    public void SetTarget(GameObject target)
    {
        //Checkinh rank of target
        enemy = target.GetComponent<EnemyStats>();
        Transform enemyTransformApprox = target.transform.Find("AnticipationAngle/AnticipationPos");
        if (FollowByRank)
        {
            if (FollowEnemyRank <= enemy.Rank)
            {
                if (isShooting)
                {
                    StartCoroutine(selfShoot.shoot);
                    isShooting = false;
                }
                FollowEnemy = enemyTransformApprox;
                FollowEnemyRank = enemy.Rank;
            }
        }
        else
        {
            if (isShooting)
            {
                StartCoroutine(selfShoot.shoot);
                isShooting = false;
            }
            FollowEnemy = enemyTransformApprox;
        }
        notarget = true;
    }
    public void AddTarget(GameObject target)
    {
        gameObjects[targetsSize] = target;
        if (targetsSize < 1000) targetsSize++;
    }
    public void DelTarget(GameObject target)
    {
        for (int i = 0; i < targetsSize; i++)
        {
            if (gameObjects[i] == target)
            {
                gameObjects[i] = null;
                break;
            }
        }
        //targetsSize--;
        notarget = false;
    }

    void Start()
    {
        selfTransform = GetComponent<Transform>();
        selfShoot = GetComponent<TowerShoot>();
    }

    void Update()
    {
        if (notarget)
        {
            //Accelerated
            Vector3 dir = FollowEnemy.position - selfTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

            transform.rotation = Quaternion.Slerp(selfTransform.rotation, rotation, speed * Time.deltaTime);
            selfShoot.Target = FollowEnemy.position;
        } else
        {
            if (targetsSize <= 0)
            {
                if (selfShoot.shoot != null)
                {
                    StopCoroutine(selfShoot.shoot);
                    isShooting = true;
                }
            }
            else
            {
                bool nobody = true;
                for (int i = 0; i < targetsSize; i++)
                {
                    if (gameObjects[i] != null)
                    {
                        SetTarget(gameObjects[i]);
                        notarget = true;
                        nobody = false;
                        break;
                    }
                }
                if (nobody)
                {
                    isShooting = true;
                    notarget = false;
                    StopCoroutine(selfShoot.shoot);
                }
            }
        }
    }

    void Destroy()
    {
        gameObjects = null;
    }
}