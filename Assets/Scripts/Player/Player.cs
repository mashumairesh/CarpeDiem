using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _scorehappy;       // ������ �� �ڿ�
    private List<int> _resource;   // ���� �ִ� ���� �ڿ�
    //private List<Card> _hands;   // �ڵ忡 �ִ� ī�� ����Ʈ
    private List<Card> _fields;    // �ʵ忡 �ִ� ī�� ����Ʈ
    private int _finalscore;       // ���� ����

    public Player(int scorehappy)  // �������� ������ ���� ����
    {
        _scorehappy = scorehappy;
        _resource = new List<int>(5);
        _fields = new List<Card>();
        _finalscore = 0;
    }

    public void Gain(List<int> gain)    // �� ���� �� ī�� ȿ���� ���� ��,�ڿ��� ����
    {
        for(int i=0;i<gain.Count;i++)
        {
            _resource[i] += gain[i];
        }
    }

    public void Use(List<int> used)     // ī�� ���ſ� ����� ��,�ڿ�
    {
        for (int i = 0; i < used.Count; i++)
        {
            _resource[i] -= used[i];
        }
    }


    public void AddCard(Card newcard) //����� �ʵ忡 ī�� �߰�
    {
        _fields.Add(newcard);
    }

    public void RemoveCard(Card removecard) // ����� �ʵ忡�� ī�� ����
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
