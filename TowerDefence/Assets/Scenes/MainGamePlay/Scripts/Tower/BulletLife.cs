using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletLife : MonoBehaviour
{

    private Transform selfTransform;

    public Vector3 Target;
    public bool enable = false;
    public float speed = 1;
    public int damage = 1;

    private float lifecount = 0;
    [SerializeField] public float lifetime = 10;

    public void Crash()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        selfTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (enable)
        {
            selfTransform.Translate(Vector2.right * speed * Time.deltaTime);
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
