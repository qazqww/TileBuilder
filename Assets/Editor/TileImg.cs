// 타일 이미지를 다루는데 필요한 요소들을 선언

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    None,
    CircleCollider,
    BoxCollider,
}

public enum TileType
{
    None,
    Wall,
    Sky,
}

public class TileImg
{
    public Sprite sprite;
    public Texture2D texture;
    public ColliderType colliderType = ColliderType.None;
    public TileType tileType = TileType.None;
    public Color color = Color.white;
    public int layerOrder;
}
