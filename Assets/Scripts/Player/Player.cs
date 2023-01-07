using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    private int _order;            // 플레이 순서
    [SerializeField] private int _scorehappy;       // 점수가 될 자원
    [SerializeField] private List<int> _resource;   // 갖고 있는 돈과 자원
    //private List<Card> _hands;   // 핸드에 있는 카드 리스트
    private List<GameObject> _fields;    // 필드에 있는 카드 리스트
    private int slotUsed;
    public const int maxSlot = 10;
    public float cardGap;  // 카드 사이의 간격

    public int Order { get => _order; set => _order = value; }
    public int Scorehappy { get => _scorehappy; set => _scorehappy = value; }
    public List<int> Resource { get => _resource; set => _resource = value; }
    public List<GameObject> Fields { get => _fields; set => _fields = value; }
    public int SlotUsed { get => slotUsed; }
    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// _resource와 _fields를 초기화
    /// </summary>
    public void Initialize()
    {
        //_resource = new List<int>();
        _fields = new List<GameObject>();
    }

    /// <summary>
    /// 턴 종류 후 카드 효과에 의한 돈,자원의 증가를 반영합니다.
    /// </summary>
    /// <param name="gain">얼마나 얻었는지</param>
    public void Gain(List<int> gain)    // 
    {
        for (int i = 0; i < _resource.Count; i++)
        {
            Debug.Log(_resource.Count + " " + gain.Count);
            _resource[i] += gain[i];
        }
    }

    /// <summary>
    /// 카드 구매에 사용한 돈,자원의 감소를 반영합니다.
    /// </summary>
    /// <param name="used">얼마나 지불했는지</param>
    public void Use(List<int> used)     // 
    {
        for (int i = 0; i < used.Count; i++)
        {
            _resource[i] -= used[i];
        }
    }

    /// <summary>
    /// 사용자 필드에 카드(newcard) 추가
    /// </summary>
    /// <param name="newcard">추가할 카드</param>
    public void AddCard(GameObject newcard) 
    {
        _fields.Add(newcard);
        slotUsed += newcard.GetComponent<CardScript>().GetSlot();
        int index = _fields.Count-1;
        _fields[index].transform.parent = transform;
        _fields[index].transform.DOLocalMove(Vector3.right * cardGap * index, 0.5f);
        //Debug.Log(_fields[index].transform.localPosition);
    }

    /// <summary>
    /// 사용자 필드에서 카드 제거
    /// </summary>
    /// <param name="card">제거할 카드</param>
    public void RemoveCard(GameObject card) // 
    {
        int cardNum = card.GetComponent<CardScript>().GetCardNum();
        int i;
        for(i=0;i<_fields.Count;i++)
        {
            if (_fields[i].GetComponent<CardScript>().GetCardNum() == cardNum)
            {
                _fields.RemoveAt(i);
                break;
            }
        }
        if (i == _fields.Count)
            return;
        for(; i<_fields.Count;i++)
        {
            _fields[i].transform.position = gameObject.transform.position + Vector3.right * cardGap * i;
        }
        card.gameObject.SetActive(false);
        Destroy(card);
        slotUsed -= card.GetComponent<CardScript>().GetSlot();
    }

    /// <summary>
    /// 한 턴이 끝날 때마다 필요한 작업
    /// 필드에 있는 카드 효과 
    /// 필드에 있는 카드마다 턴 줄이기
    /// 구매를 하지 않았을 때 돈 또는 자원을 획득
    /// </summary>
    public void EndTurn()
    {
        List<GameObject> deleteTargets = new List<GameObject>();
        foreach(var card in _fields)
        {
            CardScript cs = card.GetComponent<CardScript>();
            this.Gain(cs.GetEffect());
            if (--(cs.TurnLeft) == 0)
                deleteTargets.Add(card);
        }
        foreach (var target in deleteTargets)
            RemoveCard(target);
    }

    /// <summary>
    /// 최종 점수 반환
    /// </summary>
    /// <returns>자원 중 플레이어에게 점수가 되는 것</returns>
    public int GetScore()
    {
        return _resource[_scorehappy];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
