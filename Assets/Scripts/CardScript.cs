using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public TextMeshPro[] ReqTexts;
    public TextMeshPro[] EffectTexts;
    public TextMeshPro TurnText;
    private CardData cardData;
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

    public void SetCardData(CardData from)
    {
        this.cardData = from;
        for (int i = 0; i < 5; i++)
        {
            ReqTexts[i].text = cardData.Price[i].ToString();
            EffectTexts[i].text = cardData.Effect[i].ToString();
        }
        TurnText.text = cardData.Turn.ToString();

    }
    public void OnMouseEnter()
    {
        targetScale = originScale * 3.0f;
        transform.Translate(Vector3.back * 0.25f);
    }

    public void OnMouseExit()
    {
        targetScale = originScale;
        transform.Translate(Vector3.forward * 0.25f);
    }

    public void OnMouseDown()
    {

    }
}