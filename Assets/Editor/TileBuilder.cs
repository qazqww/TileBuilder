// 실질적인 창에 해당하는 클래스
// 창이 열리고 닫힘에 따라 여러 기능들이 작동하도록 설정

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileBuilder : EditorWindow
{
    ImageMng imageMng = new ImageMng();
    TileCursor cursor = new TileCursor();
    TileViewer viewer = new TileViewer();
    TileInfo info = new TileInfo();

    SpriteRenderer tilePrefab;

    void Awake()
    {
        tilePrefab = Resources.Load<SpriteRenderer>("Prefabs/TilePrefab");
        imageMng.Load("TileImages");        
    }
    
    void Update()
    {
        if(cursor.IsClick)
        {
            //if (viewer == null || viewer.SelTile == null)
            if (viewer.SelTile == null)
                return;

            SpriteRenderer sprite = Instantiate(tilePrefab, cursor.ClickPos, Quaternion.identity);
            sprite.sprite = viewer.SelTile.sprite;
            sprite.color = viewer.SelTile.color;
            sprite.sortingOrder = viewer.SelTile.layerOrder;

            switch (viewer.SelTile.colliderType)
            {
                case ColliderType.BoxCollider:
                    if (sprite.GetComponent<BoxCollider2D>() != null)
                        sprite.gameObject.AddComponent<BoxCollider2D>();
                    break;
                case ColliderType.CircleCollider:
                    if (sprite.GetComponent<CircleCollider2D>() != null)
                        sprite.gameObject.AddComponent<CircleCollider2D>();
                    break;
            }

            cursor.SetClickState(false);
        }
    }

    private void OnEnable()
    {
        cursor.SetShow(true);
        viewer.SetTile(imageMng.tileImg);
    }

    private void OnDisable()
    {
        cursor.SetShow(false);
    }

    private void OnGUI()
    {
        viewer.Draw(0, 0, 550, 600, 5);
        info.Draw(560, 0, 200, 350, viewer.SelTile);
    }
}

/*
 유니티 Lagacy GUI 클래스
 GUI : 완성된 형태
 GUILayout, EditorGUILayout : 부품 형태, 세부 설정도 가능

 GUI, GUILayout은 일반 프로그램이나 에디터에서 공통적으로 사용할 수 있다
 EditorGUILayout은 에디터 상에서만 사용 가능, 세부 기능이 더 많다
 */