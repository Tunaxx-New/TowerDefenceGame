using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeInTower : MonoBehaviour
{
    public TowerAngle tower;
    public TowerShoot towersh;
    public Sprite Active;
    public Sprite Deactive;
    [SerializeField] Text text;
    [SerializeField] Text cost;
    [SerializeField] int textType = 0;

    private Image image;

    public void ChangeFollowing()
    {
        if (towersh.type != 0)
        {
            tower.ChangeFBR();
            Global.souls--;
            ActiveDe();
            SetText();
        }
    }
    public void IncreaseDamage()
    {
        if (towersh.type != 0)
        {
            towersh.IncreaseDamage();
            Global.souls -= towersh.damage;
            SetText();
        }
    }
    public void ActiveDe()
    {
        if (tower.FollowByRank)
        {
            image.sprite = Active;
        }
        else image.sprite = Deactive;
    }
    public void SetText()
    {
        switch (textType) {
            case 0:
                if (tower.FollowByRank)
                {
                    text.text = Strings.On[Strings.lang];
                } else text.text = Strings.Off[Strings.lang];
                break;
            case 1:
                text.text = towersh.damage.ToString();
                cost.text = (towersh.damage + towersh.increaseDam).ToString();
                break;
        }
    }

    void Start()
    {
        image = GetComponent<Image>();
    }
}
