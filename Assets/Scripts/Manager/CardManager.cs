using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    
    private List<GameObject> listGenCard;   //Game Object 형태로 생성된 카드를 보관

    [SerializeField] private int marketMax; //마켓에 최대로 들어갈 수 있는 카드의 갯수
    private List<GameObject> listMarketCardGO;//마켓 카드 리스트
    private List<CardScript> listMarketCardCS;//마켓 카드 리스트
    private List<CardData> listOrgData;

    private List<List<GameObject>> listPlayerCard;  //플레이어들이 가진 카드 리스트

    [SerializeField] private List<Transform> listMarketHolder;   //마켓에 카드가 들어갈 홀더
    [SerializeField] private Deck deck;     //덱...
    [SerializeField] private bool CheckBuyFst;

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
            //테스트 소모
            TestSpendCard();
        }*/
    }

    /// <summary>
    /// 초기화
    /// </summary>
    private void Initialize()
    {
        listGenCard = new List<GameObject>();
        listMarketCardGO = new List<GameObject>();
        listMarketCardCS = new List<CardScript>();
        CheckBuyFst = false;
        GenerationCardList();

    }

    /// <summary>
    /// 카드 리스트를 만들어 혹은 카드 리스트들을 받아와 마켓에 추가합니다.
    /// </summary>
    private void GenerationCardList()
    {
        //테스트 생성
        //TestCardMake();
        GenerateCardGameObject();

        //listGenCard에 생성된 카드를 가져와 저장함.

        //마켓에 추가
        Add_Market();

        //마켓 카드 홀더를 우선 초기화 해야함.
        //인스펙터에 넣을지 동적으로 생성되는 홀더를 가져오게 할지
        //...

        //1번칸의 구매 여부를 확인
        

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
    /// 자신이 가진 모든 카드 리스트를 기반으로 가장 첫번째 인덱스의 카드를 마켓에 추가합니다.
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
                    //SoundManager.instance.PlayAudio(SoundType.LoadDeck);
                    listMarketCardGO.Add(tmpG);
                    listMarketCardCS.Add(tmpG.GetComponent<CardScript>());
                }
            }



    }

    /// <summary>
    /// 마켓에서부터 카드를 가져옵니다.
    /// </summary>
    /// <param name="cardNum">카드번호 입니다.</param>
    /// <returns></returns>
    public GameObject Get_MarketCard(int cardNum)
    {
        GameObject tmpCard;
        int tmpindex = 99999;

        //선택한 카드 번호를 list에 존재하는지 찾고 해당 리스트 인덱스를 저장
        for (int i = 0; i < listMarketCardGO.Count; i++)
            if (listMarketCardCS[i].GetCardNum() == cardNum)
                tmpindex = i;
        //발견시 저장

        if (tmpindex == 0)
            CheckBuyFst = true;

        if (tmpindex == 99999)
        {
            Debug.LogError("Get_MarketCard : NO IN HAS MARKET!!");
            return null;
        }

        //발견한 인덱스의 카드를 마켓에서 제거하고 리턴
        tmpCard = listMarketCardGO[tmpindex];
        listMarketCardGO.RemoveAt(tmpindex);
        listMarketCardCS[tmpindex].IsPurchased = true;
        if(listMarketCardCS[tmpindex].GetEffect()[5] == 1 )
        {
            Debug.Log("END CARD");
            //TableManager에 ThisEndCard 라는 것을 알리는 함수를 호출
            TableManager.instance.increaseCEC();
        }
        listMarketCardCS.RemoveAt(tmpindex);

        Add_Market();
        RePosition_MarketCard();
        SoundManager.instance.PlayAudio(SoundType.LoadDeck);
        //임시 카드 이동
        //tmpCard.transform.DOMove(new Vector2(100, 0),1f);

        return tmpCard;
    }

    /// <summary>
    /// 마켓 카드 포지션 재정렬
    /// </summary>
    private void RePosition_MarketCard()
    {

        SoundManager.instance.PlayAudio(SoundType.LoadDeck);
        //리포지션 변경
        //listMarketHolder의 첫 인덱스와 끝 인덱스를 참조하여 카드들을 내부에 자동 정렬.

        float Length = ((listMarketHolder[listMarketHolder.Count - 1].transform.position.x - listMarketHolder[0].transform.position.x) / 9f);
        Vector3 VecOrg = listMarketHolder[0].transform.position;
        Vector3 tmpVec = listMarketHolder[0].transform.position;
        for (int i = 0; i < listMarketCardGO.Count; i++)
        {
            //1. Transform 을 기준으로     + ( ( ( (2)B-(1)A ) / (Max - 1) ) * i )
            tmpVec = VecOrg;
            tmpVec.x += (Length * (float)i);
            listMarketCardGO[i].transform.DOMove(
                ( tmpVec )
                , 0.3f);
        }
        for (int i = 0; i < listMarketCardCS.Count; i++)
        {
            if (i < 2)
                listMarketCardCS[i].UpdateSaleInfo(-1);
            else if (i < 2 + 5)
                listMarketCardCS[i].UpdateSaleInfo(0);
            else
                listMarketCardCS[i].UpdateSaleInfo(1);
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
        List<bool> buyAble = new List<bool>(); //5가지 항목
        for (int i = 0; i < 5; i++) buyAble.Add(false);
        Debug.Log(buyAble.Count);
        //list Market Card 에서 카드 번호를 순회한 뒤 해당하는 항목과 가격비교
        for (int i = 0; i < listMarketCardGO.Count; i++)
            //카드 번호가 동일함. 구매 가능 여부 확인
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

    public void CheckBuyFirst()
    {
        if(!CheckBuyFst)
        {
            GameObject tmpCard;
            tmpCard = listMarketCardGO[0];
            listMarketCardGO.RemoveAt(0);
            listMarketCardCS[0].IsPurchased = true;
            if (listMarketCardCS[0].GetEffect()[5] == 1)
            {
                Debug.Log("END CARD");
                //TableManager에 ThisEndCard 라는 것을 알리는 함수를 호출
                TableManager.instance.increaseCEC();
            }
            listMarketCardCS.RemoveAt(0);
            Destroy(tmpCard);
        }
        CheckBuyFst = false;

        Add_Market();
        RePosition_MarketCard();
    }

    public void UpdatePlayerSaleInfo(int curPlayer)
    {
        for (int i = 0; i < listMarketCardCS.Count; i++)
            listMarketCardCS[i].UpdatePlayerSaleInfo(curPlayer);
    }
}
