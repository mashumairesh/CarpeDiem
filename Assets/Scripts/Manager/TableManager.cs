using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Space))
            turnEnd = true;
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
                Debug.Log("Now Player : " + j);

                //플레이어 턴 실행
                Run_PlayerTurn(j);

                yield return new WaitUntil(() => turnEnd == true);
                turnEnd = false;




            }
        }
    }

    // 게임 로직
    // 시장에 카드가 들어온다
    // 카드를 구매 - 사용한다
    // 다음 플레이어에게 넘긴다.
    // 반복


    /// <summary>
    /// 플레이어 턴을 실행한다.
    /// </summary>
    /// <param name="rsh"></param>
    public void Run_PlayerTurn(int rsh)
    {

        //rsh를 기반으로 하위 실질적으로 동작이 필요한 오브젝트들에게 턴 동작을 하게 만든다.


    }






}
