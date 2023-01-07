using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardObject : MonoBehaviour
{
}

[System.Serializable]
public class CardData
{
    public int CardNum;
    public List<int> Price; //[10]
    public List<int> Effect; //[7]
    public int Turn;
    public int Slot;
}
