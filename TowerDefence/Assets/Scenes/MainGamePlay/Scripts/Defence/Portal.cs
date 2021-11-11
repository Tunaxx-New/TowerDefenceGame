using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int GameOverCount;
    private GameCore gc;

    void Start()
    {
        gc = GetComponent<GameCore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameOverCount--;
            if (GameOverCount <= 0)
            {
                gc.GameOver();
            }
        }
    }
}
