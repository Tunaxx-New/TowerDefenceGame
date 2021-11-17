using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public static int souls = 1000;
}
public class Strings
{
    public static int lang = 0;
    const int size = 2;
    public static string[] FBRinfo = new string[size] { "Tower will target on higher rank enemy",
    "Башня будет атаковать цели, у которых ранг выше"};

    public static string[] DMGinfo = new string[size] { "Tower damage per bullet",
    "Урон за каждую пулю башни"};

    public static string[] soul = new string[size] { "Souls: ", "Души: " };
    public static string[] On = new string[size] { "On", "Вкл" };
    public static string[] Off = new string[size] { "Off", "Выкл" };
}

public class SpawnEnemies : MonoBehaviour
{
    [System.Serializable]
    struct Enemy
    {
        public GameObject enemy;
        public float time;
        public int numberofenemies;
        public float wait;
    }
    int j = 0;
    [SerializeField] Enemy[] enemies;
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(enemies[j].time);
        for (int i = 0; i < enemies[j].numberofenemies; i++)
        {
            GameObject enem = Instantiate(enemies[j].enemy);
            enem.GetComponent<FollowPath>().enabled = true;
            yield return new WaitForSeconds(enemies[j].wait);
        }
        j++;
        if (j != enemies.Length) StartCoroutine(Spawn());
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }
}
