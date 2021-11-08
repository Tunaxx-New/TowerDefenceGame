using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int Rank;
    public int health;

    public void ApplyDamage(int damage)
    {
        health -= damage;
    }
}
