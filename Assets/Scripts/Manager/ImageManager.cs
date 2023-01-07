using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageManager : MonoBehaviour
{

    private Sprite[] sprites;

    /// <summary>
    /// �־��� ��ȣ�� ���� �ʿ��� ��������Ʈ�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="spriteNum">��������Ʈ ��ȣ</param>
    /// <returns>�ش� ��������Ʈ</returns>
    public Sprite GetSprite(int spriteNum)
    {
        return sprites[spriteNum];
    }

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("");
        Debug.Log(sprites.Length);
        foreach(var sprite in sprites)
        {
            Debug.Log(sprite.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
