using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoFolder : MonoBehaviour
{
    private bool hided = true;
    private Text text;

    void Start()
    {
        text = transform.GetChild(1).GetComponent<Text>();
        this.gameObject.SetActive(false);
    }

    public void HideShow(string tag)
    {
        this.gameObject.SetActive(hided);
        hided = !hided;
        switch (tag) {
            case "FollowByRank":
                {
                    text.text = "Tower will target on higher rank enemy";
                }
            break;
        }
    }
}
