using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _order;            // 플레이 순서
    private int _scorehappy;       // 점수가 될 자원
    private List<int> _resource;   // 갖고 있는 돈과 자원
    //private List<Card> _hands;   // 핸드에 있는 카드 리스트
    private List<GameObject> _fields;    // 필드에 있는 카드 리스트
     
    public float cardGap;

    public int Order { get => _order; set => _order = value; }
    public int Scorehappy { get => _scorehappy; set => _scorehappy = value; }
    public List<int> Resource { get => _resource; set => _resource = value; }
    public List<GameObject> Fields { get => _fields; set => _fields = value; }

    public void Gain(List<int> gain)    // 턴 종류 후 카드 효과에 의한 돈,자원의 증가
    {
        for (int i = 0; i < gain.Count; i++)
        {
            _resource[i] += gain[i];
        }
    }

    public void Use(List<int> used)     // 카드 구매에 사용한 돈,자원
    {
        for (int i = 0; i < used.Count; i++)
        {
            _resource[i] -= used[i];
        }
    }

    public void AddCard(GameObject newcard) //사용자 필드에 카드 추가
    {
        _fields.Add(newcard);
        int index = _fields.Count-1;
        _fields[index].transform.position = gameObject.transform.position + Vector3.right * cardGap * index;
    }

    public void RemoveCard(GameObject card) // 사용자 필드에서 카드 제거
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
        for(; i<_fields.Count;i++)
        {
            _fields[i].transform.position = gameObject.transform.position + Vector3.right * cardGap * i;
        }
        Destroy(card);
    }

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
