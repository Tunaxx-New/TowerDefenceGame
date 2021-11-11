using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool paused = false;
    public void Pause()
    {
        if (paused)
        {
            Time.timeScale = 1;
        } else
        {
            Time.timeScale = 0;
        }
        paused = !paused;
    }
}
