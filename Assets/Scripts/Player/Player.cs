using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _order;            // �÷��� ����
    private int _scorehappy;       // ������ �� �ڿ�
    private List<int> _resource;   // ���� �ִ� ���� �ڿ�
    //private List<Card> _hands;   // �ڵ忡 �ִ� ī�� ����Ʈ
    private List<GameObject> _fields;    // �ʵ忡 �ִ� ī�� ����Ʈ
     
    public float cardGap;

    public int Order { get => _order; set => _order = value; }
    public int Scorehappy { get => _scorehappy; set => _scorehappy = value; }
    public List<int> Resource { get => _resource; set => _resource = value; }
    public List<GameObject> Fields { get => _fields; set => _fields = value; }

    public void Gain(List<int> gain)    // �� ���� �� ī�� ȿ���� ���� ��,�ڿ��� ����
    {
        for (int i = 0; i < gain.Count; i++)
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

    public void AddCard(GameObject newcard) //����� �ʵ忡 ī�� �߰�
    {
        _fields.Add(newcard);
        int index = _fields.Count-1;
        _fields[index].transform.position = gameObject.transform.position + Vector3.right * cardGap * index;
    }

    public void RemoveCard(GameObject card) // ����� �ʵ忡�� ī�� ����
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
