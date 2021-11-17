using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAngle : MonoBehaviour
{
    [SerializeField] private TowerConstants info;

    //Interactable
    [SerializeField] public bool FollowByRank = true;
    [SerializeField] public float speed = 1;
    [SerializeField] public int cost = 0;

    //Script values
    [SerializeField] public float     offset= 0;
    private int       FollowEnemyRank = -1;
    public bool isShooting = true;
    public Transform FollowEnemy;

    private Transform selfTransform;

    [SerializeField] Animation[] spawnAnimation;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer spriteRendererLight;
    private Sprite[] LightImages = new Sprite[8];
    private Sprite[] TowerImages = new Sprite[8];

    public EnemyStats enemy;
    private TowerShoot selfShoot;

    private GameObject[] gameObjects = new GameObject[1000];
    private int targetsSize = 0;
    private bool notarget = false;

    public void ChangeFBR()
    {
        FollowByRank = !FollowByRank;
    }
    public void StartShoot()
    {
        if (isShooting)
        {
            StartCoroutine(selfShoot.shoot);
            isShooting = false;
        }
    }
    public void SetTarget(GameObject target)
    {
        //Checkinh rank of target
        enemy = target.GetComponent<EnemyStats>();
        Transform enemyTransformApprox = target.transform.Find("AnticipationAngle/AnticipationPos");
        if (FollowByRank)
        {
            if (FollowEnemyRank <= enemy.Rank)
            {
                StartShoot();
                FollowEnemy = enemyTransformApprox;
                FollowEnemyRank = enemy.Rank;
            }
        }
        else
        {
            StartShoot();
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
        notarget = false;
    }

    public IEnumerator UpdateSprite;

    public void SetType(int tp)
    {
        selfShoot.type = tp;

        speed = info.RotateSpeed[tp];
        selfShoot.ShootSpeed = info.ShotSpeed[tp];
        selfShoot.speed = info.Speed[tp];
        selfShoot.damage = info.Damage[tp];
        selfShoot.increaseDam = info.IncreaseDamage[tp];
        cost = info.Cost[tp];
        spawnAnimation[0].Play("spawnTower");
        spawnAnimation[1].Play("teethTower");
        StartCoroutine("SpawnTime");
    }
    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(0.5f);
        TowerImages = info.Tower1Images;
        LightImages = info.Light1Images;
        spriteRendererLight.gameObject.SetActive(true);
        switch (selfShoot.type)
        {
            case 0:
                {
                    selfShoot.enable = false;
                    spriteRenderer.sprite = null;
                    TowerImages = info.TowerImagesNull;
                    LightImages = info.LightImagesNull;
                    spriteRendererLight.gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    selfShoot.enable = false;

                    //isShooting = true;
                    //StopCoroutine(selfShoot.shoot);
                    StartShoot();
                    selfShoot.enable = true;
                    selfShoot.PlayFire();

                    SpriteWithAngle();
                }
                break;
        }
    }
    private void SpriteWithAngle()
    {
        float rotate = selfTransform.eulerAngles.z;
        while (rotate < 0) rotate += 360;
        int angle = (int)(rotate / 45.0f);
        spriteRenderer.sprite = TowerImages[angle];
        spriteRendererLight.sprite = LightImages[angle];
    }

    void Start()
    {
        selfTransform = GetComponent<Transform>();
        selfShoot = GetComponent<TowerShoot>();

        UpdateSprite = UpdateSpriteCoroutine();
        StartCoroutine(UpdateSprite);
        SetType(selfShoot.type);
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

    private IEnumerator UpdateSpriteCoroutine()
    {
        while (true)
        {
            SpriteWithAngle();
            yield return new WaitForSeconds(info.ANGLE_SPRITE_UPDATE);
        }
    }

    void Destroy()
    {
        gameObjects = null;
        TowerImages = null;
        LightImages = null;
    }
}