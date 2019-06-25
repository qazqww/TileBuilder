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
    TileViewer tileViewer = new TileViewer();
    TileInfo tileInfo = new TileInfo();
    TileBuilderTag tagBuilder = new TileBuilderTag();
    InsertTag insertTag = new InsertTag();

    SpriteRenderer tilePrefab;

    bool inputMainTag = false;

    string path;
    public string Path
    {
        get { return Application.dataPath + "/Resources/TagData/"; }
    }

    string TagName
    {
        get { return "Tag.txt"; }
    }

    string LoadPath
    {
        get { return "TagData/Tag"; }
    }

    void Awake()
    {
        path = Path;

        //tagBuilder.AddTag("Tiles", "Cake");
        //tagBuilder.AddTag("Tiles", "Castle");
        //tagBuilder.AddTag("Tiles", "Choco");
        //tagBuilder.AddTag("Tiles", "Grass");
        //tagBuilder.AddTag("Tiles", "Ice");
        //tagBuilder.AddTag("Tiles", "Lava");
        //tagBuilder.AddTag("Tiles", "Sand");
        //tagBuilder.AddTag("Tiles", "Snow");
        //tagBuilder.AddTag("Tiles", "Stone");
        //tagBuilder.AddTag("Tiles", "Tundra");
        //tagBuilder.AddTag("Tiles", "Water");
        
        // 저장할 때만 확장자 필요
        //tagBuilder.SaveTag(path, "Tag.txt");
        tagBuilder.LoadTag(LoadPath);

        tilePrefab = Resources.Load<SpriteRenderer>("Prefabs/TilePrefab");
        imageMng.Load("TileImages");

        tagBuilder.SetMainTag("Tiles");
        SettingTile();
    }
    
    void Update()
    {
        if (tagBuilder.Changed)
        {
            SettingTile();
        }

        if (cursor.IsClick)
        {
            if (tileViewer == null || tileViewer.SelTile == null)
                return;

            SpriteRenderer sprite = Instantiate(tilePrefab, cursor.ClickPos, Quaternion.identity);
            sprite.sprite = tileViewer.SelTile.sprite;
            sprite.color = tileViewer.SelTile.color;
            sprite.sortingOrder = tileViewer.SelTile.layerOrder;

            switch (tileViewer.SelTile.colliderType)
            {
                case ColliderType.BoxCollider:
                    if (sprite.GetComponent<BoxCollider2D>() == null)
                        sprite.gameObject.AddComponent<BoxCollider2D>();
                    break;
                case ColliderType.CircleCollider:
                    if (sprite.GetComponent<CircleCollider2D>() == null)
                        sprite.gameObject.AddComponent<CircleCollider2D>();
                    break;
            }

            cursor.SetClickState(false);
        }

        if(inputMainTag)
        {
            tagBuilder.AddTag(insertTag.MainTag, insertTag.SubTag);
            inputMainTag = false;
        }

        if(insertTag.Load)
            tagBuilder.LoadTag(LoadPath);

        if (insertTag.Save)
            tagBuilder.SaveTag(path, TagName);
    }

    public void SettingTile()
    {
        TileImg[] tileArr = imageMng.GetTile(tagBuilder.Tag);
        if(tileArr != null)
            tileViewer.SetTile(tileArr);
    }

    private void OnEnable()
    {
        cursor.SetShow(true);
        SettingTile();
        //tileViewer.SetTile(imageMng.tileImg);
    }

    private void OnDisable()
    {
        cursor.SetShow(false);
    }

    private void OnGUI()
    {
        tagBuilder.Draw(0, 0, 550, 40);
        tileViewer.Draw(0, 40, 550, 600, 5);
        tileInfo.Draw(560, 70, 200, 350, tileViewer.SelTile);
        insertTag.Draw(560, 00, 200, 65, ref inputMainTag);
    }
}

/*
 유니티 Lagacy GUI 클래스
 GUI : 완성된 형태
 GUILayout, EditorGUILayout : 부품 형태, 세부 설정도 가능

 GUI, GUILayout은 일반 프로그램이나 에디터에서 공통적으로 사용할 수 있다
 EditorGUILayout은 에디터 상에서만 사용 가능, 세부 기능이 더 많다
 */