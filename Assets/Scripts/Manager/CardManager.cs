using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    
    private List<GameObject> listGenCard;   //Game Object ���·� ������ ī�带 ����

    [SerializeField] private int marketMax; //���Ͽ� �ִ�� �� �� �ִ� ī���� ����
    private List<GameObject> listMarketCardGO;//���� ī�� ����Ʈ
    private List<CardObject> listMarketCardCO;//���� ī�� ����Ʈ

    private List<List<GameObject>> listPlayerCard;  //�÷��̾���� ���� ī�� ����Ʈ

    [SerializeField] private List<Transform> listMarketHolder;   //���Ͽ� ī�尡 �� Ȧ��


    [SerializeField] private GameObject TestCard;
    [SerializeField] private int TestAmount;

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�׽�Ʈ �Ҹ�
            TestSpendCard();
        }
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    private void Initialize()
    {
        listGenCard = new List<GameObject>();
        listMarketCardGO = new List<GameObject>();
        listMarketCardCO = new List<CardObject>();

        GenerationCardList();

    }

    private void GenerationCardList()
    {
        //�׽�Ʈ ����
        TestCardMake();

        //listGenCard�� ������ ī�带 ������ ������.

        //���Ͽ� �߰�
        Add_Market();

        //���� ī�� Ȧ���� �켱 �ʱ�ȭ �ؾ���.
        //�ν����Ϳ� ������ �������� �����Ǵ� Ȧ���� �������� ����
        //...
        RePosition_MarketCard();

    }

    private void TestCardMake()
    {
        for (int i = 0; i < TestAmount; i++)
        {
            listGenCard.Add(Instantiate(TestCard));
        }
    }
    private void TestSpendCard()
    {
        if (listMarketCardGO.Count != 0)
        {
            Destroy(listMarketCardGO[0]);
            listMarketCardGO.RemoveAt(0);
            Add_Market();
            RePosition_MarketCard();
        }
    }

    private void Add_Market()
    {
        int tmp = marketMax - listMarketCardGO.Count;
        GameObject tmpG;
        if (listMarketCardGO.Count < marketMax)
            for (int i = 0; i < tmp; i++)
            {
                tmpG = Get_Card();
                if (tmpG != null)
                {
                    listMarketCardGO.Add(tmpG);
                    listMarketCardCO.Add(tmpG.GetComponent<CardObject>());
                }
            }
    }



    private GameObject Get_MarketCard(int rsh)
    {
        return listMarketCardGO[rsh];
    }

    /// <summary>
    /// ���� ī�� ������ ������
    /// </summary>
    private void RePosition_MarketCard()
    {
        if (listMarketHolder.Count != marketMax)
            Debug.LogError("!!Position Count unMatched MarketMax!!");
        for(int i = 0; i < listMarketCardGO.Count; i++)
        {
            //��ġ ����
            listMarketCardGO[i].transform.position = listMarketHolder[i].transform.position;
        }
    }

    /// <summary>
    /// ���� ī�� Ȧ�� Transform �� ���������� �����ϰ�.
    /// </summary>
    /// <param name="position"></param>
    public void Set_MarketHolderList(List<Transform> position)
    {
        listMarketHolder = position;
    }

    /// <summary>
    /// �ܺο��� ȣ���Ͽ� ī�� ����Ʈ �� ���� ù��° �ε����� ī�带 �������� ��.
    /// </summary>
    /// <returns></returns>
    public GameObject Get_Card()
    {
        if (listGenCard.Count > 0)
        {
            GameObject card = listGenCard[0];
            listGenCard.RemoveAt(0);
            return card;
        }
        else
            return null;
    }



    /// <summary>
    /// ī�� ���� ���� ����
    /// </summary>
    /// <param name="cardNum">������ ī�� ��ȣ</param>
    /// <param name="Happy">�÷��̾��� ��ȭ</param>
    /// <returns>5���� �׸��� � ǰ���� ���� �������� bool ������ ����.</returns>
    public List<bool> Check_BuyThisCard(int cardNum, List<int> Happy)
    {
        List<int> tmpPrice;
        List<bool> buyAble = new List<bool>(5); //5���� �׸�
        //list Market Card ���� ī�� ��ȣ�� ��ȸ�� �� �ش��ϴ� �׸�� ���ݺ�
        for (int i = 0; i < listMarketCardGO.Count; i++)
            //ī�� ��ȣ�� ������. ���� ���� ���� Ȯ��
            if (listMarketCardCO[i].GetCardNum() == cardNum)
            {
                tmpPrice = listMarketCardCO[i].GetPrice();
                for (int j = 0; j < 5; j++)
                {
                    if (tmpPrice[j] + tmpPrice[j + 5] <= Happy[j])
                        buyAble[j] = true;
                }
                break;
            }


        return buyAble;

    }

}
