using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTower : MonoBehaviour
{
    public int TowerType;
    public bool pressed;

    private Image Background;
    private Image TowerIcon;
    private Text  text;

    public TowerAngle tower;

    void Start()
    {
        Background = transform.GetChild(0).GetComponent<Image>();
        TowerIcon = transform.GetChild(1).GetComponent<Image>();
        text = transform.GetChild(2).GetComponent<Text>();

        text.text = "Souls cannon";
    }

    public void Spawn()
    {
        pressed = !pressed;
        if (pressed)
        {
            tower.SetType(0);
        }
        else
        {
            tower.SetType(TowerType);
        }
        ActiveDe();
    }

    public void ActiveDe()
    {
        if (pressed)
        {
            text.text = "Souls cannon";
        }
        else
        {
            text.text = "Clear";
        }
    }
}
