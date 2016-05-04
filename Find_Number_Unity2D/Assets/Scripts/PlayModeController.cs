using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayModeController : MonoBehaviour
{
    public bool increaseMode = false;
    public static PlayModeController Instance { get; set; }

    public void ButtonHardPressed()
    {
        increaseMode = true;
    }

    public void ButtonEasyPressed()
    {
        increaseMode = false;
    }
}
