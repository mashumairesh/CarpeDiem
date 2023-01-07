using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject : MonoBehaviour
{

    [SerializeField] private Image image;
    [SerializeField] private CardData cardData;
    public CardData CardData { get => cardData; set => cardData = value; }


    public List<TextMeshProUGUI> costText;
    public List<TextMeshProUGUI> EffectText;

    public void Set_Values(List<int> tmp)
    {

    }

}

[System.Serializable]
public class CardData
{
    [SerializeField] private int cardNum;
    [SerializeField] private List<int> price; //[10]
    [SerializeField] private List<int> effect; //[7]
    [SerializeField] private int turn;
    [SerializeField] private int slot;

    public int CardNum { get => cardNum; set => cardNum = value; }
    public List<int> Price { get => price; set => price = value; }
    public List<int> Effect { get => effect; set => effect = value; }
    public int Turn { get => turn; set => turn = value; }
    public int Slot { get => slot; set => slot = value; }
}