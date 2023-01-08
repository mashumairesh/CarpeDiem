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
    [SerializeField] private GameObject ShoppingClickBlocker;
    [SerializeField] private GameObject ShoppingWorldClickBlocker;
    [SerializeField] private GameObject ShoppingBreaker;

    [SerializeField] private List<Button> ShoppingButton;
    [SerializeField] private List<bool> ShoppingButtonAble;
    [SerializeField] private List<TextMeshProUGUI> ShoppingText;
    [SerializeField] private List<int> ShoppingTextResource;
    
    [SerializeField] private List<UpScorePopup> upScorePopup;

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
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            Popup_PurchaseUI(0, testBool, testInt);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Popdown_PurchaseUI(0);
        }*/
    }

    public void Popup_PurchaseUI(int cardNum, List<bool> Able, List<int> resource)
    {
        ShoppingClickBlocker.SetActive(true);
        ShoppingWorldClickBlocker.SetActive(true);

        CardNum = cardNum;
        ShoppingButtonAble = Able;
        ShoppingTextResource = resource;
        StartCoroutine(corFunc_PopupPurchaseUI());
        for (int i = 0; i < ShoppingButton.Count; i++)
        {
            if (ShoppingTextResource[i] < 99999)
                ShoppingText[i].text = ShoppingTextResource[i].ToString();
            else
                ShoppingText[i].text = "X";
        }
    }

    public void Popdown_PurchaseUI(int rsh)
    {

        List<int> tmp = new List<int>();
        for (int i = 0; i < 5; i++) tmp.Add(0);
        tmp[rsh] = ShoppingTextResource[rsh] + ShoppingTextResource[rsh + 5];
        StartCoroutine(corFunc_PopDownPurchaseUI());

        Debug.Log(TableManager.instance.Get_NowPlayerScript());
        Debug.Log(CardNum);

        TableManager.instance.Get_NowPlayerScript().AddCard(CardManager.instance.Get_MarketCard(CardNum));
        TableManager.instance.Get_NowPlayerScript().Use(tmp);
        TableManager.instance.End_PlayerTurn();

    }

    public void ButtonClose()
    {
        for (int i = 0; i < ShoppingButton.Count; i++)
        {
            ShoppingButton[i].interactable = false;
        }
    }

    public void BTN_CancelShopping()
    {

        StartCoroutine(corFunc_PopDownPurchaseUI());
    }

    private IEnumerator corFunc_PopupPurchaseUI()
    {
        SoundManager.instance.PlayAudio(SoundType.UIOn);
        ButtonClose();
        ShoppingPannel.DOMoveY(1080 * 2, 0f);
        ShoppingPannel.gameObject.SetActive(true);
        ShoppingPannel.DOMoveY(1080 / 2f, 0.5f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.5f);

        ShoppingBreaker.SetActive(true);

        for (int i = 0; i < ShoppingButton.Count; i++)
        {
            ShoppingButton[i].interactable = ShoppingButtonAble[i];
        }
    }

    private IEnumerator corFunc_PopDownPurchaseUI()
    {
        SoundManager.instance.PlayAudio(SoundType.UIOff);
        ButtonClose();
        ShoppingBreaker.SetActive(false);
        ShoppingPannel.gameObject.SetActive(true);
        ShoppingPannel.DOMoveY(1080 * 2f, 0.5f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.5f);

        ShoppingPannel.gameObject.SetActive(false);
        ShoppingClickBlocker.SetActive(false);
        ShoppingWorldClickBlocker.SetActive(false);
    }

    public UpScorePopup Get_UpScore(int rsh)
    {
        return upScorePopup[rsh];
    }

}
