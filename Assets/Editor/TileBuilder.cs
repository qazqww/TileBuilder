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

    void Awake()
    {
        imageMng.Load("TileImages");
        viewer.SetTile(imageMng.tileImg);
    }
    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        cursor.SetShow(true);
    }

    private void OnDisable()
    {
        cursor.SetShow(false);
    }

    private void OnGUI()
    {
        viewer.Draw(0, 0, 800, 800, 5);
    }
}

/*
 유니티 Lagacy GUI 클래스
 GUI : 완성된 형태
 GUILayout, EditorGUILayout : 부품 형태, 세부 설정도 가능

 GUI, GUILayout은 일반 프로그램이나 에디터에서 공통적으로 사용할 수 있다
 EditorGUILayout은 에디터 상에서만 사용 가능, 세부 기능이 더 많다
 */