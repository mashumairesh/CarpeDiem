using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    private int _order;            // �÷��� ����
    [SerializeField] private int _scorehappy;       // ������ �� �ڿ�
    [SerializeField] private List<int> _resource;   // ���� �ִ� ���� �ڿ�
    //private List<Card> _hands;   // �ڵ忡 �ִ� ī�� ����Ʈ
    private List<GameObject> _fields;    // �ʵ忡 �ִ� ī�� ����Ʈ
    private int slotUsed;
    public const int maxSlot = 10;
    public float cardGap;  // ī�� ������ ����

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
    /// _resource�� _fields�� �ʱ�ȭ
    /// </summary>
    public void Initialize()
    {
        //_resource = new List<int>();
        _fields = new List<GameObject>();
    }

    /// <summary>
    /// �� ���� �� ī�� ȿ���� ���� ��,�ڿ��� ������ �ݿ��մϴ�.
    /// </summary>
    /// <param name="gain">�󸶳� �������</param>
    public void Gain(List<int> gain)    // 
    {
        for (int i = 0; i < _resource.Count; i++)
        {
            Debug.Log(_resource.Count + " " + gain.Count);
            _resource[i] += gain[i];
        }
    }

    /// <summary>
    /// ī�� ���ſ� ����� ��,�ڿ��� ���Ҹ� �ݿ��մϴ�.
    /// </summary>
    /// <param name="used">�󸶳� �����ߴ���</param>
    public void Use(List<int> used)     // 
    {
        for (int i = 0; i < used.Count; i++)
        {
            _resource[i] -= used[i];
        }
    }

    /// <summary>
    /// ����� �ʵ忡 ī��(newcard) �߰�
    /// </summary>
    /// <param name="newcard">�߰��� ī��</param>
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
    /// ����� �ʵ忡�� ī�� ����
    /// </summary>
    /// <param name="card">������ ī��</param>
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
    /// �� ���� ���� ������ �ʿ��� �۾�
    /// �ʵ忡 �ִ� ī�� ȿ�� 
    /// �ʵ忡 �ִ� ī�帶�� �� ���̱�
    /// ���Ÿ� ���� �ʾ��� �� �� �Ǵ� �ڿ��� ȹ��
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
    /// ���� ���� ��ȯ
    /// </summary>
    /// <returns>�ڿ� �� �÷��̾�� ������ �Ǵ� ��</returns>
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
