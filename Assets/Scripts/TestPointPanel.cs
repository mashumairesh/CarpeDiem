using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestPointPanel : MonoBehaviour
{

    [SerializeField] private Image Highlight;
    [SerializeField] private List<TextMeshProUGUI> Text;
    [SerializeField] private List<string> _Name;

    private void Awake()
    {
        _Name = new List<string>();
        _Name.Add("Money :");
        _Name.Add("A : ");
        _Name.Add("B : ");
        _Name.Add("C : ");
        _Name.Add("D : ");

    }

    public void DrawInfo(bool isHighlight, List<int> text)
    {
        if (isHighlight)
            Highlight.color = Color.red;
        else
            Highlight.color = Color.white;
        for(int i = 0; i < text.Count; i++)
            Text[i].text = _Name[i] + text[i].ToString();
    }




}
