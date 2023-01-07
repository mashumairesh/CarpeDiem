using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardObject : MonoBehaviour
{

    [SerializeField] private Image _image;
    [SerializeField] private CardData _cardData;
    
    public CardObject (int cardNum, List<int> price, List<int> effect, int turn, int slot)
    {
        _cardData.CardNum = cardNum;
        _cardData.Price = price;
        _cardData.Effect = effect;
        _cardData.Turn = turn;
        _cardData.Slot = slot;
        _cardData.isControlAble = true;
    }
    public int GetCardNum()
    {
        return _cardData.CardNum;
    }
    public List<int> GetPrice()
    {
        return _cardData.Price;
    }
    public List<int> GetEffect()
    {
        return _cardData.Effect;
    }
    public int GetTurn()
    {
        return _cardData.Turn;
    }
    public int GetSlot()
    {
        return _cardData.Slot;
    }
    public bool GetCA()
    {
        return _cardData.isControlAble;
    }
    public void Puchased()
    {
        _cardData.isControlAble = false;
    }
}

[System.Serializable]
public class CardData
{
    public int CardNum;
    public List<int> Price; //[10]
    public List<int> Effect; //[7]
    public int Turn;
    public int Slot;
    public bool isControlAble;
}
