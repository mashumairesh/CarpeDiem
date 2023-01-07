using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    
    private List<GameObject> listGenCard;   //Game Object ���·� ������ ī�带 ����

    [SerializeField] private int marketMax; //���Ͽ� �ִ�� �� �� �ִ� ī���� ����
    private List<GameObject> listMarketCardGO;//���� ī�� ����Ʈ
    private List<CardScript> listMarketCardCS;//���� ī�� ����Ʈ
    private List<CardData> listOrgData;

    private List<List<GameObject>> listPlayerCard;  //�÷��̾���� ���� ī�� ����Ʈ

    [SerializeField] private List<Transform> listMarketHolder;   //���Ͽ� ī�尡 �� Ȧ��
    [SerializeField] private Deck deck;     //��...

    [SerializeField] private GameObject TestCard;
    [SerializeField] private int TestAmount;

    

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
/*        //if(Input.GetKeyDown(KeyCode.I))
        //    Initialize();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�׽�Ʈ �Ҹ�
            TestSpendCard();
        }*/
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    private void Initialize()
    {
        listGenCard = new List<GameObject>();
        listMarketCardGO = new List<GameObject>();
        listMarketCardCS = new List<CardScript>();

        GenerationCardList();

    }

    /// <summary>
    /// ī�� ����Ʈ�� ����� Ȥ�� ī�� ����Ʈ���� �޾ƿ� ���Ͽ� �߰��մϴ�.
    /// </summary>
    private void GenerationCardList()
    {
        //�׽�Ʈ ����
        //TestCardMake();
        GenerateCardGameObject();

        //listGenCard�� ������ ī�带 ������ ������.

        //���Ͽ� �߰�
        Add_Market();

        //���� ī�� Ȧ���� �켱 �ʱ�ȭ �ؾ���.
        //�ν����Ϳ� ������ �������� �����Ǵ� Ȧ���� �������� ����
        //...
        RePosition_MarketCard();

    }

    private void GenerateCardGameObject()
    {
        listOrgData = new List<CardData>();
        listOrgData = deck.cards;
        int LoopAmount = listOrgData.Count;

        Debug.Log("Count" + listOrgData.Count);
        for (int i = 0; i < LoopAmount; i++)
        {
            listGenCard.Add(Instantiate(TestCard));
            listGenCard[i].GetComponent<CardScript>().Initalize(listOrgData[i]);
        }
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
            listMarketCardGO[0].transform.DOKill(false);
            Destroy(listMarketCardGO[0]);
            listMarketCardGO.RemoveAt(0);
            Add_Market();
            RePosition_MarketCard();
        }
    }
    /// <summary>
    /// �ڽ��� ���� ��� ī�� ����Ʈ�� ������� ���� ù��° �ε����� ī�带 ���Ͽ� �߰��մϴ�.
    /// </summary>
    public void Add_Market()
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
                    listMarketCardCS.Add(tmpG.GetComponent<CardScript>());
                }
            }



    }

    /// <summary>
    /// ���Ͽ������� ī�带 �����ɴϴ�.
    /// </summary>
    /// <param name="cardNum">ī���ȣ �Դϴ�.</param>
    /// <returns></returns>
    public GameObject Get_MarketCard(int cardNum)
    {
        GameObject tmpCard;
        int tmpindex = 99999;

        //������ ī�� ��ȣ�� list�� �����ϴ��� ã�� �ش� ����Ʈ �ε����� ����
        for (int i = 0; i < listMarketCardGO.Count; i++)
            if (listMarketCardCS[i].GetCardNum() == cardNum)
                tmpindex = i;
                //�߽߰� ����

        if(tmpindex == 99999)
        {
            Debug.LogError("Get_MarketCard : NO IN HAS MARKET!!");
            return null;
        }

        //�߰��� �ε����� ī�带 ���Ͽ��� �����ϰ� ����
        tmpCard = listMarketCardGO[tmpindex];
        listMarketCardGO.RemoveAt(tmpindex);
        listMarketCardCS[tmpindex].IsPurchased = true;
        if(listMarketCardCS[tmpindex].GetEffect()[5] == 1 )
        {
            Debug.Log("END CARD");
            //TableManager�� ThisEndCard ��� ���� �˸��� �Լ��� ȣ��
        }
        listMarketCardCS.RemoveAt(tmpindex);

        Add_Market();
        RePosition_MarketCard();

        //�ӽ� ī�� �̵�
        //tmpCard.transform.DOMove(new Vector2(100, 0),1f);

        return tmpCard;
    }

    /// <summary>
    /// ���� ī�� ������ ������
    /// </summary>
    private void RePosition_MarketCard()
    {
        /*        if (listMarketHolder.Count != marketMax)
                    Debug.LogError("!!Position Count unMatched MarketMax!!");
                for(int i = 0; i < listMarketCardGO.Count; i++)
                {
                    //��ġ ����
                    listMarketCardGO[i].transform.DOMove(listMarketHolder[i].transform.position, 0.3f);
                }*/

        //�������� ����
        //listMarketHolder�� ù �ε����� �� �ε����� �����Ͽ� ī����� ���ο� �ڵ� ����.

        float Length = ((listMarketHolder[listMarketHolder.Count - 1].transform.position.x - listMarketHolder[0].transform.position.x) / 9f);
        Vector3 VecOrg = listMarketHolder[0].transform.position;
        Vector3 tmpVec = listMarketHolder[0].transform.position;
        for (int i = 0; i < marketMax; i++)
        {
            //1. Transform �� ��������     + ( ( ( (2)B-(1)A ) / (Max - 1) ) * i )

            tmpVec = VecOrg;
            tmpVec.x += (Length * (float)i);
            listMarketCardGO[i].transform.DOMove(
                ( tmpVec )
                , 0.3f);

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
    /// ī�� ���� ���� ���θ� �����մϴ�
    /// </summary>
    /// <param name="cardNum">������ ī�� ��ȣ</param>
    /// <param name="Happy">�÷��̾��� ��ȭ</param>
    /// <returns>5���� �׸��� � ǰ���� ���� �������� bool ������ ����.</returns>
    public List<bool> Check_BuyThisCard(int cardNum, List<int> Happy)
    {
        List<int> tmpPrice;
        List<bool> buyAble = new List<bool>(); //5���� �׸�
        for (int i = 0; i < 5; i++) buyAble.Add(false);
        Debug.Log(buyAble.Count);
        //list Market Card ���� ī�� ��ȣ�� ��ȸ�� �� �ش��ϴ� �׸�� ���ݺ�
        for (int i = 0; i < listMarketCardGO.Count; i++)
            //ī�� ��ȣ�� ������. ���� ���� ���� Ȯ��
            if (listMarketCardCS[i].GetCardNum() == cardNum)
            {
                tmpPrice = listMarketCardCS[i].GetPrice();
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
