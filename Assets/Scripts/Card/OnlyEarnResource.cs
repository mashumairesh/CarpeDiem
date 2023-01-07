using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyEarnResource : MonoBehaviour
{

    /// <summary>
    /// 버튼이 호출하여 오직 자원만을 엏는 스크립트 입니다.
    /// </summary>
    /// <param name="rsh"></param>

    public List<List<int>> listResource;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        listResource = new List<List<int>>();
        for (int i = 0; i < 5; i++)
        {
            listResource.Add(new List<int>());
            for(int j = 0; j < 5; j++)
                listResource[i].Add(0);
        }

        listResource[0][0] = 5;
        listResource[1][1] = 2;
        listResource[2][2] = 2;
        listResource[3][3] = 2;
        listResource[4][4] = 2;

    }

    public void BTN_CallButton(int rsh)
    {

        TableManager.instance.Get_NowPlayerScript().Gain(listResource[rsh]);
        TableManager.instance.End_PlayerTurn();

    }

}
