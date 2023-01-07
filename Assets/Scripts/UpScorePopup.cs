using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpScorePopup : MonoBehaviour
{

    [SerializeField] private string ColorCode;
    //<color=red>
    //<color=#2ECDC0>
    [SerializeField] List<TextMeshProUGUI> text;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void DrawText(List<int> rsh)
    {

        for(int i = 0; i < text.Count; i++)
        {
            text[i].text = ColorCode + "+ " + rsh[i].ToString() + "</color>";
        }
        StartCoroutine(corFunc_SelfOff());
    }


    private IEnumerator corFunc_SelfOff()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
    

}
