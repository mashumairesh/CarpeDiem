using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public TextMeshPro[] ReqTexts;
    public TextMeshPro[] EffectTexts;
    public TextMeshPro TurnText, SaleText;
    public GameObject slotObject, slotPrefab, SaleObject;
    public float scaleMultiplier;
    private CardData _cardData;
    bool isPurchased;
    int turnLeft;
    float targetScale, originScale;
    float targetZ, originZ;
    int originGoldCost;
    Vector3 v1, v2;
    // Start is called before the first frame update
    void Start()
    {
        targetScale = originScale = transform.localScale.x;
        targetZ = originZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(targetScale, targetScale, 1.0f), ref v1, 0.3f);
        Vector3 targetPos = transform.localPosition;
        targetPos.z = targetZ;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref v2, 0.3f);
    }

    public void Initalize(CardData from)
    {
        this._cardData = from;
        for (int i = 0; i < 5; i++)
        {
            ReqTexts[i].text = _cardData.Price[i] > 1000000 ? "X" : _cardData.Price[i].ToString();
            EffectTexts[i].text = _cardData.Effect[i].ToString();
        }
        TurnText.text = _cardData.Turn.ToString();
        for(int i = 0; i < _cardData.Slot; i++)
        {
            var newObj = Instantiate(slotPrefab);
            newObj.transform.parent = slotObject.transform;
            newObj.transform.localPosition = Vector3.down * 0.15f * i;
        }
        isPurchased = false;
        turnLeft = _cardData.Turn;
        originGoldCost = _cardData.Price[0];
    }
    public void OnMouseEnter()
    {
        //if (!isControlAble) return;
        targetScale = originScale * scaleMultiplier;
        targetZ = originZ - 0.25f;
        Vector3 before = transform.localPosition;
        before.z = targetZ;
        transform.localPosition = before;
    }
    public void OnMouseExit()
    {
        targetScale = originScale;
        targetZ = originZ;
    }

    public void OnMouseDown()
    {
        if (!isPurchased)
        {
            if (TableManager.instance.Get_NowPlayerScript().SlotLeft >= this._cardData.Slot)
                UIManager.instance.Popup_PurchaseUI(this._cardData.CardNum, CardManager.instance.Check_BuyThisCard(this._cardData.CardNum, TableManager.instance.Get_NowPlayerResource()), this._cardData.Price);
            else
                TableManager.instance.Get_NowPlayerScript().FlashRed();
        }
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
    public int TurnLeft
    {
        get => turnLeft; 
        set
        {
            turnLeft = value;
            TurnText.text = turnLeft.ToString();
        }
    }
    public int GetSlot()
    {
        return _cardData.Slot;
    }
    public bool IsPurchased { get { return isPurchased; } set { isPurchased = value; } }

    /// <summary>
    /// ī���� ���� ���� ����
    /// </summary>
    /// <param name="n">-1�̸� ����, 0�̸� �״��, +1�̸� ����</param>
    public void UpdateSaleInfo(int n)
    {
        SaleObject.SetActive(n != 0);
        _cardData.Price[0] = originGoldCost;
        if (n > 0)
        {
            SaleText.text = "+1";
            _cardData.Price[0]++;
            SaleText.color = Color.red;
        }
        else if (n < 0)
        {
            SaleText.text = "-1";
            _cardData.Price[0]--;
            SaleText.color = Color.blue;
        }
    }
}
