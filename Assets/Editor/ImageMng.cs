// 리소스의 Sprite를 복사하여 새로운 List 'tileImg' 에 저장

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMng
{
    public List<TileImg> tileImg = new List<TileImg>();

    public void Load(string path)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);

        // 리소스로부터 복사본을 생성하여 새 클래스 객체(리스트)에 저장하는 과정        
        foreach (Sprite s in sprites)
        {
            TileImg tile = new TileImg();
            Texture2D texture = new Texture2D((int)s.textureRect.width, (int)s.textureRect.height);
            var pixels = s.texture.GetPixels((int)s.textureRect.x, (int)s.textureRect.y,
                                            (int)s.textureRect.width, (int)s.textureRect.height);
            texture.SetPixels(pixels);
            texture.name = s.name;
            tile.texture = texture;
            tile.sprite = s;
            texture.Apply();
            tileImg.Add(tile);
            //Debug.Log(tile.sprite);
        }
    }

    // 파일명 비교를 통해 서브 키에 해당하는 이미지 리스트를 리턴
    public TileImg[] GetTile(string key)
    {
        if (string.IsNullOrEmpty(key))
            return null;

        List<TileImg> tileList = new List<TileImg>();

        for (int i=0; i<tileImg.Count; i++)
        {
            string imageName = tileImg[i].sprite.name.ToLower();
            if (imageName.Contains(key.ToLower()))
            {
                tileList.Add(tileImg[i]);
            }
        }

        return tileList.ToArray();
    }
}

// Texture와 Sprite의 차이
// Texture 는 데이터 입니다. 실제 이미지를 가지고 있는 데이터!
// Sprite 는 그 값을 가지고 행위를 하는 개체 입니다. 얘가 데이터(Texture)를 어떻게 그려낼지 결정하죠 (좀 더 상위개념)