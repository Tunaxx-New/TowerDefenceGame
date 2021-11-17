using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSoulsText : MonoBehaviour
{

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = Strings.soul[Strings.lang] + Global.souls.ToString();
    }
}
