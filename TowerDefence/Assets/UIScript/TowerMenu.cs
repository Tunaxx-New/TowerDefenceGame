using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    private Animator animator;
    [SerializeField] SpawnTower[] buttons;
    [SerializeField] ChangeInTower[] cbuttons;

    public void OpenMenu(GameObject tower)
    {
        TowerAngle a = tower.GetComponent<TowerAngle>();
        TowerShoot b = tower.GetComponent<TowerShoot>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].tower = a;
            if (b.type == buttons[i].TowerType)
            {
                buttons[i].pressed = false;
                buttons[i].ActiveDe();
            }
            else
            {
                buttons[i].pressed = true;
                buttons[i].ActiveDe();
            }
        }
        for (int i = 0; i < cbuttons.Length; i++)
        {
            cbuttons[i].tower = a;
            cbuttons[i].towersh = b;
            cbuttons[i].ActiveDe();
            cbuttons[i].SetText();
        }
        animator.SetBool("Open", true);
    }

    public void CloseMenu()
    {
        animator.SetBool("Open", false);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
