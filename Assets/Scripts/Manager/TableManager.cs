using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Tooltip("4���� ? Ȥ�� 2���� �������� ����")]
    [SerializeField] private int maxPlayer;
    [SerializeField] private int nowPlayerTurn;

    [SerializeField] private bool playerTurnEnd;
    [SerializeField] private bool playerAfterTurnEnd;
    [SerializeField] private bool TableTurnEnd;
    [SerializeField] private bool TableAfterTurnEnd;

    [SerializeField] private List<Player> listPlayer;

    [SerializeField] private TextMeshProUGUI tmpSpendTurn;  //���� ��
    [SerializeField] private TextMeshProUGUI tmpLimitTurn;  //�ִ� ��

    [SerializeField] private TextMeshProUGUI tmpNowTurn;

    [SerializeField] private List<TestPointPanel> testPointPanel;

    private bool hasInit = false;

    private void OnEnable()
    {
        if(instance == null)
            instance = this;
    }

    private void Awake()
    {
        if (!hasInit)
            Initialize();
    }

    private void Start()
    {
        StartTable();
    }

    private void Initialize()
    {
        hasInit = true;

        nowPlayerTurn = 0;
        playerTurnEnd = false;
        playerAfterTurnEnd = false;
        TableTurnEnd = false;
        TableAfterTurnEnd = false;
    }

    /// <summary>
    /// ���̺��� �����մϴ�
    /// </summary>
    public void StartTable()
    {
        StartCoroutine(corFunc_RollTable());
    }

    private void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.End))
            StartTable();
        if (Input.GetKeyDown(KeyCode.UpArrow))
            End_PlayerTurn();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            End_TableTurn();*/
    }


    /// <summary>
    /// ���̺��� ȸ����ŵ�ϴ�.
    /// </summary>
    /// <returns></returns>
    private IEnumerator corFunc_RollTable()
    {
        DrawPannel();

        for (int i = 0; i < maxTurn; i++)
        {
            Debug.Log("Now Turn : " + i);

            for (int j = 0; j < maxPlayer; j++)
            {
                nowPlayerTurn = j;
                DrawPannel();

                //�÷��̾� �� ����
                Run_PlayerTurn(j);

                yield return new WaitUntil(() => playerTurnEnd == true);
                playerTurnEnd = false;

                Run_AfterPlayerTurn(j);

                yield return new WaitUntil(() => playerAfterTurnEnd == true);
                playerAfterTurnEnd = false;


                DrawPannel();
            }

            //���̺� ��ü�� ��� ȿ���� ���;� �Ѵٸ� ȣ��
            Run_TableTurn();

            yield return new WaitUntil(() => TableTurnEnd == true);
            TableTurnEnd = false;

            //���̺� ���� �����ÿ� ȣ��
            Run_AfterTableTurn();

            yield return new WaitUntil(() => TableAfterTurnEnd == true);
            TableTurnEnd = false;

        }
    }

    // ���� ����
    // ���忡 ī�尡 ���´�
    // ī�带 ���� - ����Ѵ�
    // ���� �÷��̾�� �ѱ��.
    // �ݺ�

    private void DrawPannel()
    {
        bool tmp = false;
        for (int i = 0; i < 4; i++)
        {
            if (i == nowPlayerTurn)
                tmp = true;
            else
                tmp = false;
            testPointPanel[i].DrawInfo(tmp, listPlayer[i].Resource);
        }
    }

    /// <summary>
    /// �ش��ϴ� �÷��̾� ���� �����Ѵ�.
    /// </summary>
    /// <param name="rsh"></param>
    private void Run_PlayerTurn(int rsh)
    {
        //���� �κ�
        //�÷��̾��� ����� Ȯ��


    }


    /// <summary>
    /// �÷��̾��� ���� ����� �� ����˴ϴ�.
    /// ī���� ���� �߰� �̷��͵��� �ʿ��ϸ�
    /// </summary>
    /// <param name="rsh"></param>
    private void Run_AfterPlayerTurn(int rsh)
    {
        //�÷��̾��� ��ȭ Ȯ��

        listPlayer[nowPlayerTurn].EndTurn();

        //���� ���
        CardManager.instance.Add_Market();

        End_AfterPlayerTurn();

    }

    /// <summary>
    /// ���̺� ���� ���۵� �� ȣ��
    /// </summary>
    private void Run_TableTurn()
    {
        //���̺� �� ���۽� �Լ� ȣ��
        End_TableTurn();
    }

    /// <summary>
    /// ���̺� ���� ����� �� ȣ��
    /// </summary>
    private void Run_AfterTableTurn()
    {
        //���̺� �� ������� �Լ� ȣ��
        End_AfterTableTurn();
    }

    /// <summary>
    /// �÷��̾��� �� ����� ȣ���ؾ� �մϴ�.
    /// </summary>

    public void End_PlayerTurn()
    {
        playerTurnEnd = true;
    }

    public void End_AfterPlayerTurn()
    {
        playerAfterTurnEnd = true;
    }

    /// <summary>
    /// ���̺��� �� ����� ȣ��Ǿ�� �մϴ�.
    /// </summary>
    public void End_TableTurn()
    {
        TableTurnEnd = true;
    }

    public void End_AfterTableTurn()
    {
        TableAfterTurnEnd = true;
    }

    public int Get_NowPlayer()
    {
        return nowPlayerTurn;
    }

    public List<int> Get_NowPlayerResource()
    {

        return listPlayer[nowPlayerTurn].Resource;

    }

    public Player Get_NowPlayerScript()
    {

        return listPlayer[nowPlayerTurn];
    
    }




}
