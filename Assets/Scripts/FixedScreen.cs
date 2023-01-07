using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScreen : MonoBehaviour
{

    private void OnEnable()
    {
        Screen.SetResolution(1920, 1080, true);
    }
}
