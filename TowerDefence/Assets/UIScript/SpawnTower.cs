using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTower : MonoBehaviour
{
    public int TowerType;
    public bool pressed;
    public TowerConstants tc;

    private Image Background;
    private Image TowerIcon;
    private Text  text;
    private Text  cost;

    public TowerAngle tower;

    void Start()
    {
        Background = transform.GetChild(0).GetComponent<Image>();
        TowerIcon = transform.GetChild(1).GetComponent<Image>();
        text = transform.GetChild(2).GetComponent<Text>();
        cost = transform.GetChild(3).GetComponent<Text>();

        text.text = "Souls cannon";
    }

    public void Spawn()
    {
        pressed = !pressed;
        if (pressed)
        {
            Global.souls += tower.cost / 2;
            tower.SetType(0);
        }
        else
        {
            tower.SetType(TowerType);
            Global.souls -= tower.cost;
        }
        ActiveDe();
    }

    public void ActiveDe()
    {
        if (pressed)
        {
            cost.text = tc.Cost[TowerType].ToString();
            text.text = "Souls cannon";
        }
        else
        {
            cost.text = (tc.Cost[TowerType] / 2).ToString();
            text.text = "Clear";
        }
    }
}
