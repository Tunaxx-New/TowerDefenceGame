using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    public RenderTexture rt;
    public int pixelesation;

    public Camera camera;

    void Start()
    {
        int width = Screen.width / pixelesation;
        if (width % 2 != 0) width++;
        rt.width = width;
        int height = Screen.height / pixelesation;
        if (height % 2 != 0) height++;
        rt.height = height;

        camera.targetTexture = rt;
    }
    void Destroy()
    {
        rt.height = Screen.height;
        rt.width = Screen.width;
    }
}
