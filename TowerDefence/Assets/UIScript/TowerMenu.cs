using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    private Animator animator;
    [SerializeField] SpawnTower[] buttons;
    [SerializeField] ChangeInTower[] cbuttons;

    public void OpenMenu(TowerAngle tower)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].tower = tower;
            if (tower.gameObject.GetComponent<TowerShoot>().type == buttons[i].TowerType)
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
            cbuttons[i].tower = tower;
            cbuttons[i].ActiveDe();
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
