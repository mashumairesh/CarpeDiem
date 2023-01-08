using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// 테이블의 진행을 관리하는 메니저입니다.
/// 
/// 
/// 
/// </summary>
public class TableManager : MonoBehaviour
{
    
    public static TableManager instance;

    public bool IsDebuging;

    [Tooltip("최대 턴 횟수 입니다.")]
    [SerializeField] private int maxTurn;

    [Tooltip("4명을 ? 혹은 2인을 기준으로 제작")]
    [SerializeField] private int maxPlayer;
    [SerializeField] private int nowPlayerTurn;

    [SerializeField] private bool playerTurnEnd;
    [SerializeField] private bool playerAfterTurnEnd;
    [SerializeField] private bool TableTurnEnd;
    [SerializeField] private bool TableAfterTurnEnd;

    [SerializeField] private List<Player> listPlayer;

    [SerializeField] private TextMeshProUGUI tmpSpendTurn;  //지난 턴
    [SerializeField] private TextMeshProUGUI tmpLimitTurn;  //최대 턴

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
    /// 테이블을 시작합니다
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
    /// 테이블을 회전시킵니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator corFunc_RollTable()
    {
        DrawPannel();

        for (int i = 0; i < maxTurn; i++) // maxRound
        {
            Debug.Log("Now Turn : " + i);

            for (int j = 0; j < maxPlayer; j++)
            {
                nowPlayerTurn = j;
                DrawPannel();
                CardManager.instance.UpdatePlayerSaleInfo(j + 1);
                //플레이어 턴 실행
                Run_PlayerTurn(j);

                yield return new WaitUntil(() => playerTurnEnd == true);
                playerTurnEnd = false;

                Run_AfterPlayerTurn(j);

                yield return new WaitUntil(() => playerAfterTurnEnd == true);
                playerAfterTurnEnd = false;

                yield return new WaitForSeconds(0.5f);

                SoundManager.instance.PlayAudio(SoundType.Bell);

                DrawPannel();
            }

            //테이블 자체에 어떠한 효과가 나와야 한다면 호출
            Run_TableTurn();

            yield return new WaitUntil(() => TableTurnEnd == true);
            TableTurnEnd = false;

            //테이블 턴이 끝날시에 호출
            Run_AfterTableTurn();

            // 게임 종료 조건 확인하기
            CheckGameOver();

            yield return new WaitUntil(() => TableAfterTurnEnd == true);
            TableTurnEnd = false;

        }
    }

    // 게임 로직
    // 시장에 카드가 들어온다
    // 카드를 구매 - 사용한다
    // 다음 플레이어에게 넘긴다.
    // 반복

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
    /// 해당하는 플레이어 턴을 실행한다.
    /// </summary>
    /// <param name="rsh"></param>
    private void Run_PlayerTurn(int rsh)
    {
        //실행 부분
        //플레이어의 제어권 확보


    }


    /// <summary>
    /// 플레이어의 턴이 종료된 뒤 실행됩니다.
    /// 카드의 정렬 추가 이런것들이 필요하며
    /// </summary>
    /// <param name="rsh"></param>
    private void Run_AfterPlayerTurn(int rsh)
    {
        //플레이어의 재화 확보
        listPlayer[nowPlayerTurn].EndTurn();

        //구매하지 않으면 제거
        CardManager.instance.CheckBuyFirst();

        //마켓 충당
        //CardManager.instance.Add_Market();

        // test increase
        // increaseCEC();

        // 턴 종류 메세지 띄우기
        StartCoroutine(EndMessage());


        End_AfterPlayerTurn();

    }

    /// <summary>
    /// 턴 종료 메세지를 띄운다.
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
    /// 게임 종료 조건을 확인한다. 
    /// 조건이 맞으면 게임 종료 문구를 출력한다.
    /// </summary>
    private void CheckGameOver()
    {
        if (CountEndCards >= 3)
        {
            StartCoroutine(OverMessage());
        }
    }

    /// <summary>
    /// 게임 종료 문구를 출력한다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator OverMessage()
    {
        GameOverMessage.gameObject.SetActive(true);
        GameOverBlock.SetActive(true);
        GameOverBlockImg.SetActive(true);

        //스코어 계산

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

        //종료 확인 버튼 -> 메인으로 돌아감

    }

    public void BTN_ReturnMain()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Cardmanager에서 호출 가능하다. 종료 카드 수를 증가시킨다.
    /// </summary>
    public void increaseCEC()
    {
        CountEndCards++;
    }

    /// <summary>
    /// 테이블 턴이 시작될 때 호출
    /// </summary>
    private void Run_TableTurn()
    {
        //테이블 턴 시작시 함수 호출
        End_TableTurn();
    }

    /// <summary>
    /// 테이블 턴이 종료될 떄 호출
    /// </summary>
    private void Run_AfterTableTurn()
    {
        //테이블 턴 종료시의 함수 호출
        End_AfterTableTurn();
    }

    public void End_PlayerSelfEnd(int rsh)
    {
        playerTurnEnd = true;
    }

    /// <summary>
    /// 플레이어의 턴 종료시 호출해야 합니다.
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
    /// 테이블의 턴 종료시 호출되어야 합니다.
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
