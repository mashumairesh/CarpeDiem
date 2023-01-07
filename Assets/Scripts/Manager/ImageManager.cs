using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageManager : MonoBehaviour
{

    private Sprite[] sprites;

    /// <summary>
    /// 주어진 번호에 따라 필요한 스프라이트를 반환합니다.
    /// </summary>
    /// <param name="spriteNum">스프라이트 번호</param>
    /// <returns>해당 스프라이트</returns>
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
