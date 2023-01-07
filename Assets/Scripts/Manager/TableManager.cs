using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 테이블의 진행을 관리하는 메니저입니다.
/// 
/// 
/// 
/// </summary>
public class TableManager : MonoBehaviour
{
    
    public static TableManager instance;

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
    /// 테이블을 시작합니다
    /// </summary>
    public void StartTable()
    {
        StartCoroutine(corFunc_RollTable());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
            StartTable();
        if (Input.GetKeyDown(KeyCode.UpArrow))
            End_PlayerTurn();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            End_TableTurn();
    }


    /// <summary>
    /// 테이블을 회전시킵니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator corFunc_RollTable()
    {

        for (int i = 0; i < maxTurn; i++)
        {
            Debug.Log("Now Turn : " + i);

            for (int j = 0; j < maxPlayer; j++)
            {
                nowPlayerTurn = j;
                Debug.Log("Now Player : " + j);

                //플레이어 턴 실행
                Run_PlayerTurn(j);

                yield return new WaitUntil(() => playerTurnEnd == true);
                playerTurnEnd = false;

                Run_AfterPlayerTurn(j);

                yield return new WaitUntil(() => playerAfterTurnEnd == true);
                playerAfterTurnEnd = false;


            }

            //테이블 자체에 어떠한 효과가 나와야 한다면 호출
            Run_TableTurn();

            yield return new WaitUntil(() => TableTurnEnd == true);
            TableTurnEnd = false;

            //테이블 턴이 끝날시에 호출
            Run_AfterTableTurn();

            yield return new WaitUntil(() => TableAfterTurnEnd == true);
            TableTurnEnd = false;

        }
    }

    // 게임 로직
    // 시장에 카드가 들어온다
    // 카드를 구매 - 사용한다
    // 다음 플레이어에게 넘긴다.
    // 반복


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


        //마켓 충당
        CardManager.instance.Add_Market();

    }

    /// <summary>
    /// 테이블 턴이 시작될 때 호출
    /// </summary>
    private void Run_TableTurn()
    {
        //테이블 턴 시작시 함수 호출
    }

    /// <summary>
    /// 테이블 턴이 종료될 떄 호출
    /// </summary>
    private void Run_AfterTableTurn()
    {

        //테이블 턴 종료시의 함수 호출

    }

    /// <summary>
    /// 플레이어의 턴 종료시 호출해야 합니다.
    /// </summary>

    private void End_PlayerTurn()
    {
        playerTurnEnd = true;
    }
    
    /// <summary>
    /// 테이블의 턴 종료시 호출되어야 합니다.
    /// </summary>
    public void End_TableTurn()
    {
        TableTurnEnd = true;
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
