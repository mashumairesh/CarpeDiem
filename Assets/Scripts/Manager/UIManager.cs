using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private RectTransform ShoppingPannel;
    
    [SerializeField] private List<Button> ShoppingButton;
    [SerializeField] private List<bool> ShoppingButtonAble;
    [SerializeField] private List<TextMeshProUGUI> ShoppingText;
    [SerializeField] private List<int> ShoppingTextResource;

    [SerializeField] private int CardNum;

    [SerializeField] private List<bool> testBool;
    [SerializeField] private List<int> testInt;

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Popup_PurchaseUI(0, testBool, testInt);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Popdown_PurchaseUI(0);
        }
    }

    public void Popup_PurchaseUI(int cardNum, List<bool> Able, List<int> resource)
    {
        CardNum = cardNum;
        ShoppingButtonAble = Able;
        ShoppingTextResource = resource;
        StartCoroutine(corFunc_PopupPurchaseUI());
    }

    public void Popdown_PurchaseUI(int rsh)
    {
        List<int> tmp = new List<int>();
        for (int i = 0; i < 5; i++) tmp.Add(0);
        tmp[rsh] = ShoppingTextResource[rsh] + ShoppingTextResource[rsh + 5];
        StartCoroutine(corFunc_PopDownPurchaseUI());

        CardManager.instance.Get_MarketCard(CardNum);
        TableManager.instance.Get_NowPlayerScript().Use(tmp);

    }

    public void ButtonClose()
    {
        for (int i = 0; i < ShoppingButton.Count; i++)
        {
            ShoppingButton[i].interactable = false;
        }
    }

    private IEnumerator corFunc_PopupPurchaseUI()
    {
        ButtonClose();
        ShoppingPannel.DOMoveY(1080 * 2, 0f);
        ShoppingPannel.gameObject.SetActive(true);
        ShoppingPannel.DOMoveY(1080 / 2f, 1f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < ShoppingButton.Count; i++)
        {
            ShoppingButton[i].interactable = ShoppingButtonAble[i];
            ShoppingText[i].text = ShoppingTextResource[i].ToString();
        }
    }

    private IEnumerator corFunc_PopDownPurchaseUI()
    {
        ButtonClose();
        ShoppingPannel.gameObject.SetActive(true);
        ShoppingPannel.DOMoveY(1080 * 2f, 1f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(1f);

        ShoppingPannel.gameObject.SetActive(false);
    }

}
