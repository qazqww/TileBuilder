using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class OpenBuilder
{
    // 유니티 에디터 상에 메뉴를 추가할 수 있음!
    [MenuItem("Window/OpenBuilder")]
    public static void OpenMenu()
    {
        TileBuilder builder = (TileBuilder)EditorWindow.GetWindow(typeof(TileBuilder));
        builder.minSize = new Vector2(100, 100);
    }
}
