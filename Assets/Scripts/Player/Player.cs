using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _scorehappy;       // 점수가 될 자원
    private List<int> _resource;   // 갖고 있는 돈과 자원
    //private List<Card> _hands;   // 핸드에 있는 카드 리스트
    private List<Card> _fields;    // 필드에 있는 카드 리스트
    private int _finalscore;       // 최종 점수

    public Player(int scorehappy)  // 무엇으로 점수를 낼지 정함
    {
        _scorehappy = scorehappy;
        _resource = new List<int>(5);
        _fields = new List<Card>();
        _finalscore = 0;
    }

    public void Gain(List<int> gain)    // 턴 종류 후 카드 효과에 의한 돈,자원의 증가
    {
        for(int i=0;i<gain.Count;i++)
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


    public void AddCard(Card newcard) //사용자 필드에 카드 추가
    {
        _fields.Add(newcard);
    }

    public void RemoveCard(Card removecard) // 사용자 필드에서 카드 제거
    {
        private int cardNum = removecard.cardNum;
        for(int i=0;i<_fields.Count;i++)
        {
            if (_fields[i].cardNum == cardNum)
            {
                _fields.RemoveAt(i);
                break;
            }
        }
    }

    public int GetScore()
    {
        _finalscore = _Happy[_scorehappy];
        return _finalscore;
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
