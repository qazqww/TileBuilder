using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMng
{
    public List<TileImg> tileImg = new List<TileImg>();

    public void Load(string path)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);

        // 복사본을 생성하여 저장하는 과정
        foreach(Sprite s in sprites)
        {
            TileImg tile = new TileImg();
            Texture2D texture = new Texture2D((int)s.textureRect.width, (int)s.textureRect.height);
            var pixels = s.texture.GetPixels((int)s.textureRect.x, (int)s.textureRect.y,
                                            (int)s.textureRect.width, (int)s.textureRect.height);
            texture.SetPixels(pixels);
            tile.texture = texture;
            tile.sprite = s;
            tile.name = tile.sprite.name;
            texture.Apply();
            tileImg.Add(tile);
            Debug.Log(tile.sprite);
        }
    }
}
