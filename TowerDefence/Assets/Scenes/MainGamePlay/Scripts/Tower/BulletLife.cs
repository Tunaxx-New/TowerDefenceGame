using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletLife : MonoBehaviour
{

    private Transform selfTransform;

    public Vector3 Target;
    public bool enable = false;
    public float speed = 0;
    public int damage  = 0;

    public float maxAcceleration;
    private float SpeedTemp = 1;

    private float lifecount = 0;
    [SerializeField] public float lifetime = 10;
    [SerializeField] GameObject FireBlick;

    public void Crash()
    {
        if (Global.souls > 0) Global.souls--;
        GameObject fire = Instantiate(FireBlick, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        fire.GetComponent<Animation>().Play(fire.GetComponent<FireSelfDestroy>().animationName);
        fire.GetComponent<FireSelfDestroy>().StartCoroutine("OnCompleteAnimation");
        Destroy(this.gameObject);
    }

    void Start()
    {
        selfTransform = GetComponent<Transform>();
        GetComponent<Animation>().Play("SoulBullet");
    }

    void Update()
    {
        if (enable)
        {
            selfTransform.Translate(Vector2.right * SpeedTemp * Time.deltaTime);
            if (SpeedTemp < maxAcceleration) SpeedTemp += speed * Time.deltaTime;
        }
        if (lifecount >= lifetime)
        {
            if (enable)
            Crash();
        } else
        {
            lifecount += Time.deltaTime;
        }
        Vector3 offset = selfTransform.position - Target;
        if (offset.sqrMagnitude < 0.1f || offset.sqrMagnitude > 1000.0f)
        {
            Crash();
        }
    }
}
