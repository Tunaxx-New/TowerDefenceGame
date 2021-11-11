using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public Button[] Open;
    public Button Close;

    private Animator animator;

    public void OpenMenu()
    {
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
