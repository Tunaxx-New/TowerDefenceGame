using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateStats : MonoBehaviour
{
    public int hp;

    private GameObject[] gameObjects = new GameObject[1000];
    private int goSize = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObjects[goSize] = collision.gameObject;
        goSize++;
    }

    public void ApplyDamageGate(int damage)
    {
        hp -= damage;
        if (hp <= 0) Crash();
    }

    private void Crash()
    {
        for (int i = 0; i < goSize; i++)
        {
            gameObjects[i].SendMessage("StopAttackFence", this.gameObject);
            gameObjects[i].SendMessage("ChangePath", true);
        }
        Destroy(this.gameObject);
    }
    void Destroy()
    {
        gameObjects = null;
    }
}