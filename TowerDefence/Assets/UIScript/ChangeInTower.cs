using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInTower : MonoBehaviour
{
    public TowerAngle tower;
    public Sprite Active;
    public Sprite Deactive;

    private Image image;

    public void ChangeFollowing()
    {
        tower.ChangeFBR();
        ActiveDe();
    }
    public void ActiveDe()
    {
        if (tower.FollowByRank)
        {
            image.sprite = Active;
        }
        else image.sprite = Deactive;
    }

    void Start()
    {
        image = GetComponent<Image>();
    }
}
