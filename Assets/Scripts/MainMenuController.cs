using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject ExitText;

    private void Start()
    {
        ExitText.SetActive(false);
    }

    public void BTN_CallExit()
    {
        StartCoroutine(ExitAuto());
    }

    public void BTN_CallStart()
    {
        SceneManager.LoadScene(2);
    }


    private IEnumerator ExitAuto()
    {
        ExitText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 20; i++)
        {
            if (i % 2 != 0)
                ExitText.gameObject.SetActive(true);
            else
                ExitText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.05f);

        }

        ExitText.gameObject.SetActive(false);
    }
}
