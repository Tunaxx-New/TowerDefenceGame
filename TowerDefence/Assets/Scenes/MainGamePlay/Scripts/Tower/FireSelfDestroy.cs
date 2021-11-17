using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSelfDestroy : MonoBehaviour
{
    public bool enabled = false;
    public string animationName;
    void Awake()
    {
        if (enabled)
        {
            GetComponent<Animation>().Play(animationName);
            StartCoroutine("OnCompleteAnimation");
        }
    }
    IEnumerator OnCompleteAnimation()
    {
        yield return new WaitForSeconds(3);

        Destroy(this.gameObject);
    }
}
