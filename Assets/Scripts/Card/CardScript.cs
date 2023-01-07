using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public TextMeshPro[] ReqTexts;
    public TextMeshPro[] EffectTexts;
    public TextMeshPro TurnText;
    public float scaleMultiplier;
    private CardData _cardData;
    bool isPurchased;
    int turnLeft;
    float targetScale;
    float originScale;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        targetScale = originScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(targetScale, targetScale, 1.0f), ref velocity, 0.3f);
    }

    public void Initalize(CardData from)
    {
        this._cardData = from;
        for (int i = 0; i < 5; i++)
        {
            ReqTexts[i].text = _cardData.Price[i].ToString();
            EffectTexts[i].text = _cardData.Effect[i].ToString();
        }
        TurnText.text = _cardData.Turn.ToString();
        isPurchased = false;
        turnLeft = _cardData.Turn;
    }
    public void OnMouseEnter()
    {
        //if (!isControlAble) return;
        targetScale = originScale * scaleMultiplier;
        transform.Translate(Vector3.back * 0.25f);
    }
    public bool execute()
    {
        turnLeft--;
        return turnLeft == 0;
    }
    public void OnMouseExit()
    {
        targetScale = originScale;
        transform.Translate(Vector3.forward * 0.25f);
    }

    public void OnMouseDown()
    {
        if (!isPurchased)
            CardManager.instance.Check_BuyThisCard(this._cardData.CardNum, TableManager.instance.Get_NowPlayerResource());
        else
            TableManager.instance.Get_NowPlayerScript().RemoveCard(gameObject);
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
    public bool IsPurchased { get { return isPurchased; } set { isPurchased = value; } }
}
