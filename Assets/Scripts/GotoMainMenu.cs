using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GotoMainMenu : MonoBehaviour
{
    [SerializeField] private List<Image> listImage;

    [SerializeField] private float WaitTime;

    [SerializeField] private Text text;

    private void Start()
    {
        for (int i = 0; i < listImage.Count; i++)
        {
            listImage[i].gameObject.SetActive(false);
            listImage[i].DOFade(0f, 0f);
        }

        StartCoroutine(corFunc_StartGame());
    }

    private IEnumerator corFunc_StartGame()
    {
        yield return new WaitForSeconds(2f);

        listImage[0].gameObject.SetActive(true);
        listImage[0].DOFade(1f, 1f);
        text.DOText("Team Carpe Diem Presents", 1.5f);

        yield return new WaitForSeconds(1.5f);

        listImage[0].DOKill(true);
        listImage[0].DOFade(0f, 1f);

        yield return new WaitForSeconds(2f);
        listImage[0].gameObject.SetActive(false);

        

        SceneManager.LoadScene(1);

    }

}
