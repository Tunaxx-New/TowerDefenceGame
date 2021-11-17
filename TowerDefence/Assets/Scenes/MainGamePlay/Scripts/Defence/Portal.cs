using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int GameOverCount;
    private GameCore gc;
    [SerializeField] GameObject FireBlick;

    void Start()
    {
        gc = GetComponent<GameCore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 epos = collision.gameObject.transform.position;
            GameOverCount--;
            GameObject fire = Instantiate(FireBlick, new Vector3(epos.x, epos.y, epos.z), Quaternion.identity);
            fire.GetComponent<Animation>().Play(fire.GetComponent<FireSelfDestroy>().animationName);
            fire.GetComponent<FireSelfDestroy>().StartCoroutine("OnCompleteAnimation");
            Destroy(collision.gameObject);
            if (GameOverCount <= 0)
            {
                gc.GameOver();
            }
        }
    }
}
