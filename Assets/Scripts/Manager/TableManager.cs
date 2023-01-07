using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// ���̺��� ������ �����ϴ� �޴����Դϴ�.
/// 
/// 
/// 
/// </summary>
public class TableManager : MonoBehaviour
{
    
    public static TableManager instance;

    public bool IsDebuging;

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

    [SerializeField] private TextMeshProUGUI TurnEndMessage;
    [SerializeField] private GameObject TurnEndBlock;
    [SerializeField] private GameObject TurnEndBlockImg;

    [SerializeField] private TextMeshProUGUI GameOverMessage;
    [SerializeField] private GameObject GameOverBlock;
    [SerializeField] private GameObject GameOverBlockImg;

    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private Image WinnerFill;
    [SerializeField] private List<TextMeshProUGUI> WinnerRank;
    [SerializeField] private List<TextMeshProUGUI> WinnerPlayer;
    [SerializeField] private Button ReturnButton;

    private int CountEndCards = 0;

    private bool hasInit = false;

    private void OnEnable()
    {
        if(instance == null)
            instance = this;
    }

    private void Awake()
    {
        TurnEndBlock.SetActive(false);
        TurnEndBlockImg.SetActive(false);
        TurnEndMessage.gameObject.SetActive(false);
        GameOverBlock.SetActive(false);
        GameOverBlockImg.SetActive(false);
        GameOverMessage.gameObject.SetActive(false);
        if (!hasInit)
            Initialize();
        for (int i = 0; i < maxPlayer; i++)
        {
            listPlayer[i].Order = i;
        }
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

        WinnerPanel.gameObject.SetActive(false);
        ReturnButton.interactable = false;
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

        //�������� ������ ����
        CardManager.instance.CheckBuyFirst();

        //���� ���
        //CardManager.instance.Add_Market();

        // test increase
        // increaseCEC();

        // �� ���� �޼��� ����
        StartCoroutine(EndMessage());

        // ���� ���� ���� Ȯ���ϱ�
        CheckGameOver();

        End_AfterPlayerTurn();

    }

    /// <summary>
    /// �� ���� �޼����� ����.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndMessage()
    {
        TurnEndMessage.gameObject.SetActive(true);
        TurnEndBlock.SetActive(true);
        TurnEndBlockImg.SetActive(true);
        if (IsDebuging)
            yield return new WaitForSeconds(0.1f);
        else
            yield return new WaitForSeconds(1f);

        TurnEndMessage.gameObject.SetActive(false);
        TurnEndBlock.SetActive(false);
        TurnEndBlockImg.SetActive(false);
    }

    /// <summary>
    /// ���� ���� ������ Ȯ���Ѵ�. 
    /// ������ ������ ���� ���� ������ ����Ѵ�.
    /// </summary>
    private void CheckGameOver()
    {
        if (CountEndCards >= 3)
        {
            StartCoroutine(OverMessage());
        }
    }

    /// <summary>
    /// ���� ���� ������ ����Ѵ�.
    /// </summary>
    /// <returns></returns>
    private IEnumerator OverMessage()
    {
        yield return new WaitForSeconds(2f);
        GameOverMessage.gameObject.SetActive(true);
        GameOverBlock.SetActive(true);
        GameOverBlockImg.SetActive(true);

        //���ھ� ���

        List<int> Score = new List<int>();
        List<int> Player = new List<int>();
        for (int i = 0; i < maxPlayer; i++)
        {
            Score.Add(listPlayer[i].Resource[i + 1]);
            Player.Add(i + 1);
        }
        int tmpint;
        for (int i = 0; i < maxPlayer - 1; i++)
        {
            for (int j = 0; j < maxPlayer - 1; j++)
            {
                if (Score[j] < Score[j + 1])
                {
                    tmpint = Score[j + 1];
                    Score[j + 1] = Score[j];
                    Score[j] = tmpint;

                    tmpint = Player[j + 1];
                    Player[j + 1] = Player[j];
                    Player[j] = tmpint;
                }
            }
        }

        yield return new WaitForSeconds(3f);

        WinnerPanel.gameObject.SetActive(true);

        WinnerFill.DOFillAmount(1f, 10f);

        for (int i = 0; i < maxPlayer; i++)
        {
            WinnerRank[i].text = (i + 1).ToString();
            WinnerPlayer[i].text = "Player : " + Player[i].ToString() +"\nScore : " + Score[i].ToString();
        }

        yield return new WaitForSeconds(10f);

        ReturnButton.interactable = true;

        //���� Ȯ�� ��ư -> �������� ���ư�

    }

    public void BTN_ReturnMain()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Cardmanager���� ȣ�� �����ϴ�. ���� ī�� ���� ������Ų��.
    /// </summary>
    public void increaseCEC()
    {
        CountEndCards++;
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

    public void End_PlayerSelfEnd(int rsh)
    {
        playerTurnEnd = true;
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
