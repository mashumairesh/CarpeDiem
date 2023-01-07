using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    
    private List<GameObject> listGenCard;   //Game Object 형태로 생성된 카드를 보관

    [SerializeField] private int marketMax; //마켓에 최대로 들어갈 수 있는 카드의 갯수
    private List<GameObject> listMarketCardGO;//마켓 카드 리스트
    private List<CardObject> listMarketCardCO;//마켓 카드 리스트

    private List<List<GameObject>> listPlayerCard;  //플레이어들이 가진 카드 리스트

    [SerializeField] private List<Transform> listMarketHolder;   //마켓에 카드가 들어갈 홀더


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
            //테스트 소모
            TestSpendCard();
        }
    }

    /// <summary>
    /// 초기화
    /// </summary>
    private void Initialize()
    {
        listGenCard = new List<GameObject>();
        listMarketCardGO = new List<GameObject>();
        listMarketCardCO = new List<CardObject>();

        GenerationCardList();

    }

    /// <summary>
    /// 카드 리스트를 만들어 혹은 카드 리스트들을 받아와 마켓에 추가합니다.
    /// </summary>
    private void GenerationCardList()
    {
        //테스트 생성
        TestCardMake();

        //listGenCard에 생성된 카드를 가져와 저장함.

        //마켓에 추가
        Add_Market();

        //마켓 카드 홀더를 우선 초기화 해야함.
        //인스펙터에 넣을지 동적으로 생성되는 홀더를 가져오게 할지
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
    /// <summary>
    /// 자신이 가진 모든 카드 리스트를 기반으로 가장 첫번째 인덱스의 카드를 마켓에 추가합니다.
    /// </summary>
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


    /// <summary>
    /// 마켓에서부터 카드를 가져옵니다.
    /// </summary>
    /// <param name="CardCode">카드번호 입니다.</param>
    /// <returns></returns>
    public GameObject Get_MarketCard(int CardCode)
    {
        GameObject tmpCard;
        int tmpindex = 99999;

        //선택한 카드 번호를 list에 존재하는지 찾고 해당 리스트 인덱스를 저장
        for (int i = 0; i < listMarketCardGO.Count; i++)
            if (listMarketCardCO[i].GetCardNum() == CardCode)
                tmpindex = i;
                //발견시 저장

        if(tmpindex == 99999)
        {
            Debug.LogError("Get_MarketCard : NO IN HAS MARKET!!");
            return null;
        }

        //발견한 인덱스의 카드를 마켓에서 제거하고 리턴
        tmpCard = listMarketCardGO[tmpindex];
        listMarketCardGO.RemoveAt(tmpindex);
        listMarketCardCO.RemoveAt(tmpindex);
        return tmpCard;
    }

    /// <summary>
    /// 마켓 카드 포지션 재정렬
    /// </summary>
    private void RePosition_MarketCard()
    {
        if (listMarketHolder.Count != marketMax)
            Debug.LogError("!!Position Count unMatched MarketMax!!");
        for(int i = 0; i < listMarketCardGO.Count; i++)
        {
            //위치 변경
            listMarketCardGO[i].transform.position = listMarketHolder[i].transform.position;
        }
    }

    /// <summary>
    /// 마켓 카드 홀더 Transform 을 지역변수로 저장하게.
    /// </summary>
    /// <param name="position"></param>
    public void Set_MarketHolderList(List<Transform> position)
    {
        listMarketHolder = position;
    }

    /// <summary>
    /// 외부에서 호출하여 카드 리스트 중 가장 첫번째 인덱스의 카드를 가져가게 됨.
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
    /// 카드 구매 가능 여부를 리턴합니다
    /// </summary>
    /// <param name="cardNum">선택한 카드 번호</param>
    /// <param name="Happy">플레이어측 재화</param>
    /// <returns>5가지 항목중 어떤 품목이 구매 가능한지 bool 변수로 전달.</returns>
    public List<bool> Check_BuyThisCard(int cardNum, List<int> Happy)
    {
        List<int> tmpPrice;
        List<bool> buyAble = new List<bool>(5); //5가지 항목
        //list Market Card 에서 카드 번호를 순회한 뒤 해당하는 항목과 가격비교
        for (int i = 0; i < listMarketCardGO.Count; i++)
            //카드 번호가 동일함. 구매 가능 여부 확인
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
