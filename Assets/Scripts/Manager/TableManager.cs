using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̺��� ������ �����ϴ� �޴����Դϴ�.
/// 
/// 
/// 
/// </summary>
public class TableManager : MonoBehaviour
{
    
    public static TableManager instance;

    [Tooltip("�ִ� �� Ƚ�� �Դϴ�.")]
    [SerializeField] private int maxTurn;

    [SerializeField] private int maxPlayer;
    [SerializeField] private int nowPlayerTurn;

    [SerializeField] private bool turnEnd;

    private void OnEnable()
    {
        if(instance == null)
            instance = this;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        nowPlayerTurn = 0;
        turnEnd = false;
    }

    /// <summary>
    /// ���̺��� �����մϴ�
    /// </summary>
    public void StartTable()
    {
        StartCoroutine(corFunc_RollTable());
    }
    


    /// <summary>
    /// ���̺��� ȸ����ŵ�ϴ�.
    /// </summary>
    /// <returns></returns>
    private IEnumerator corFunc_RollTable()
    {

        for (int i = 0; i < maxTurn; i++)
        {
            Debug.Log("Now Turn : " + i);

            for (int j = 0; j < maxPlayer; j++)
            {
                Debug.Log("Now Player : " + j);

                yield return new WaitUntil(() => turnEnd == true);
            }
        }
    }

    // ���� ����
    // ���忡 ī�尡 ���´�
    // ī�带 ���� - ����Ѵ�
    // ���� �÷��̾�� �ѱ��.
    // �ݺ�





}
